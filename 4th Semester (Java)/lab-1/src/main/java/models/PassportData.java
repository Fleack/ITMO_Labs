package models;
import exceptions.BanksException;

/**
 * Contains information about passport.
 */
public class PassportData {
    /**
     * Passport number.
     */
    private final PassportNumber number;

    /**
     * Passport series.
     */
    private final PassportSeries series;

    /**
     * PassportData constructor.
     * @param passportNumber number of passport
     * @param passportSeries series of passport
     * @throws BanksException if exception was thrown in PassportNumber constructor or PassportSeries constructor
     */
    public PassportData(int passportNumber, int passportSeries) throws BanksException {
        this.number = new PassportNumber(passportNumber);
        this.series = new PassportSeries(passportSeries);
    }

    public PassportNumber getNumber() {
        return this.number;
    }

    public PassportSeries getSeries() {
        return this.series;
    }
}
