package models;

import bank.Bank;
import exceptions.BanksException;

/**
 * Contains information about name and surname.
 */
public class FullName {
    /**
     * Name of person.
     */
    private final String name;

    /**
     * Surname of person.
     */
    private final String surname;

    /**
     * FullName constructor.
     * @param name name of the person
     * @param surname surname of the person
     * @throws BanksException if surname or/and name is empty
     */
    public FullName(String name, String surname) throws BanksException {
        if (name.isEmpty()) {
            throw new BanksException("Given value name can not be null or whitespace");
        }

        if (surname.isEmpty()) {
            throw new BanksException("Given value surname can not be null or whitespace");
        }

        this.name = name;
        this.surname = surname;
    }

    public String getName() {
        return name;
    }

    public String getSurname() {
        return surname;
    }
}
