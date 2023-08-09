package accounts;

import client.Client;
import exceptions.BanksException;

import java.util.UUID;

/**
 * An account that allows you to take money on credit with the ability to go into the negative on the account
 */
public class CreditAccount extends Account {
    private final double MinMoneyAmount = 0;
    private final double MinLoanCommission = 0;

    /**
     * CreditAccount constructor.
     * @param client the client who owns the account
     * @param commission the commission charged to the account when making transactions
     * @throws BanksException if client param is null or commission is negative
     */
    public CreditAccount(Client client, double commission) throws BanksException {
        if (client == null) {
            throw new BanksException("Failed to construct client, given value client can not be null");
        }

        if (commission <= MinLoanCommission) {
            throw new BanksException("Failed to construct client, given value commission can not be less or equal" + "MinLoanCommission");
        }

        Id = UUID.randomUUID();
        this.client = client;
        this.commission = commission;
        this.money = 0;
    }

    /**
     * @see Account#withdraw(double)
     * @param amount amount of money you want to replenish
     * @throws BanksException if amount param is negative
     */
    public void replenish(double amount) throws BanksException {
        if (amount <= MinMoneyAmount) {
            throw new BanksException("Failed to Withdraw, given value amount can not be <= " + "MinMoneyAmount");
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
            throw new BanksException("Failed to Replenish, given value amount can not be <= " + "MinMoneyAmount");
        }

        if (money - amount < MinMoneyAmount) {
            money -= commission;
        }

        money -= amount;
    }

    /**
     * @see Account#addDailyCommission()
     * @throws BanksException always, because credit account can not have daily commission
     */
    public void addDailyCommission() throws BanksException {
        throw new BanksException("Impossible to addDailyCommission to CreditAccount!");
    }

    /**
     * @see Account#addMonthlyCommission()
     * @throws BanksException always, because credit account can not have daily commission
     */
    public void addMonthlyCommission() throws BanksException {
        throw new BanksException("Impossible to addMonthlyCommission to CreditAccount!");
    }
}
