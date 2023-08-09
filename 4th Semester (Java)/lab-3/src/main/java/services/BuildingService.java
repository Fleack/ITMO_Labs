package services;

import entities.Building;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import repositories.BuildingCrudRepository;
import tools.ControllerException;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class BuildingService {
    private final BuildingCrudRepository repository;
    
    @Autowired
    public BuildingService(BuildingCrudRepository repository) {
        this.repository = repository;
    }

    public Building save(Building entity) {
        return repository.save(entity);
    }

    public void deleteById(long id) {
        repository.deleteById(id);
    }

    public void deleteByEntity(Building entity) {
        repository.delete(entity);
    }

    public void deleteAll() {
        repository.deleteAll();
    }

    public Building update(Building entity) {
        return repository.save(entity);
    }

    public Building getById(long id) throws ControllerException {
        Optional<Building> entity = repository.findById(id);

        if(entity.isEmpty())
            throw new ControllerException("No entity with given id");

        return entity.get();
    }

    public List<Building> getAll() {
        List<Building> list = new ArrayList<>();
        repository.findAll().forEach(list::add);
        return list;
    }

    public List<Building> getAllByVId(long id) {
        return repository.findBuildingsByStreet(id);
    }

    public List<Building> getAllByName(String name) {
        return repository.findAllByName(name);
    }
}
