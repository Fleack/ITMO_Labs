package repositories;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import entities.User;

import java.util.Optional;


@Repository
public interface UserCrudRepository extends CrudRepository<User, Long> {
    Optional<User> findByLogin(@Param("id") String login);
}
