package controllers;

import entities.Street;
import entities.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.context.SecurityContextHolder;
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
    public Street save(@RequestBody Street entity) {
        return service.save(entity);
    }

    @PutMapping("/update")
    @PreAuthorize("hasAuthority('ADMIN')")
    public Street update(@RequestBody Street entity) {
        return service.update(entity);
    }

    @GetMapping("/getById/{id}")
    @PreAuthorize("hasAuthority('ADMIN')")
    public Street getById(@PathVariable("id") long id) throws ControllerException {
        return service.getById(id);
    }

    @DeleteMapping("/deleteById/{id}")
    @PreAuthorize("hasAuthority('ADMIN')")
    public void deleteById(@PathVariable("id") long id) {
        service.deleteById(id);
    }

    @DeleteMapping("/deleteByEntity")
    @PreAuthorize("hasAuthority('ADMIN')")
    public void deleteByEntity(@RequestBody Street entity) {
        service.deleteByEntity(entity);
    }

    @DeleteMapping("/deleteAll")
    @PreAuthorize("hasAuthority('ADMIN')")
    public void deleteAll() {
        service.deleteAll();
    }

    @GetMapping("/getAll")
    @PreAuthorize("hasAuthority('ADMIN')")
    public List<Street> getAll() {
        return service.getAll();
    }

    @GetMapping("/getAllByName/{name}")
    @PreAuthorize("hasAuthority('ADMIN')")
    public List<Street> getAllByVId(@PathVariable("name") String name) {
        return service.getAllByName(name);
    }
}
