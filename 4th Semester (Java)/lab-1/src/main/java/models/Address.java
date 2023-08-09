package models;
import exceptions.BanksException;

/**
 * Contains information about address.
 */
public class Address {
    /**
     * Country component of address.
     */
    private final String country;

    /**
     * City component of address.
     */
    private final String city;

    /**
     * Street component of address.
     */
    private final String street;

    /**
     * Number of building of address.
     */
    private final int buildingNumber;

    /**
     * Address constructor.
     * @param country country component of full address
     * @param city city component of full address
     * @param street street component of full address
     * @param buildingNumber number of the building component of full address
     * @throws BanksException if one of {country, city, street} params is empty
     */
    public Address(String country, String city, String street, int buildingNumber) throws BanksException {
        if (country.isEmpty()) {
            throw new BanksException("Failed to construct Address, country can not be null or empty");
        }

        if (city.isEmpty()) {
            throw new BanksException("Failed to construct Address, city can not be null or empty");
        }

        if (street.isEmpty()) {
            throw new BanksException("Failed to construct Address, street can not be null or empty");
        }

        this.country = country;
        this.city = city;
        this.street = street;
        this.buildingNumber = buildingNumber;
    }

    public String getCountry() {
        return country;
    }

    public String getCity() {
        return city;
    }

    public String getStreet() {
        return street;
    }

    public int getBuildingNumber() {
        return buildingNumber;
    }
}
