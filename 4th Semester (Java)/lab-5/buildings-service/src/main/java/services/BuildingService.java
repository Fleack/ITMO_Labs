package services;

import dto.BuildingDTO;
import entities.Building;
import kafka.BuildingProducer;
import repositories.BuildingCrudRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Service
public class BuildingService {
    @Autowired
    private BuildingCrudRepository buildingRepository;

    @Autowired
    private BuildingProducer buildingProducer;

    @Autowired
    public BuildingService(BuildingCrudRepository buildingRepository, BuildingProducer buildingProducer) {
        this.buildingRepository = buildingRepository;
        this.buildingProducer = buildingProducer;
    }

    public List<BuildingDTO> getAllBuildings() {
        List<Building> apartments = StreamSupport.stream(
                        buildingRepository.findAll().spliterator(), false)
                .toList();
        return apartments.stream()
                .map(this::convertToDTO)
                .collect(Collectors.toList());
    }

    public BuildingDTO getBuildingById(Long id) {
        Optional<Building> buildingOptional = buildingRepository.findById(id);
        if (buildingOptional.isPresent()) {
            Building building = buildingOptional.get();
            return convertToDTO(building);
        }
        return null;
    }

    public BuildingDTO createBuilding(BuildingDTO buildingDTO) {
        Building building = convertToEntity(buildingDTO);
        Building savedBuilding = buildingRepository.save(building);
        BuildingDTO savedBuildingDTO = convertToDTO(savedBuilding);
        buildingProducer.sendBuilding(savedBuildingDTO);
        return savedBuildingDTO;
    }

    public BuildingDTO save(BuildingDTO buildingDTO) {
        Building building = convertToEntity(buildingDTO);
        buildingRepository.save(building);
        return buildingDTO;
    }

    public BuildingDTO getById(long id) {
        Optional<Building> temp = buildingRepository.findById(id);
        if (temp.isEmpty()) {
            throw new RuntimeException("Failed to find");
        }

        return convertToDTO(temp.get());
    }

    public BuildingDTO update(BuildingDTO buildingDTO) {
        return save(buildingDTO);
    }

    public void deleteBuilding(Long id) {
        buildingRepository.deleteById(id);
    }

    private BuildingDTO convertToDTO(Building building) {
        BuildingDTO buildingDTO = new BuildingDTO();
        buildingDTO.setId(building.getId());
        buildingDTO.setName(building.getName());
        return buildingDTO;
    }

    private Building convertToEntity(BuildingDTO buildingDTO) {
        Building building = new Building();
        building.setId(buildingDTO.getId());
        building.setName(buildingDTO.getName());
        return building;
    }
}
