package interfaces;

import entities.Building;
import tools.BuildingType;
import tools.DatabaseException;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

public interface BuildingTable {
    public Building save(Building entity) throws DatabaseException, SQLException;

    public Building update(Building entity) throws DatabaseException, SQLException;

    public Building getById(long id) throws SQLException, DatabaseException;

    public void deleteById(long id) throws DatabaseException, SQLException;

    public void deleteByEntity(Building entity) throws SQLException, DatabaseException;

    public void deleteAll() throws DatabaseException, SQLException;

    public ArrayList<Building> getAll() throws DatabaseException, SQLException;

    public ArrayList<Building> getAllByVId(long id) throws DatabaseException, SQLException;
}
