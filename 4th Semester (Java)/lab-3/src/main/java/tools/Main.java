package tools;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@SpringBootApplication(scanBasePackages = {"controllers", "services", "configs", "security"})
@EnableJpaRepositories(basePackages = "repositories")
@EntityScan(basePackages = "entities")
public class Main {
    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);
    }
}
