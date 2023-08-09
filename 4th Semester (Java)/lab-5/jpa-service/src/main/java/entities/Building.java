package entities;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.EnumType;
import jakarta.persistence.Enumerated;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.Table;
import jakarta.persistence.Temporal;
import jakarta.persistence.TemporalType;
import tools.BuildingType;

import java.sql.Date;

@Entity
@Table(name = "buildings")
public class Building {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long Id;

    @Column(name = "Name")
    private String name;

    @Temporal(TemporalType.DATE)
    @Column(name = "Building_date")
    private Date buildingDate;

    @Column(name = "Floors_amount")
    private Integer floorsAmount;

    @Enumerated(EnumType.STRING)
    @Column(name = "Building_type")
    private BuildingType buildingType;

    @JoinColumn(name = "Street")
    private Long street;

    public Building() {}

    public Building(String name, Date buildingDate, int floorsAmount, BuildingType buildingType, long street) {
        this.name = name;
        this.buildingDate = buildingDate;
        this.floorsAmount = floorsAmount;
        this.buildingType = buildingType;
        this.street = street;
    }

    public Building(long Id, String name, Date buildingDate, int floorsAmount, BuildingType buildingType, long street) {
        this(name, buildingDate, floorsAmount, buildingType, street);
        this.Id = Id;
    }

    public void setId(long id) {
        this.Id = id;
    }

    public long getId() {
        return Id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Date getBuildingDate() {
        return buildingDate;
    }

    public void setBuildingDate(Date buildingDate) {
        this.buildingDate = buildingDate;
    }

    public int getFloorsAmount() {
        return floorsAmount;
    }

    public void setFloorsAmount(int floorsAmount) {
        this.floorsAmount = floorsAmount;
    }

    public BuildingType getBuildingType() {
        return buildingType;
    }

    public void setBuildingType(BuildingType buildingType) {
        this.buildingType = buildingType;
    }

    public long getStreetId() {
        return street;
    }

    public void setStreetId(long street) {
        this.street = street;
    }
}
