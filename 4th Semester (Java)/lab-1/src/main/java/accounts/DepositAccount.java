package accounts;

import client.Client;
import exceptions.BanksException;

import java.time.Duration;
import java.util.UUID;

/**
 * An account that allows you to open a deposit for a certain period with a profit after the deposit expires
 */
public class DepositAccount extends Account {
    private final double MinMoneyAmount = 0;
    private final double MinDepositMoney = 0;
    private final double MinDepositInterest = 0;
    private final double MinCommission = 0;
    private final double DaysInAYear = 365;

    public Boolean getOnDeposit() {
        return onDeposit;
    }

    /**
     * The period for which the deposit is opened.
     */
    private final Duration depositPeriod;

    /**
     * The number of days during which the deposit is open.
     */
    private Duration depositTime;

    /**
     * The amount of money accumulated in the deposit account.
     */
    private double depositMoney;

    /**
     * Early interest on the deposit account.
     */
    private final double depositInterest;

    /**
     * Daily interest on the deposit account.
     */
    private final double dailyDepositInterest;

    /**
     * Flag if deposit is still opened.
     */
    private Boolean onDeposit;

    /**
     * DepositAccount constructor.
     * @param client the client who owns the account
     * @param depositPeriod the period for which the deposit is made
     * @param depositMoney the amount of money that is deposited
     * @param depositInterest interest on the deposit
     * @throws BanksException if client is null or depositInterest is negative or depositMoney is negative
     */
    public DepositAccount(Client client, Duration depositPeriod, double depositMoney, double depositInterest) throws BanksException {
        if (client == null){
            throw new BanksException("Failed to construct client, given value: client  can not be null");
        }

        if (depositInterest <= MinDepositInterest) {
            throw new BanksException("Failed to construct client, given value: depositInterest can not be less or equal " + "MinDepositInterest");
        }

        if (depositMoney <= MinDepositMoney) {
            throw new BanksException("Failed to construct client, given value: depositMoney can not be less or equal " + "MinDepositMoney");
        }

        Id = UUID.randomUUID();
        this.client = client;
        this.depositMoney = depositMoney;
        this.depositInterest = depositInterest;
        this.dailyDepositInterest = depositInterest / DaysInAYear;
        this.depositPeriod = depositPeriod;
        this.depositTime = Duration.ZERO;
        this.onDeposit = true;
        this.money = MinMoneyAmount;
        this.commission = MinCommission;
    }

    /**
     * @see Account#replenish(double)
     * @param amount amount of money you want to replenish
     * @throws BanksException if the deposit period has not ended or amount of money you want to replenish is negative
     */
    public void replenish(double amount) throws BanksException {
        if (!onDeposit) {
            throw new BanksException("Failed to Replenish account, forbidden to replenish from DepositAccount if it's not on deposit");
        }

        if (amount <= MinMoneyAmount) {
            throw new BanksException("Failed to Replenish account, given value: amount can not be <= " + "MinMoneyAmount");
        }

        money += amount;
    }

    /**
     * @see Account#withdraw(double)
     * @param amount amount of money you want to withdraw
     * @throws BanksException if the deposit period has ended or amount of money you want to withdraw is negative or bigger then you have now
     */
    public void withdraw(double amount) throws BanksException {
        if (onDeposit) {
            throw new BanksException("Failed to Withdraw account, forbidden to withdraw from DepositAccount if it's still on deposit");
        }

        if (amount <= MinMoneyAmount) {
            throw new BanksException("Failed to Withdraw account, given value: amount can not be <= " + "MinMoneyAmount");
        }

        if (money - amount < MinMoneyAmount) {
            throw new BanksException("Failed to Withdraw from account. Account doesn't have enough money");
        }

        money -= amount;
    }

    /**
     * Adds daily interest if deposit period has not ended
     * @see Account#addDailyCommission()
     */
    public void addDailyCommission() {
        if (!onDeposit) return;

        money += depositMoney * dailyDepositInterest;
        increaseDepositTime();
    }

    /**
     * Throws exception.
     * @throws BanksException always, because impossible to add monthly interest, deposit account has only daily interest
     */
    public void addMonthlyCommission() throws BanksException {
        throw new BanksException("Impossible to addMonthlyCommission to DepositAccount!");
    }

    /**
     * Adds money saved during the deposit and closes it.
     */
    private void closeDepositAccount() {
        money += depositMoney;
        depositMoney = MinMoneyAmount;
        onDeposit = false;
    }

    /**
     * Adds one day to period of deposit being opened and trying to close it.
     */
    private void increaseDepositTime() {
        depositTime = depositTime.plusDays(1);
        tryToCloseDepositAccount();
    }

    /**
     * Trying to close deposit if it's possible.
     * @throws BanksException if deposit is already closed, or it's not the day it has to be closed
     */
    private void tryToCloseDepositAccount() throws BanksException {
        if (!onDeposit) {
            throw new BanksException("Failed to CloseDebitAccount from account. DebitAccount already is closed");
        }

        if (depositTime.compareTo(depositPeriod) > 0) {
            throw new BanksException("Failed to TryToCloseDepositAccount. DepositTime is incorrect!");
        }

        if (depositTime == depositPeriod) {
            closeDepositAccount();
        }
    }

    public Duration getDepositPeriod() {
        return depositPeriod;
    }

    public Duration getDepositTime() {
        return depositTime;
    }

    public double getDepositMoney() {
        return depositMoney;
    }

    public double getDepositInterest() {
        return depositInterest;
    }

    public double getDailyDepositInterest() {
        return dailyDepositInterest;
    }
}
