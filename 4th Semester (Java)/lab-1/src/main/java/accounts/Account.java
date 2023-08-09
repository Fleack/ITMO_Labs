package accounts;

import java.util.UUID;

import client.Client;
import exceptions.BanksException;

/**
 * Basic logic of bank's account.
 */
public abstract class Account {
    /**
     * Unique account ID.
     */
    protected UUID Id;

    /**
     * The client who owns the account.
     */
    protected Client client;

    /**
     * The commission charged to the account when making transactions.
     */
    protected double commission;

    /**
     * The amount of money in the account.
     */
    protected double money;

    /**
     * Method to replenish amount of money to account.
     * @param amount amount of money you want to replenish
     */
    public abstract void replenish(double amount);

    /**
     * Method to withdraw amount of money from account.
     * @param amount amount of money you want to withdraw
     */
    public abstract void withdraw(double amount);

    /**
     * Adds daily commission to account, if required by account type.
     */
    public abstract void addDailyCommission();

    /**
     * Adds monthly commission to account, if required by account type.
     */
    public abstract void addMonthlyCommission();

    /**
     * Sets money amount to account.
     * @param money amount of money you want to set
     * @throws BanksException if money amount is negative
     */
    public void setMoney(double money) throws BanksException {
        if (money < 0)
            throw new BanksException("Failed to setMoney. Given value money has to be a not negative number");
        this.money = money;
    }

    public UUID getId() {
        return Id;
    }

    public Client getClient() {
        return client;
    }

    public double getCommission() {
        return commission;
    }

    public double getMoney() {
        return money;
    }
}
