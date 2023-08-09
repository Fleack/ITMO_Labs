package kafka;

import dto.StreetDTO;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Component;

@Component
public class StreetConsumer {
    private static final String TOPIC = "streets";

    private final StreetRepository streetRepository;

    private Street result;

    public StreetConsumer(StreetRepository repository) {
        this.streetRepository = repository;
    }

    @KafkaListener(topics = TOPIC, groupId = "street-group")
    public void consumeStreet(StreetDTO dto) {
        System.out.println("New consume: " + dto.toString());
        if ("create".equals(dto.getOperation())) {
            Building building = new Apartment(dto.getId(), dto.getAddress(), dto.getPrice());
            result = streetRepository.save(building);
        } else if ("update".equals(dto.getOperation())) {
            Optional<Street> optionalStreet = streetRepository.findById(dto.getId());
            if (optionalStreet.isPresent()) {
                result = streetRepository.save(apartment);
            }
        } else if ("delete".equals(dto.getOperation())) {
            result = streetRepository.deleteById(dto.getId());
        } else if ("getById".equals(dto.getOperation())) {
            result = streetRepository.getById(dto.getId());
        } else {
            result = null;
        }
    }

    public Street getResult() {
        return result;
    }
}
