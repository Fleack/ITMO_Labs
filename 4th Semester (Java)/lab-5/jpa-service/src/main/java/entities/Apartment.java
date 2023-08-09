package entities;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.Table;

@Entity
@Table(name = "apartments")
public class Apartment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long Id;

    @Column(name = "Number")
    private Integer number;

    @Column(name = "Area")
    private Integer area;

    @Column(name = "Rooms_amount")
    private Integer roomsAmount;

    @JoinColumn(name = "Building")
    private Long building;

    public Long getId() {
        return Id;
    }

    public void setId(Long id) {
        Id = id;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }

    public int getArea() {
        return area;
    }

    public void setArea(int area) {
        this.area = area;
    }

    public int getRoomsAmount() {
        return roomsAmount;
    }

    public void setRoomsAmount(int roomsAmount) {
        this.roomsAmount = roomsAmount;
    }

    public long getBuilding() {
        return building;
    }

    public void setBuilding(long building) {
        this.building = building;
    }
}
