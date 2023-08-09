package transactions;

import accounts.Account;
import exceptions.BanksException;

import java.time.LocalDateTime;
import java.util.UUID;

/**
 * Represents a transaction of withdrawing money from account.
 */
public class WithdrawTransaction extends Transaction {
    /**
     * WithdrawTransaction constructor
     * @param account the account transaction was made from
     * @param moneyAmount amount of money in transaction
     * @param commission commission amount was taken from client
     * @param dateTime date and time of transaction
     */
    public WithdrawTransaction(Account account, double moneyAmount, double commission, LocalDateTime dateTime) {
        Id = UUID.randomUUID();
        this.account = account;
        this.moneyAmount = moneyAmount;
        this.commission = commission;
        this.dateTime = dateTime;
    }

    /**
     * Cancels transaction and returns money to account.
     * @throws BanksException if transaction is already canceled
     */
    public void cancel() throws BanksException {
        if (canceled) {
            throw new BanksException("Failed to Cancel transaction, transaction is already canceled!");
        }

        double newMoneyAmount = account.getMoney() + moneyAmount + commission;
        account.setMoney(newMoneyAmount);
        canceled = true;
    }
}
