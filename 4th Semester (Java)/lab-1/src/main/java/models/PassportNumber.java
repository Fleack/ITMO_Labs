package models;

import exceptions.BanksException;

/**
 * Contains information about number of passport.
 */
public class PassportNumber {
    private final int MinPassportsNumberNumber = 100000;
    private final int MaxPassportsNumberNumber = 999999;

    /**
     * Passport number.
     */
    private final int number;

    /**
     * PassportNumber constructor.
     * @param number passport number
     * @throws BanksException if given passport number is not in range of [100000; 999999]
     */
    public PassportNumber(int number) throws BanksException {
        if (number < MinPassportsNumberNumber || number > MaxPassportsNumberNumber) {
            throw new BanksException("Given value series has to be between " + MinPassportsNumberNumber + " and " + MaxPassportsNumberNumber);
        }

        this.number = number;
    }

    public int getNumber() {
        return number;
    }
}
