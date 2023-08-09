package controllers;

import dto.BuildingDTO;
import entities.Building;
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
import services.BuildingService;
import services.UserDetailsServiceImpl;
import tools.ControllerException;

import java.util.List;

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
    public BuildingDTO save(@RequestBody Building entity) {
        BuildingDTO building = service.getBuildingById(entity.getId());
        return service.save(building);
    }

    @PutMapping("/update")
    public BuildingDTO update(@RequestBody Building entity) throws ControllerException {
        BuildingDTO building = service.getBuildingById(entity.getId());
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            service.update(building);
        }
        return building;
    }

    @GetMapping("/getById/{id}")
    public BuildingDTO getById(@PathVariable("id") long id) throws ControllerException {
        BuildingDTO building = service.getById(id);
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            return building;
        }
        return null;
    }

    @DeleteMapping("/deleteById/{id}")
    public void deleteById(@PathVariable("id") long id) throws ControllerException {
        BuildingDTO building = service.getById(id);
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            service.deleteBuilding(id);
        }
    }

    @DeleteMapping("/deleteByEntity")
    public void deleteByEntity(@RequestBody Building entity) throws ControllerException {
        BuildingDTO building = service.getById(entity.getId());
        long streetId = building.getStreetId();
        if (streetId == getUserStreetId() || isSessionUserAdmin()) {
            service.deleteBuilding(entity.getId());
        }
    }

    @GetMapping("/getAll")
    @PreAuthorize("hasAuthority('ADMIN')")
    public List<BuildingDTO> getAll() {
        return service.getAllBuildings();
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
