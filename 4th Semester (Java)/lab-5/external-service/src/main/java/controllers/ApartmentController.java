package controllers;

import dto.ApartmentDTO;
import entities.Apartment;
import org.springframework.beans.factory.annotation.Autowired;
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
    public ApartmentDTO save(@RequestBody Apartment entity) {
        ApartmentDTO apartmentDTO = service.getApartmentById(entity.getId());
        return service.save(apartmentDTO);
    }

    @PutMapping("/update")
    public ApartmentDTO update(@RequestBody Apartment entity) {
        return save(entity);
    }

    @GetMapping("/getById/{id}")
    public ApartmentDTO getById(@PathVariable("id") long id) throws ControllerException {
        return service.getById(id);
    }

    @DeleteMapping("/deleteById/{id}")
    public void deleteById(@PathVariable("id") long id) {
        service.deleteApartment(id);
    }

    @DeleteMapping("/deleteByEntity")
    public void deleteByEntity(@RequestBody Apartment entity) {
        service.deleteApartment(entity.getId());
    }

    @GetMapping("/getAll")
    public List<ApartmentDTO> getAll() {
        return service.getAllApartments();
    }
}
