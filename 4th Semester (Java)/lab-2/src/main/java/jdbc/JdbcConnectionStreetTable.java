package jdbc;

import entities.Building;
import entities.Street;
import interfaces.StreetTable;
import tools.DatabaseException;

import javax.xml.crypto.Data;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.DriverManager;
import java.util.ArrayList;

public class JdbcConnectionStreetTable implements StreetTable {
    private final String url;

    private final String username;

    private final String password;

    private Connection connection;

    public JdbcConnectionStreetTable(String url, String username, String password) {
        this.url = url;
        this.username = username;
        this.password = password;
    }

    public Street save(Street entity) throws DatabaseException, SQLException {
        String query = "INSERT INTO streets (Name, Postal_code) VALUES (?, ?)";

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

    public Street update(Street entity) throws DatabaseException, SQLException {
        String query = "UPDATE streets SET Name = ?, Postal_code = ? WHERE ID = ?";

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
            statement.setLong(3, entity.getId());
        } catch (Exception e) {
            connection.close();
            if (statement != null)
                statement.close();
            throw new DatabaseException("Failed to save entity");
        }

        executeStatement(statement);
        return entity;
    }

    public Street getById(long id) throws SQLException, DatabaseException {
        String query = "SELECT * FROM streets WHERE ID = ?";

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

        Street street = null;
        try {
            if (resultSet.next()) {
                street = new Street(
                        resultSet.getLong("ID"),
                        resultSet.getString("Name"),
                        resultSet.getInt("Postal_code")
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

        if (street == null)
            throw new DatabaseException("There is not building with given id!");

        return street;
    }

    public void deleteById(long id) throws DatabaseException, SQLException {
        String query = "DELETE FROM streets WHERE ID = ?";

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

        JdbcConnectionBuildingTable buildingTable = new JdbcConnectionBuildingTable(url, username, password);
        ArrayList<Building> buildingsList = buildingTable.getAllByVId(id);
        try {
            for(Building building : buildingsList) {
                buildingTable.deleteByEntity(building);
            }
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to deleteById");
        }

        executeStatement(statement);
    }

    public void deleteByEntity(Street entity) throws SQLException, DatabaseException {
        deleteById(entity.getId());
    }

    public void deleteAll() throws DatabaseException, SQLException {
        String query = "DELETE FROM streets";

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

        JdbcConnectionBuildingTable buildingTable = new JdbcConnectionBuildingTable(url, username, password);
        buildingTable.deleteAll();
        executeStatement(statement);
    }

    public ArrayList<Street> getAll() throws DatabaseException, SQLException {
        String query = "SELECT * FROM streets";

        return fromListOfBuildingsByQuery(query);
    }

    private void executeStatement(PreparedStatement statement) throws SQLException, DatabaseException {
        try {
            statement.executeUpdate();
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to save entity");
        }

        connection.close();
        statement.close();
    }

    private void formPreparedStatement(PreparedStatement statement, Street entity) throws SQLException, DatabaseException {
        try {
            statement.setString(1, entity.getName());
            statement.setInt(2, entity.getPostalCode());
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to update formPreparedStatement");
        }
    }

    private ArrayList<Street> fromListOfBuildingsByQuery(String query) throws DatabaseException, SQLException {
        try {
            openConnectionToDB();
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery");
        }

        PreparedStatement statement = null;
        try {
            statement = connection.prepareStatement(query);
        } catch (Exception e) {
            connection.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery");
        }

        ResultSet resultSet = null;
        try {
            resultSet = statement.executeQuery();
        } catch (Exception e) {
            connection.close();
            statement.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery");
        }

        ArrayList<Street> result = new ArrayList<>();

        try {
            while (resultSet.next()) {
                result.add(new Street (
                        resultSet.getLong("ID"),
                        resultSet.getString("Name"),
                        resultSet.getInt("Postal_code"))
                );
            }
        } catch (Exception e) {
            connection.close();
            statement.close();
            resultSet.close();
            throw new DatabaseException("Failed to update fromListOfBuildingsByQuery");
        }

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
