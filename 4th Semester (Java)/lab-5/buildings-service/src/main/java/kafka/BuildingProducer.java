package kafka;

import dto.BuildingDTO;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.stereotype.Component;

@Component
public class BuildingProducer {
    private static final String TOPIC = "buildings";

    @Autowired
    private KafkaTemplate<String, BuildingDTO> kafkaTemplate;

    public void sendBuilding(BuildingDTO building) {
        kafkaTemplate.send(TOPIC, building);
    }
}
