package hibernate;

import entities.Building;
import entities.Street;
import interfaces.StreetTable;
import org.hibernate.Transaction;
import tools.DatabaseException;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class HibernateConnectionStreetTable implements StreetTable {
    private final HibernateConnection connection;

    public HibernateConnectionStreetTable() {
        connection = new HibernateConnection();
    }

    public Street save(Street entity) throws DatabaseException, SQLException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().persist(entity);
        tx.commit();

        connection.closeConnection();

        return entity;
    }
    
    public Street update(Street entity) throws DatabaseException, SQLException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().merge(entity);
        tx.commit();

        connection.closeConnection();

        return entity;
    }
    
    public Street getById(long id) throws SQLException, DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        Street result = connection.getSession().find(Street.class, id);
        tx.commit();

        connection.closeConnection();

        return result;
    }
    
    public void deleteById(long id) throws DatabaseException, SQLException {
        Street entity = getById(id);
        connection.openConnection();

        if (entity == null) {
            connection.closeConnection();
            throw new DatabaseException("Failed to deleteById");
        }

        HibernateConnectionBuildingTable buildingTable = new HibernateConnectionBuildingTable();
        ArrayList<Building> buildings = buildingTable.getAllByVId(id);
        for (Building building : buildings) {
            buildingTable.deleteByEntity(building);
        }

        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().remove(entity);
        tx.commit();

        connection.closeConnection();
    }
    
    public void deleteByEntity(Street entity) throws SQLException, DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().remove(entity);
        tx.commit();

        connection.closeConnection();
    }
    
    public void deleteAll() throws DatabaseException, SQLException {
        connection.openConnection();

        HibernateConnectionBuildingTable buildingTable = new HibernateConnectionBuildingTable();
        buildingTable.deleteAll();

        Transaction tx = connection.getSession().beginTransaction();
        String stringQuery = "DELETE FROM Street s";
        connection.getSession().createQuery(stringQuery).executeUpdate();
        tx.commit();

        connection.closeConnection();
    }
    
    public ArrayList<Street> getAll() throws DatabaseException, SQLException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        String stringQuery = "SELECT s FROM Street s";
        List<Street> result = connection.getSession().createQuery(stringQuery, Street.class).getResultList();
        tx.commit();

        connection.closeConnection();

        return new ArrayList<>(result);
    }
}
