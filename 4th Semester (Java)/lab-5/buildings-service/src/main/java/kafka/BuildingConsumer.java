package kafka;

import dto.BuildingDTO;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Component;

@Component
public class BuildingConsumer {
    private static final String TOPIC = "buildings";
    private final BuildingRepository buildingRepository;

    private Building result;
    
    public BuildingConsumer(BuildingRepository repository) {
        this.buildingRepository = repository;
    }
    
    @KafkaListener(topics = TOPIC, groupId = "building-group")
    public void consumeBuilding(BuildingDTO dto) {
        System.out.println("New consume: " + dto.toString());
        if ("create".equals(dto.getOperation())) {
            Building building = new Apartment(dto.getId(), dto.getAddress(), dto.getPrice());
            result = buildingRepository.save(building);
        } else if ("update".equals(dto.getOperation())) {
            Optional<Building> optionalBuilding = buildingRepository.findById(dto.getId());
            if (optionalBuilding.isPresent()) {
                result = buildingRepository.save(apartment);
            }
        } else if ("delete".equals(dto.getOperation())) {
            result = buildingRepository.deleteById(dto.getId());
        } else if ("getById".equals(dto.getOperation())) {
            result = buildingRepository.getById(dto.getId());
        } else {
            result = null;
        }
    }

    public Building getResult() {
        return result;
    }
}
