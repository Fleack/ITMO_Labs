package repositories;

import entities.Street;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;


@Repository
public interface StreetCrudRepository extends CrudRepository<Street, Long> {
    List<Street> findAllByName(@Param("id") String name);
}
