package services;

import entities.Apartment;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import repositories.ApartmentCrudRepository;
import tools.ControllerException;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class ApartmentService {
    private final ApartmentCrudRepository repository;

    @Autowired
    public ApartmentService(ApartmentCrudRepository repository) {
        this.repository = repository;
    }

    public Apartment save(Apartment entity) {
        return repository.save(entity);
    }

    public void deleteById(long id) {
        repository.deleteById(id);
    }

    public void deleteByEntity(Apartment entity) {
        repository.delete(entity);
    }

    public void deleteAll() {
        repository.deleteAll();
    }

    public Apartment update(Apartment entity) {
        return repository.save(entity);
    }

    public Apartment getById(long id) throws ControllerException {
        Optional<Apartment> entity = repository.findById(id);

        if(entity.isEmpty())
            throw new ControllerException("No entity with given id");

        return entity.get();
    }

    public List<Apartment> getAll() {
        List<Apartment> list = new ArrayList<>();
        repository.findAll().forEach(list::add);
        return list;
    }

    public List<Apartment> getAllByVId(long id) {
        return repository.findApartmentsByBuilding(id);
    }
}
