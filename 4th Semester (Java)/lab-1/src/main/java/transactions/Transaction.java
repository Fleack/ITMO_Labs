package transactions;

import accounts.Account;

import java.time.LocalDateTime;
import java.util.UUID;

/**
 * Basic transaction logic.
 */
public abstract class Transaction {
    /**
     * Unique id of transaction.
     */
    protected UUID Id;

    /**
     * The account from which the transaction was made.
     */
    protected Account account;

    /**
     * The amount of money processed in the transaction.
     */
    protected double moneyAmount;

    /**
     * The commission was taken from user.
     */
    protected double commission;

    /**
     * Date and time of transaction.
     */
    protected LocalDateTime dateTime;

    /**
     * Flag if transaction was canceled.
     */
    protected Boolean canceled;

    /**
     * Canceling transaction.
     */
    public abstract void cancel();

    public UUID getId() {
        return Id;
    }

    public accounts.Account getAccount() {
        return account;
    }

    public double getMoneyAmount() {
        return moneyAmount;
    }

    public double getCommission() {
        return commission;
    }

    public LocalDateTime getDateTime() {
        return dateTime;
    }

    public Boolean getCanceled() {
        return canceled;
    }
}
