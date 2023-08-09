package jdbc;
import entities.Building;
import interfaces.BuildingTable;
import tools.BuildingType;
import tools.DatabaseException;

import java.sql.Connection;
import java.sql.SQLException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.DriverManager;
import java.util.ArrayList;

public class JdbcConnectionBuildingTable implements BuildingTable {
    private final String url;

    private final String username;

    private final String password;

    private Connection connection;

    public JdbcConnectionBuildingTable(String url, String username, String password) {
        this.url = url;
        this.username = username;
        this.password = password;
    }

    public Building save(Building entity) throws DatabaseException, SQLException {
        String query = "INSERT INTO buildings (Name, Building_date, Floors_amount, Building_type, Street) VALUES (?, ?, ?, ?, ?)";

        try {
            openConnectionToDB();
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to save entity");
        }

        PreparedStatement statement = null;
        try {
            statement = connection.prepareStatement(query);
            formPreparedStatement(statement, entity);
        } catch (Exception e) {
            connection.close();
            if (statement != null)
                statement.close();
            throw new DatabaseException("Failed to save entity");
        }

        executeStatement(statement);
        return entity;
    }

    public Building update(Building entity) throws DatabaseException, SQLException {
        String query = "UPDATE buildings SET Name = ?, Building_date = ?, Floors_amount = ?, Building_type = ?, Street = ? WHERE ID = ?";

        try {
            openConnectionToDB();
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to save entity");
        }

        PreparedStatement statement = null;
        try {
            statement = connection.prepareStatement(query);
            formPreparedStatement(statement, entity);
            statement.setLong(6, entity.getId());
        } catch (Exception e) {
            connection.close();
            if (statement != null)
                statement.close();
            throw new DatabaseException("Failed to save entity");
        }

        executeStatement(statement);
        return entity;
    }

    public Building getById(long id) throws SQLException, DatabaseException {
        String query = "SELECT * FROM buildings WHERE ID = ?";

        try {
            openConnectionToDB();
        } catch (Exception E) {
            connection.close();
            throw new DatabaseException("Failed to getById");
        }

        PreparedStatement statement = null;
        try {
            statement = connection.prepareStatement(query);
            statement.setLong(1, id);
        } catch (Exception e) {
            connection.close();
            if (statement != null)
                statement.close();
            throw new DatabaseException("Failed to getById");
        }

        ResultSet resultSet = null;
        try {
            resultSet = statement.executeQuery();
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to getById");
        }

        Building building = null;
        try {
            if (resultSet.next()) {
                building = new Building(
                        resultSet.getLong("ID"),
                        resultSet.getString("Name"),
                        resultSet.getDate("Building_date"),
                        resultSet.getInt("Floors_amount"),
                        BuildingType.valueOf(resultSet.getString("Building_type")),
                        resultSet.getLong("Street")
                );
            }
        } catch (Exception e) {
            connection.close();
            statement.close();
            resultSet.close();
            throw new DatabaseException("Failed to getById");
        }

        connection.close();
        statement.close();
        resultSet.close();

        if (building == null)
            throw new DatabaseException("There is not building with given id!");

        return building;
    }

    public void deleteById(long id) throws DatabaseException, SQLException {
        String query = "DELETE FROM buildings WHERE ID = ?";

        try {
            openConnectionToDB();
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to deleteById");
        }

        PreparedStatement statement = null;
        try {
            statement = connection.prepareStatement(query);
            statement.setLong(1, id);
        } catch (Exception e) {
            connection.close();
            if (statement != null)
                statement.close();
            throw new DatabaseException("Failed to deleteById");
        }

        executeStatement(statement);
    }

    public void deleteByEntity(Building entity) throws SQLException, DatabaseException {
        deleteById(entity.getId());
    }

    public void deleteAll() throws DatabaseException, SQLException {
        String query = "DELETE FROM buildings";

        try {
            openConnectionToDB();
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to deleteAll");
        }

        PreparedStatement statement = null;
        try {
            statement = connection.prepareStatement(query);
        } catch (Exception e) {
            connection.close();
        }

        executeStatement(statement);
    }

    public ArrayList<Building> getAll() throws DatabaseException, SQLException {
        String query = "SELECT * FROM buildings";

        return formListOfAllBuildings(query);
    }

    public ArrayList<Building> getAllByVId(long id) throws DatabaseException, SQLException {
        String query = "SELECT * FROM buildings WHERE Street = ?";

        return formListOfAllBuildingsByParentId(query, id);
    }

    private void executeStatement(PreparedStatement statement) throws SQLException, DatabaseException {
        try {
            statement.executeUpdate();
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to executeStatement");
        }

        connection.close();
        statement.close();
    }

    private void formPreparedStatement(PreparedStatement statement, Building entity) throws SQLException, DatabaseException {
        try {
            statement.setString(1, entity.getName());
            statement.setDate(2, entity.getBuildingDate());
            statement.setInt(3, entity.getFloorsAmount());
            statement.setString(4, entity.getBuildingType().name());
            statement.setLong(5, entity.getStreetId());
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to update formPreparedStatement");
        }
    }

    private void tryOpenConnection() throws DatabaseException, SQLException {
        try {
            openConnectionToDB();
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery. Connection");
        }
    }

    private PreparedStatement tryPrepareStatement(String query) throws DatabaseException, SQLException {
        PreparedStatement statement = null;
        try {
            statement = connection.prepareStatement(query);
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery. Statement");
        }
        return statement;
    }

    private ResultSet tryExecuteQuery(PreparedStatement statement) throws SQLException, DatabaseException {
        ResultSet resultSet = null;
        try {
            resultSet = statement.executeQuery();
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery. ResultSet");
        }
        return resultSet;
    }

    private ArrayList<Building> formListByStatement(ResultSet resultSet, PreparedStatement statement) throws SQLException, DatabaseException {
        ArrayList<Building> result = new ArrayList<>();
        try {
            while (resultSet.next()) {
                result.add(new Building (
                        resultSet.getLong("ID"),
                        resultSet.getString("Name"),
                        resultSet.getDate("Building_date"),
                        resultSet.getInt("Floors_amount"),
                        BuildingType.valueOf(resultSet.getString("Building_type")),
                        resultSet.getLong("Street"))
                );
            }
        } catch (Exception e) {
            connection.close();
            statement.close();
            resultSet.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery. List");
        }
        return result;
    }

    private ArrayList<Building> formListOfAllBuildingsByParentId(String query, long id) throws DatabaseException, SQLException {
        tryOpenConnection();
        PreparedStatement statement = tryPrepareStatement(query);
        statement.setLong(1, id);
        ResultSet resultSet = tryExecuteQuery(statement);
        ArrayList<Building> result = formListByStatement(resultSet, statement);

        connection.close();
        statement.close();
        resultSet.close();

        return result;
    }

    private ArrayList<Building> formListOfAllBuildings(String query) throws DatabaseException, SQLException {
        tryOpenConnection();
        PreparedStatement statement = tryPrepareStatement(query);
        ResultSet resultSet = tryExecuteQuery(statement);
        ArrayList<Building> result = formListByStatement(resultSet, statement);

        connection.close();
        statement.close();
        resultSet.close();

        return result;
    }

    private void openConnectionToDB() throws DatabaseException {
        try {
            connection = DriverManager.getConnection(url, username, password);
        } catch (SQLException e) {
            throw new DatabaseException(e.getMessage());
        }
    }
}
