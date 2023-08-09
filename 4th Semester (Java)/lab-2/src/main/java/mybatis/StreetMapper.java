package mybatis;

import entities.Building;
import entities.Street;
import org.apache.ibatis.annotations.Insert;
import org.apache.ibatis.annotations.Options;
import org.apache.ibatis.annotations.Result;
import org.apache.ibatis.annotations.Results;
import org.apache.ibatis.annotations.Update;
import org.apache.ibatis.annotations.Delete;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;

public interface StreetMapper {
    @Insert("INSERT INTO streets (Name, Postal_code)" +
            " VALUES (#{name}, #{postalCode})")
    @Options(useGeneratedKeys = true, keyProperty = "id")
    int saveStreet(Street entity);

    @Update("UPDATE streets " +
            "SET Name = #{name}, " +
            "Postal_code = #{postalCode} " +
            "WHERE ID = #{id}")
    int updateStreet(Street entity);

    @Select("SELECT * FROM streets WHERE ID = #{id}")
    @Results({
            @Result(property = "id", column = "ID"),
            @Result(property = "name", column = "Name"),
            @Result(property = "postalCode", column = "Postal_code"),
    })
    Street getById(long id);

    @Delete("DELETE FROM streets WHERE ID = #{id};")
    void deleteStreetById(long id);

    @Select("SELECT * FROM streets")
    @Results({
            @Result(property = "id", column = "ID"),
            @Result(property = "name", column = "Name"),
            @Result(property = "postalCode", column = "Postal_code"),
    })
    ArrayList<Street> getAll();

    @Delete("DELETE FROM streets")
    void deleteAllStreets();

    default void deleteAll() throws IOException {
        String resource = "mybatis-config.xml";
        InputStream inputStream = Resources.getResourceAsStream(resource);
        SqlSessionFactory sqlSessionFactory = new SqlSessionFactoryBuilder().build(inputStream);
        BuilderMapper bcon = null;
        try (SqlSession session = sqlSessionFactory.openSession()) {
            bcon = session.getMapper(BuilderMapper.class);
            bcon.deleteAll();
            session.commit();
        }

        deleteAllStreets();
    }

    default void deleteById(long id) throws IOException {
        String resource = "mybatis-config.xml";
        InputStream inputStream = Resources.getResourceAsStream(resource);
        SqlSessionFactory sqlSessionFactory = new SqlSessionFactoryBuilder().build(inputStream);
        BuilderMapper bcon = null;
        try (SqlSession session = sqlSessionFactory.openSession()) {
            bcon = session.getMapper(BuilderMapper.class);
            ArrayList<Building> buildings = bcon.getAllByVId(id);
            for(Building building : buildings) {
                bcon.deleteByEntity(building);
            }
            session.commit();
        }

        deleteStreetById(id);
    }

    default void deleteByEntity(Street entity) throws IOException {
        deleteById(entity.getId());
    }

    default Street save(Street building) {
        saveStreet(building);
        return building;
    }

    default Street update(Street entity) {
        updateStreet(entity);
        return entity;
    }
}
