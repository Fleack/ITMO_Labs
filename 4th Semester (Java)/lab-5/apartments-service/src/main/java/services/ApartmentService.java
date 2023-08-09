package services;

import dto.ApartmentDTO;
import entities.Apartment;
import kafka.ApartmentProducer;
import repositories.ApartmentCrudRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Service
public class ApartmentService {
    private final ApartmentCrudRepository apartmentRepository;
    private final ApartmentProducer apartmentProducer;

    @Autowired
    public ApartmentService(ApartmentCrudRepository apartmentRepository, ApartmentProducer apartmentProducer) {
        this.apartmentRepository = apartmentRepository;
        this.apartmentProducer = apartmentProducer;
    }

    public List<ApartmentDTO> getAllApartments() {
        List<Apartment> apartments = StreamSupport.stream(
                apartmentRepository.findAll().spliterator(), false)
                .toList();
        return apartments.stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    public ApartmentDTO getApartmentById(Long id) {
        Optional<Apartment> apartmentOptional = apartmentRepository.findById(id);
        if (apartmentOptional.isPresent()) {
            Apartment apartment = apartmentOptional.get();
            return convertToDTO(apartment);
        }
        return null;
    }

    public ApartmentDTO createApartment(ApartmentDTO apartmentDTO) {
        Apartment apartment = convertToEntity(apartmentDTO);
        Apartment savedApartment = apartmentRepository.save(apartment);
        ApartmentDTO savedApartmentDTO = convertToDTO(savedApartment);
        apartmentProducer.sendApartment(savedApartmentDTO);
        return savedApartmentDTO;
    }

    public ApartmentDTO save(ApartmentDTO apartmentDTO) {
        Apartment apartment = convertToEntity(apartmentDTO);
        apartmentRepository.save(apartment);
        return apartmentDTO;
    }

    public ApartmentDTO update(ApartmentDTO apartmentDTO) {
        return save(apartmentDTO);
    }

    public ApartmentDTO getById(long id) {
        Optional<Apartment> temp = apartmentRepository.findById(id);
        if (temp.isEmpty()) {
            throw new RuntimeException("Failed to find");
        }

        return convertToDTO(temp.get());
    }

    public void deleteApartment(Long id) {
        apartmentRepository.deleteById(id);
    }

    private ApartmentDTO convertToDTO(Apartment apartment) {
        ApartmentDTO apartmentDTO = new ApartmentDTO();
        apartmentDTO.setId(apartment.getId());
        apartmentDTO.setNumber(apartment.getNumber());
        return apartmentDTO;
    }

    private Apartment convertToEntity(ApartmentDTO apartmentDTO) {
        Apartment apartment = new Apartment();
        apartment.setId(apartmentDTO.getId());
        apartment.setNumber(apartmentDTO.getNumber());
        return apartment;
    }
}
