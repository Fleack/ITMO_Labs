package kafka;

import dto.ApartmentDTO;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.stereotype.Component;

@Component
public class ApartmentProducer {
    private static final String TOPIC = "apartments";

    @Autowired
    private KafkaTemplate<String, ApartmentDTO> kafkaTemplate;

    public void sendApartment(ApartmentDTO apartment) {
        kafkaTemplate.send(TOPIC, apartment);
    }
}
