package services;

import dto.StreetDTO;
import entities.Street;
import kafka.StreetProducer;
import repositories.StreetCrudRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Service
public class StreetService {
    private StreetCrudRepository streetRepository;
    private StreetProducer streetProducer;

    @Autowired
    public StreetService(StreetCrudRepository streetRepository, StreetProducer streetProducer) {
        this.streetRepository = streetRepository;
        this.streetProducer = streetProducer;
    }

    public List<StreetDTO> getAllStreets() {
        List<Street> streets = StreamSupport.stream(
                        streetRepository.findAll().spliterator(), false)
                .toList();
        return streets.stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    public StreetDTO getStreetById(Long id) {
        Optional<Street> streetOptional = streetRepository.findById(id);
        if (streetOptional.isPresent()) {
            Street street = streetOptional.get();
            return convertToDTO(street);
        }
        return null;
    }

    public StreetDTO createStreet(StreetDTO streetDTO) {
        Street street = convertToEntity(streetDTO);
        Street savedStreet = streetRepository.save(street);
        StreetDTO savedStreetDTO = convertToDTO(savedStreet);
        streetProducer.sendStreet(savedStreetDTO);
        return savedStreetDTO;
    }

    public void deleteStreet(Long id) {
        streetRepository.deleteById(id);
    }

    public StreetDTO save(StreetDTO streetDTO) {
        Street street = convertToEntity(streetDTO);
        streetRepository.save(street);
        return streetDTO;
    }

    public StreetDTO update(StreetDTO streetDTO) {
        return save(streetDTO);
    }

    public StreetDTO getById(long id) {
        Optional<Street> temp = streetRepository.findById(id);
        if (temp.isEmpty()) {
            throw new RuntimeException("Failed to find");
        }

        return convertToDTO(temp.get());
    }

    private StreetDTO convertToDTO(Street street) {
        StreetDTO streetDTO = new StreetDTO();
        streetDTO.setId(street.getId());
        streetDTO.setName(street.getName());
        return streetDTO;
    }

    private Street convertToEntity(StreetDTO streetDTO) {
        Street street = new Street();
        street.setId(streetDTO.getId());
        street.setName(streetDTO.getName());
        return street;
    }
}
