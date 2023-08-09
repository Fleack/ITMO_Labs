package hibernate;

import entities.Building;
import interfaces.BuildingTable;
import tools.DatabaseException;

import org.hibernate.Transaction;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class HibernateConnectionBuildingTable implements BuildingTable {
    private final HibernateConnection connection;

    public HibernateConnectionBuildingTable() {
        connection = new HibernateConnection();
    }

    public Building save(Building entity) throws DatabaseException {
        connection.openConnection();
        
        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().persist(entity);
        tx.commit();

        connection.closeConnection();

        return entity;
    }
    
    public Building update(Building entity) throws DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().merge(entity);
        tx.commit();

        connection.closeConnection();

        return entity;
    }

    public Building getById(long id) throws DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        Building result = connection.getSession().find(Building.class, id);
        tx.commit();

        connection.closeConnection();

        return result;
    }

    public void deleteById(long id) throws DatabaseException, SQLException {
        Building entity = getById(id);
        connection.openConnection();

        if (entity == null) {
            connection.closeConnection();
            throw new DatabaseException("Failed to deleteById");
        }

        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().remove(entity);
        tx.commit();

        connection.closeConnection();
    }
    
    public void deleteByEntity(Building entity) throws DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        connection.getSession().remove(entity);
        tx.commit();

        connection.closeConnection();
    }
    
    public void deleteAll() throws DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        String stringQuery = "DELETE FROM Building b";
        connection.getSession().createQuery(stringQuery).executeUpdate();
        tx.commit();

        connection.closeConnection();
    }
    
    public ArrayList<Building> getAll() throws DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        String stringQuery = "SELECT b FROM Building b";
        List<Building> result = connection.getSession().createQuery(stringQuery, Building.class).getResultList();
        tx.commit();

        connection.closeConnection();

        return new ArrayList<>(result);
    }
    
    public ArrayList<Building> getAllByVId(long id) throws DatabaseException {
        connection.openConnection();

        Transaction tx = connection.getSession().beginTransaction();
        String stringQuery = "SELECT b FROM Building b WHERE b.streetId = :id";
        List<Building> result = connection.getSession().createQuery(stringQuery, Building.class)
                .setParameter("id", id)
                .getResultList();
        tx.commit();

        connection.closeConnection();

        return new ArrayList<>(result);
    }
}
