package mybatis;

import entities.Street;
import interfaces.StreetTable;
import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;
import tools.DatabaseException;

import java.io.IOException;
import java.io.InputStream;
import java.sql.SQLException;
import java.util.ArrayList;

public class BatisConnectionStreetTable implements StreetTable {
    SqlSession session;

    StreetMapper streetMapper;

    public BatisConnectionStreetTable(String batisConfig) throws IOException {
        InputStream inputStream = Resources.getResourceAsStream(batisConfig);
        SqlSessionFactory sqlSessionFactory = new SqlSessionFactoryBuilder().build(inputStream);
        session = sqlSessionFactory.openSession();

        streetMapper = session.getMapper(StreetMapper.class);
    }

    public Street save(Street entity) throws DatabaseException, SQLException {
        streetMapper.save(entity);
        session.commit();
        return entity;
    }

    public Street update(Street entity) throws DatabaseException, SQLException {
        streetMapper.update(entity);
        session.commit();
        return entity;
    }

    public Street getById(long id) throws SQLException, DatabaseException {
        return streetMapper.getById(id);
    }

    public void deleteById(long id) throws DatabaseException, SQLException, IOException {
        streetMapper.deleteById(id);
        session.commit();
    }

    public void deleteByEntity(Street entity) throws SQLException, DatabaseException, IOException {
        streetMapper.deleteByEntity(entity);
        session.commit();
    }

    public void deleteAll() throws DatabaseException, SQLException, IOException {
        streetMapper.deleteAll();
        session.commit();
    }

    public ArrayList<Street> getAll() throws DatabaseException, SQLException {
        return streetMapper.getAll();
    }
}
