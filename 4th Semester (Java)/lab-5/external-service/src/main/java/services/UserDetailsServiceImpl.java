package services;

import entities.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;
import repositories.UserCrudRepository;
import security.SecurityUser;
import tools.ControllerException;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service("userDetailsServiceImpl")
public class UserDetailsServiceImpl implements UserDetailsService {
    @Autowired
    private UserCrudRepository repository;

    @Override
    public UserDetails loadUserByUsername(String login) throws UsernameNotFoundException {
        Optional<User> user = repository.findByLogin(login);
        if (user.isEmpty()) {
            throw new UsernameNotFoundException("User with given login doesn't exist");
        }
        return new SecurityUser(user.get());
    }

    public User save(User entity) {
        return repository.save(entity);
    }

    public void deleteById(long id) {
        repository.deleteById(id);
    }

    public void deleteByEntity(User entity) {
        repository.delete(entity);
    }

    public void deleteAll() {
        repository.deleteAll();
    }

    public User update(User entity) {
        return repository.save(entity);
    }

    public User getById(long id) throws ControllerException {
        Optional<User> entity = repository.findById(id);

        if(entity.isEmpty())
            throw new ControllerException("No entity with given id");

        return entity.get();
    }

    public User getByLogin(String login) throws ControllerException {
        Optional<User> user = repository.findByLogin(login);
        if(user.isEmpty()) {
            throw new ControllerException("No entity with given login");
        }
        return user.get();
    }

    public List<User> getAll() {
        List<User> list = new ArrayList<>();
        repository.findAll().forEach(list::add);
        return list;
    }
}
