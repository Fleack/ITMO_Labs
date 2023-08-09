package controllers;

import entities.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import services.UserDetailsServiceImpl;
import tools.ControllerException;

import java.util.List;

@RestController
@RequestMapping("/users")
public class UserController {
    private final UserDetailsServiceImpl service;

    private final PasswordEncoder passwordEncoder;

    @Autowired
    public UserController(UserDetailsServiceImpl service, PasswordEncoder passwordEncoder) {
        this.service = service;
        this.passwordEncoder = passwordEncoder;
    }

    @PostMapping("/save")
    public User save(@RequestBody User entity) {
        entity.setPassword(passwordEncoder.encode(entity.getPassword()));
        return service.save(entity);
    }

    @PostMapping("/deleteById/{id}")
    public void deleteById(@PathVariable("id") long id) {
        service.deleteById(id);
    }

    @PostMapping("/deleteByEntity")
    public void deleteByEntity(@RequestBody User entity) {
        service.deleteByEntity(entity);
    }

    @PostMapping("/deleteAll")
    public void deleteAll() {
        service.deleteAll();
    }

    @PostMapping("/update")
    public User update(@RequestBody User entity) {
        entity.setPassword(passwordEncoder.encode(entity.getPassword()));
        return service.update(entity);
    }

    @GetMapping("/getById/{id}")
    public User getById(@PathVariable("id") long id) throws ControllerException {
        return service.getById(id);
    }

    @GetMapping("/getByLogin/{login}")
    public User getByLogin(@PathVariable("login") String login) throws ControllerException {
        return service.getByLogin(login);
    }

    @GetMapping("/getAll")
    public List<User> getAll() {
        return service.getAll();
    }
}
