package dto;

import tools.BuildingType;

public class BuildingDTO {
    private Long id;
    private String name;
    private BuildingType type;
    private Long streetId;
    private String operation;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public BuildingType getType() {
        return type;
    }

    public void setType(BuildingType type) {
        this.type = type;
    }

    public Long getStreetId() {
        return streetId;
    }

    public void setStreetId(Long streetId) {
        this.streetId = streetId;
    }

    public String getOperation() {
        return operation;
    }

    public void setOperation(String operation) {
        this.operation = operation;
    }
}

