package kafka;

import dto.ApartmentDTO;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Component;

@Component
public class ApartmentConsumer {
    private static final String TOPIC = "apartments";

    private final ApartmentRepository apartmentRepository;

    private Apartment result;

    @Autowired
    public ApartmentConsumer(ApartmentRepository apartmentRepository) {
        this.apartmentRepository = apartmentRepository;
    }

    @KafkaListener(topics = TOPIC, groupId = "apartment-group")
    public void consumeApartment(ApartmentDTO dto) {
        System.out.println("New consume: " + dto.toString());
        if ("create".equals(dto.getOperation())) {
            Apartment apartment = new Apartment(dto.getId(), dto.getAddress());
            result = apartmentRepository.save(apartment);
        } else if ("update".equals(dto.getOperation())) {
            Optional<Apartment> optionalApartment = apartmentRepository.findById(dto.getId());
            if (optionalApartment.isPresent()) {
                Apartment apartment = optionalApartment.get();
                result = apartmentRepository.save(apartment);
            } else {
                result = null;
            }
        } else if ("delete".equals(dto.getOperation())) {
            result = apartmentRepository.deleteById(dto.getId());
        } else if ("getById".equals(dto.getOperation())) {
            result = apartmentRepository.getById(dto.getId());
        } else {
            result = null;
        }
    }

    public Apartment getResult() {
        return result;
    }
}
