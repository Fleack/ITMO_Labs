package mybatis;

import entities.Building;
import interfaces.BuildingTable;
import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;

public class BatisConnectionBuildingTable implements BuildingTable {
    SqlSession session;

    BuilderMapper builderMapper;

    public BatisConnectionBuildingTable(String batisConfig) throws IOException {
        InputStream inputStream = Resources.getResourceAsStream(batisConfig);
        SqlSessionFactory sqlSessionFactory = new SqlSessionFactoryBuilder().build(inputStream);
        session = sqlSessionFactory.openSession();

        builderMapper = session.getMapper(BuilderMapper.class);
    }

    public Building save(Building entity) {
        builderMapper.save(entity);
        session.commit();
        return entity;
    }

    public void deleteById(long id) {
        builderMapper.deleteById(id);
        session.commit();
    }

    public void deleteByEntity(Building entity) {
        builderMapper.deleteByEntity(entity);
        session.commit();
    }

    public void deleteAll() {
        builderMapper.deleteAll();
        session.commit();
    }

    public Building update(Building entity) {
        builderMapper.update(entity);
        session.commit();
        return entity;
    }

    public Building getById(long id) {
        return builderMapper.getById(id);
    }

    public ArrayList<Building> getAll() {
        return builderMapper.getAll();
    }

    public ArrayList<Building> getAllByVId(long id) {
        return builderMapper.getAllByVId(id);
    }
}
