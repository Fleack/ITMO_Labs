package interfaces;


import entities.Street;
import tools.DatabaseException;

import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;

public interface StreetTable {
    public Street save(Street entity) throws DatabaseException, SQLException;

    public Street update(Street entity) throws DatabaseException, SQLException;

    public Street getById(long id) throws SQLException, DatabaseException;

    public void deleteById(long id) throws DatabaseException, SQLException, IOException;

    public void deleteByEntity(Street entity) throws SQLException, DatabaseException, IOException;

    public void deleteAll() throws DatabaseException, SQLException, IOException;

    public ArrayList<Street> getAll() throws DatabaseException, SQLException;
}
