package services;

import entities.Street;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import repositories.StreetCrudRepository;
import tools.ControllerException;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class StreetService {
    private final StreetCrudRepository repository;

    @Autowired
    public StreetService(StreetCrudRepository repository) {
        this.repository = repository;
    }

    public Street save(Street entity) {
        return repository.save(entity);
    }

    public void deleteById(long id) {
        repository.deleteById(id);
    }

    public void deleteByEntity(Street entity) {
        repository.delete(entity);
    }

    public void deleteAll() {
        repository.deleteAll();
    }

    public Street update(Street entity) {
        return repository.save(entity);
    }

    public Street getById(long id) throws ControllerException {
        Optional<Street> entity = repository.findById(id);

        if(entity.isEmpty())
            throw new ControllerException("No entity with given id");

        return entity.get();
    }

    public List<Street> getAll() {
        List<Street> list = new ArrayList<>();
        repository.findAll().forEach(list::add);
        return list;
    }

    public List<Street> getAllByName(String name) {
        return repository.findAllByName(name);
    }
}
