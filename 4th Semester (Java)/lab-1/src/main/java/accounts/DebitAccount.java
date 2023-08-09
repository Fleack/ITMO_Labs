package accounts;

import client.Client;
import exceptions.BanksException;

import java.util.UUID;

/**
 * A regular account that allows you to make transactions without commission
 */
public class DebitAccount extends Account {
    private final double MinCommission = 0;
    private final double MinMoneyAmount = 0;
    private final double DaysInAYear = 365;

    /**
     * The amount of money accumulated since the beginning of the month.
     */
    private double savedMoney;

    /**
     * The multiplier of the early increase of money.
     */
    private final double interest;

    /**
     * The multiplier of the daily increase of money.
     */
    private final double dailyInterest;

    /**
     * DebitAccount constructor.
     * @param client the client who owns the account
     * @param interest the interest charged to the account every period of time
     * @throws BanksException if client param is null or interest is negative
     */
    public DebitAccount(Client client, double interest) throws BanksException {
        if (client == null) {
            throw new BanksException("Failed to construct DebitAccount, given value: client  can not be null");
        }

        if (interest <= MinCommission) {
            throw new BanksException("Failed to construct DebitAccount, given value: interest {interest}  can not be <= {MinCommission}");
        }

        Id = UUID.randomUUID();
        this.client = client;
        this.money = MinMoneyAmount;
        this.savedMoney = MinMoneyAmount;
        this.commission = MinCommission;
        this.interest = interest;
        this.dailyInterest = interest / DaysInAYear;
    }

    /**
     * @see Account#replenish(double)
     * @param amount amount of money you want to replenish
     * @throws BanksException if amount param is negative
     */
    public void replenish(double amount) throws BanksException {
        if (amount <= MinMoneyAmount) {
            throw new BanksException("Failed to Replenish account, given value amount  can not be <= " + "MinMoneyAmount");
        }

        money += amount;
    }

    /**
     * @see Account#withdraw(double)
     * @param amount amount of money you want to withdraw
     * @throws BanksException if amount param is negative
     */
    public void withdraw(double amount) throws BanksException {
        if (amount <= MinMoneyAmount) {
            throw new BanksException("Failed to Withdraw, given value amount can not be <= " + "MinMoneyAmount");
        }

        if (money - amount < MinMoneyAmount) {
            throw new BanksException("Failed to Withdraw. Account doesn't have enough money");
        }

        money -= amount;
    }

    /**
     * Every day adds some amount of money to saved money.
     * @see Account#addDailyCommission()
     */
    public void addDailyCommission() {
        savedMoney += money * dailyInterest;
    }

    /**
     * Each month adds the money accumulated in saved money for the month.
     * @see Account#addMonthlyCommission()
     */
    public void addMonthlyCommission() {
        money += savedMoney;
        savedMoney = 0;
    }

    public double getSavedMoney() {
        return savedMoney;
    }

    public double getInterest() {
        return interest;
    }

    public double getDailyInterest () {
        return dailyInterest;
    }
}
