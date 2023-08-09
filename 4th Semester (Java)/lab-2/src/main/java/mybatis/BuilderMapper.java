package mybatis;

import entities.Building;
import org.apache.ibatis.annotations.Delete;
import org.apache.ibatis.annotations.Insert;
import org.apache.ibatis.annotations.Options;
import org.apache.ibatis.annotations.Update;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Result;
import org.apache.ibatis.annotations.Results;

import java.util.ArrayList;

public interface BuilderMapper {
    @Insert("INSERT INTO buildings (Name, Building_date, Floors_amount, Building_type, Street)" +
            " VALUES (#{name}, #{buildingDate}, #{floorsAmount}, #{buildingType}, #{streetId})")
    @Options(useGeneratedKeys = true, keyProperty = "id")
    int insertBuilding(Building entity);

    @Update("UPDATE buildings " +
            "SET Name = #{name}, " +
            "Building_date = #{buildingDate}, " +
            "Floors_amount = #{floorsAmount}, " +
            "Building_type = #{buildingType}, " +
            "Street = #{streetId} " +
            "WHERE ID = #{id}")
    int updateBuilding(Building entity);

    @Select("SELECT * FROM buildings WHERE ID = #{id}")
    @Results({
            @Result(property = "id", column = "ID"),
            @Result(property = "name", column = "Name"),
            @Result(property = "buildingDate", column = "Building_date", javaType = java.sql.Date.class),
            @Result(property = "floorsAmount", column = "Floors_amount"),
            @Result(property = "buildingType", column = "Building_type"),
            @Result(property = "streetId", column = "Street"),
    })
    Building getById(long id);

    @Delete("DELETE FROM buildings WHERE ID = #{id}")
    void deleteById(long id);

    @Delete("DELETE FROM buildings WHERE ID = #{id}")
    void deleteByEntity(Building entity);

    @Delete("DELETE FROM buildings")
    void deleteAll();

    @Select("SELECT * FROM buildings")
    @Results({
            @Result(property = "id", column = "ID"),
            @Result(property = "name", column = "Name"),
            @Result(property = "buildingDate", column = "Building_date", javaType = java.sql.Date.class),
            @Result(property = "floorsAmount", column = "Floors_amount"),
            @Result(property = "buildingType", column = "Building_type"),
            @Result(property = "streetId", column = "Street"),
    })
    ArrayList<Building> getAll();

    @Select("SELECT * FROM buildings WHERE Street = #{id}")
    @Results({
            @Result(property = "id", column = "ID"),
            @Result(property = "name", column = "Name"),
            @Result(property = "buildingDate", column = "Building_date", javaType = java.sql.Date.class),
            @Result(property = "floorsAmount", column = "Floors_amount"),
            @Result(property = "buildingType", column = "Building_type"),
            @Result(property = "streetId", column = "Street"),
    })
    ArrayList<Building> getAllByVId(long id);

    default Building save(Building building) {
        insertBuilding(building);
        return building;
    }

    default Building update(Building entity) {
        updateBuilding(entity);
        return entity;
    }
}
