package controllers;

import entities.Building;
import entities.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
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
import services.BuildingService;
import services.UserDetailsServiceImpl;
import tools.ControllerException;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/buildings")
@PreAuthorize("hasAnyRole('ADMIN','STREET')")
public class BuildingController {
    private final BuildingService service;

    private final UserDetailsServiceImpl userDetailsService;

    @Autowired
    public BuildingController(BuildingService service, UserDetailsServiceImpl userDetailsService) {
        this.service = service;
        this.userDetailsService = userDetailsService;
    }

    @PostMapping("/save")
    @PreAuthorize("hasAuthority('ADMIN')")
    public Building save(@RequestBody Building entity) {
        return service.save(entity);
    }

    @PutMapping("/update")
    @PreAuthorize("hasAuthority('ADMIN')")
    public Building update(@RequestBody Building entity) throws ControllerException {
        Building building = service.getById(entity.getId());
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            service.update(entity);
        }
        return entity;
    }

    @GetMapping("/getById/{id}")
    public Building getById(@PathVariable("id") long id) throws ControllerException {
        Building building = service.getById(id);
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            return building;
        }
        return null;
    }

    @DeleteMapping("/deleteById/{id}")
    public void deleteById(@PathVariable("id") long id) throws ControllerException {
        Building building = service.getById(id);
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            service.deleteById(id);
        }
    }

    @DeleteMapping("/deleteByEntity")
    @PreAuthorize("hasAuthority('ADMIN')")
    public void deleteByEntity(@RequestBody Building entity) throws ControllerException {
        Building building = service.getById(entity.getId());
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            service.deleteById(entity.getId());
        }
    }

    @DeleteMapping("/deleteAll")
    @PreAuthorize("hasAuthority('ADMIN')")
    public void deleteAll() {
        service.deleteAll();
    }

    @GetMapping("/getAll")
    @PreAuthorize("hasAuthority('ADMIN')")
    public List<Building> getAll() {
        return service.getAll();
    }

    @GetMapping("/getAllByVId/{id}")
    @PreAuthorize("hasAuthority('ADMIN')")
    public List<Building> getAllByVId(@PathVariable("id") long id) {
        return service.getAllByVId(id);
    }

    @GetMapping("/getAllByName/{name}")
    @PreAuthorize("hasAuthority('ADMIN')")
    public List<Building> getAllByVId(@PathVariable("name") String name) {
        return service.getAllByName(name);
    }

    private boolean isSessionUserAdmin() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        return authentication.getAuthorities().contains(new SimpleGrantedAuthority("ADMIN"));
    }

    private long getUserStreetId() throws ControllerException {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        String username = authentication.getName();
        User user = userDetailsService.getByLogin(username);
        return user.getStreet_id();
    }
}
