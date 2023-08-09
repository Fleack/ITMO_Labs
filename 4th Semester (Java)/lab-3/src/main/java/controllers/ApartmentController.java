package controllers;

import entities.Apartment;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import services.ApartmentService;
import tools.ControllerException;

import java.util.List;

@RestController
@RequestMapping("/apartments")
public class ApartmentController {
    private final ApartmentService service;

    @Autowired
    public ApartmentController(ApartmentService service) {
        this.service = service;
    }

    @PostMapping("/save")
    public Apartment save(@RequestBody Apartment entity) {
        return service.save(entity);
    }

    @PutMapping("/update")
    public Apartment update(@RequestBody Apartment entity) {
        return service.update(entity);
    }

    @GetMapping("/getById/{id}")
    public Apartment getById(@PathVariable("id") long id) throws ControllerException {
        return service.getById(id);
    }

    @DeleteMapping("/deleteById/{id}")
    public void deleteById(@PathVariable("id") long id) {
        service.deleteById(id);
    }

    @DeleteMapping("/deleteByEntity")
    public void deleteByEntity(@RequestBody Apartment entity) {
        service.deleteByEntity(entity);
    }

    @DeleteMapping("/deleteAll")
    public void deleteAll() {
        service.deleteAll();
    }

    @GetMapping("/getAll")
    public List<Apartment> getAll() {
        return service.getAll();
    }

    @GetMapping("/getAllByVId/{id}")
    public List<Apartment> getAllByVId(@PathVariable("id") long id) {
        return service.getAllByVId(id);
    }
}
