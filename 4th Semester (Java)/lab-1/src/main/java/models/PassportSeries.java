package models;

import exceptions.BanksException;

/**
 * Contains information about series of passport.
 */
public class PassportSeries {
    private final int MinSeriesNumber = 1000;
    private final int MaxSeriesNumber = 9999;

    /**
     * Passport series.
     */
    private final int series;

    /**
     * PassportNumber constructor.
     * @param series passport series
     * @throws BanksException if given passport number is not in range of [1000; 9999]
     */
    public PassportSeries(int series) throws BanksException {
        if (series < MinSeriesNumber || series > MaxSeriesNumber) {
            throw new BanksException("Given value series has to be between " + MinSeriesNumber + " and " + MaxSeriesNumber);
        }

        this.series = series;
    }

    public int getSeries() {
        return series;
    }
}
