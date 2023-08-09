package repositories;

import entities.Apartment;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ApartmentCrudRepository extends CrudRepository<Apartment, Long> {
    List<Apartment> findApartmentsByBuilding(@Param("id") Long id);
}
