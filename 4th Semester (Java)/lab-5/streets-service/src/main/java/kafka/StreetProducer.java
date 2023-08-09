package kafka;

import dto.StreetDTO;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.stereotype.Component;

@Component
public class StreetProducer {
    private static final String TOPIC = "streets";

    @Autowired
    private KafkaTemplate<String, StreetDTO> kafkaTemplate;

    public void sendStreet(StreetDTO street) {
        kafkaTemplate.send(TOPIC, street);
    }
}