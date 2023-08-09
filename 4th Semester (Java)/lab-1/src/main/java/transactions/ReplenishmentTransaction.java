package transactions;

import accounts.Account;
import exceptions.BanksException;

import java.time.LocalDateTime;
import java.util.UUID;

/**
 * Represents a transaction for replenishing a bank account.
 */
public class ReplenishmentTransaction extends Transaction {
    /**
     * ReplenishmentTransaction constructor.
     * @param account the account from which the transaction was made
     * @param moneyAmount the amount of money was replenished
     * @param commission the amount of money was taken from user as commission
     * @param dateTime the date and time of transaction
     */
    public ReplenishmentTransaction(Account account, double moneyAmount, double commission, LocalDateTime dateTime) {
        Id = UUID.randomUUID();
        this.account = account;
        this.moneyAmount = moneyAmount;
        this.commission = commission;
        this.dateTime = dateTime;
    }

    /**
     * Cancels transaction and deletes money were replenished.
     * @throws BanksException if transaction is already canceled
     */
    public void cancel() throws BanksException {
        if (canceled) {
            throw new BanksException("Failed to Cancel transaction, transaction is already canceled!");
        }

        double newMoneyAmount = getAccount().getMoney() - (moneyAmount - commission);
        account.setMoney(newMoneyAmount);
        canceled = true;
    }
}
