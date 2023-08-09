package entities;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "streets")
public class Street {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long Id;

    @Column(name = "Name")
    private String name;

    @Column(name = "Postal_code")
    private int postalCode;

    public Street() {}

    public Street (long id, String name, int postalCode)
    {
        this(name, postalCode);
        this.Id = id;
    }

    public Street (String name, int postalCode)
    {
        this.name = name;
        this.postalCode = postalCode;
    }

    public void setId(long id) {
        this.Id = id;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setPostalCode(int postalCode) {
        this.postalCode = postalCode;
    }

    public long getId() {
        return Id;
    }

    public String getName() {
        return name;
    }

    public int getPostalCode() {
        return postalCode;
    }

}
