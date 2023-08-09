package transactions;

import accounts.Account;
import exceptions.BanksException;

import java.time.LocalDateTime;
import java.util.UUID;

/**
 * Represents a transaction of transferring money from one account to another.
 */
public class TransferTransaction extends Transaction {
    /**
     * The account money were transferred to.
     */
    public Account transferAccount;

    /**
     * TransferTransaction constructor.
     * @param accountFrom The account money were taken from
     * @param accountTo The account money were taken to
     * @param moneyAmount The amount of money was transferred
     * @param commission The amount of money was taken from user
     * @param dateTime The date and time of transaction
     */
    public TransferTransaction(Account accountFrom, Account accountTo, double moneyAmount, double commission, LocalDateTime dateTime) {
        Id = UUID.randomUUID();
        account = accountFrom;
        this.transferAccount = accountTo;
        this.moneyAmount = moneyAmount;
        this.commission = commission;
        this.dateTime = dateTime;
    }

    /**
     * Cancels transaction, takes money from TransferAccount and returns it to Account.
     * @throws BanksException if transaction is already canceled
     */
    public void cancel() throws BanksException {
        if (canceled) {
            throw new BanksException("Failed to Cancel transaction, transaction is already canceled!");
        }

        double newAccountMoneyAmount = account.getMoney() + moneyAmount + commission;
        double newTransferAccountMoneyAmount = transferAccount.getMoney() - moneyAmount;
        account.setMoney(newAccountMoneyAmount);
        transferAccount.setMoney(newTransferAccountMoneyAmount);
        canceled = true;
    }

    public Account getTransferAccount() {
        return transferAccount;
    }
}
