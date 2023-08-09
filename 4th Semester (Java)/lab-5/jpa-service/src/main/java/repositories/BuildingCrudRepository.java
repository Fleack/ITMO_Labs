package repositories;

import entities.Building;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface BuildingCrudRepository extends CrudRepository<Building, Long> {
    List<Building> findBuildingsByStreet(@Param("id") Long id);

    List<Building> findAllByName(@Param("id") String name);
}