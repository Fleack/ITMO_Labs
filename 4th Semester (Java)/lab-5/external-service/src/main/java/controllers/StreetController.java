package controllers;

import dto.StreetDTO;
import entities.Street;
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
import services.StreetService;
import tools.ControllerException;

import java.util.List;

@RestController
@RequestMapping("/streets")
public class StreetController {
    private final StreetService service;

    @Autowired
    public StreetController(StreetService service) {
        this.service = service;
    }

    @PostMapping("/save")
    @PreAuthorize("hasAuthority('ADMIN')")
    public StreetDTO save(@RequestBody Street entity) {
        StreetDTO streetDTO = service.getStreetById(entity.getId());
        return service.save(streetDTO);
    }

    @PutMapping("/update")
    @PreAuthorize("hasAuthority('ADMIN')")
    public StreetDTO update(@RequestBody Street entity) {
        return save(entity);
    }

    @GetMapping("/getById/{id}")
    @PreAuthorize("hasAuthority('ADMIN')")
    public StreetDTO getById(@PathVariable("id") long id) throws ControllerException {
        return service.getById(id);
    }

    @DeleteMapping("/deleteById/{id}")
    @PreAuthorize("hasAuthority('ADMIN')")
    public void deleteById(@PathVariable("id") long id) {
        service.deleteStreet(id);
    }

    @DeleteMapping("/deleteByEntity")
    @PreAuthorize("hasAuthority('ADMIN')")
    public void deleteByEntity(@RequestBody Street entity) {
        service.deleteStreet(entity.getId());
    }

    @GetMapping("/getAll")
    @PreAuthorize("hasAuthority('ADMIN')")
    public List<StreetDTO> getAll() {
        return service.getAllStreets();
    }
}
