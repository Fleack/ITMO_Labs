package bank;

import accounts.Account;
import client.Client;
import exceptions.BanksException;
import transactions.ReplenishmentTransaction;
import transactions.Transaction;
import transactions.TransferTransaction;
import transactions.WithdrawTransaction;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.time.LocalDateTime;
import java.util.List;
import java.util.UUID;

/**
 * Central singleton class that provides an interface for user interaction with the banking system.
 */
public class CentralBank {
    private final int FirstDayOfMonth = 1;

    /**
     * Static instance of central bank.
     */
    private static CentralBank centralBank = null;

    /**
     * List of all banks.
     */
    private final List<Bank> banks;

    /**
     * List of all transactions.
     */
    private final List<Transaction> transactions;

    /**
     * Current date in bank system.
     */
    private LocalDateTime date;

    /**
     * Private constructor for singleton.
     */
    private CentralBank() {
        banks = new ArrayList<Bank>();
        transactions = new ArrayList<Transaction>();
        date = LocalDateTime.now();
    }

    /**
     * If an instance of the class does not exist, it will be created and returned,
     * if the exam already exists, the previously created instance will be returned.
     * @return CentralBank instance
     */
    public static CentralBank getCentralBank() {
        if (centralBank == null) {
            centralBank = new CentralBank();
        }

        return centralBank;
    }

    /**
     * Creates new bank and adds it to the system.
     * @param name name of bank
     * @param depositInterest banks deposit interest
     * @param creditCommission banks credit commission for clients
     * @param debitCommission banks debit commission for clients
     * @param doubtfulClientLimit banks limit for unverified clients
     * @return created bank instance
     * @throws BanksException if bank with given name already exists
     */
    public Bank createNewBank(
        String name,
        double depositInterest,
        double creditCommission,
        double debitCommission,
        double doubtfulClientLimit) throws BanksException  {
        if (banks.stream().anyMatch(bank -> bank.getName().equals(name))) {
            throw new BanksException("Failed to CreateNewBank, Bank with given name already exists");
        }

        Bank bank = new Bank (name, depositInterest, creditCommission, debitCommission, doubtfulClientLimit);
        banks.add(bank);

        return bank;
    }

    /**
     * Adding bank created not by CentralBank to the system.
     * @param newBank bank you want to add to the system
     * @throws BanksException if bank with the same name already exists
     */
    public void addNewBank(Bank newBank) throws BanksException  {
        if (banks.stream().anyMatch(bank -> bank.getName().equals(newBank.getName()))) {
            throw new BanksException("Failed to CreateNewBank, Bank with given name already exists");
        }

        banks.add(newBank);
    }

    /**
     * Transfers the execution of the withdrawal transaction to the client's bank,
     * creates and saves the transaction to the system.
     * @param accountId ID of account
     * @param amount amount of money you want to withdraw
     */
    public void withdrawFromAccount(UUID accountId, double amount) {
        getAccountBank(accountId).withdrawFromAccount(accountId, amount);
        formWithdrawTransaction(getAccountByID(accountId), amount, getAccountByID(accountId).getCommission());
    }

    /**
     * Transfers the execution of the replenishment transaction to the client's bank,
     * creates and saves the transaction to the system.
     * @param accountId ID of account
     * @param amount amount of money you want to replenish
     */
    public void replenishAccount(UUID accountId, double amount) {
        getAccountBank(accountId).replenishAccount(accountId, amount);
        formReplenishmentTransaction(getAccountByID(accountId), amount, getAccountByID(accountId).getCommission());
    }

    /**
     * Transfers the execution of the transferring money transaction to the client's bank,
     * creates and saves the transaction to the system.
     * @param accountFromId ID of account we want transfer money to
     * @param accountToId ID of account we want transfer money from
     * @param amount amount of money you want to transfer
     */
    public void transferMoneyBetweenAccounts(UUID accountFromId, UUID accountToId, double amount) {
        getAccountBank(accountToId).replenishAccount(accountToId, amount);
        getAccountBank(accountFromId).withdrawFromAccount(accountFromId, amount);
        formTransferTransaction(getAccountByID(accountFromId), getAccountByID(accountToId), amount, getAccountByID(accountFromId).getCommission());
    }

    /**
     * Finds and cancels transaction.
     * @param transactionId ID of transaction you want to cancel
     * @throws BanksException if transaction with given ID doesn't exist or if it's already canceled
     */
    public void cancelTransaction(UUID transactionId) throws BanksException  {
        Transaction transaction = transactions.stream()
                .filter(tr -> tr.getId().equals(transactionId))
                .findFirst()
                .orElse(null);

        if (transaction == null) {
            throw new BanksException("Failed to CancelTransaction, transaction does not exist or already canceled");
        }

        transaction.cancel();
        transactions.remove(transaction);
    }

    /**
     * Simulates the passage of time, namely the passage of one day.
     */
    public void setNextDay() {
        date = date.plusDays(1);
        banksNotification();
    }

    /**
     * Simulates the passage of time, namely the passage of n days.
     * @param daysAmount amount of days you want to pass
     */
    public void timeAccelerator(int daysAmount) {
        for (int i = 0; i < daysAmount; i++) {
            setNextDay();
        }
    }

    /**
     * Gets account by its ID.
     * @param accountId ID of account
     * @return account with given ID
     * @throws BanksException if account with given ID doesn't exist
     */
    public Account getAccountByID(UUID accountId) throws BanksException  {
        Account result = banks.stream()
                .flatMap(bank -> bank.getClients().stream())
                .flatMap(client -> client.getAccounts().stream())
                .filter(account -> account.getId().equals(accountId))
                .findFirst()
                .orElse(null);

        if (result == null) {
            throw new BanksException("Failed to GetAccountByID, account with id doesn't exist");
        }

        return  result;
    }

    /**
     * Gets bank by its ID.
     * @param bankId ID of bank
     * @return bank with given ID
     * @throws BanksException if bank with given ID doesn't exist
     */
    public Bank getBankByID(UUID bankId) throws BanksException  {
        Bank result = banks.stream()
                .filter(bank -> bank.getId().equals(bankId))
                .findFirst()
                .orElse(null);

        if (result == null) {
            throw new BanksException("Failed to GetBankByID, bank with id doesn't exist");
        }

        return result;
    }

    public Collection<Bank> getBanks() {
        return Collections.unmodifiableCollection(banks);
    }

    /**
     * Causes all banks to have a method of notifying customers.
     */
    private void banksNotification() {
        dailyBanksNotification();
        if (date.getDayOfMonth() == FirstDayOfMonth)
            monthlyBanksNotification();
    }

    /**
     * Causes all banks to use the method of monthly notification of customers.
     */
    private void monthlyBanksNotification() {
        banks.forEach(Bank::monthlyAccountNotification);
    }

    /**
     * Causes all banks to use the method of daily notification of customers.
     */
    private void dailyBanksNotification() {
        banks.forEach(Bank::dailyAccountNotification);
    }

    /**
     * Gets the bank where the account is opened.
     * @param accountId ID of account
     * @return bank where account with given is opened
     * @throws BanksException if bank of account doesn't exist
     */
    private Bank getAccountBank(UUID accountId) throws BanksException  {
        Bank result = banks.stream()
                .filter(bank -> bank.getClients().stream()
                        .flatMap(client -> client.getAccounts().stream())
                        .anyMatch(account -> account.getId().equals(accountId)))
                .findFirst()
                .orElse(null);

        if (result == null) {
            throw new BanksException("Failed to GetAccountBank, Bank of account with id doesn't exist");
        }

        return result;
    }

    /**
     * Creates withdraw transaction and adds it to the system.
     * @param account account of transaction
     * @param amount amount of money in transaction
     * @param commission commission which was taken from client
     */
    private void formWithdrawTransaction(Account account, double amount, double commission) {
        WithdrawTransaction transaction = new WithdrawTransaction(account, amount, commission, date);
        transactions.add(transaction);
    }

    /**
     * Creates replenishment transaction and adds it to the system.
     * @param account account of transaction
     * @param amount amount of money in transaction
     * @param commission commission which was taken from client
     */
    private void formReplenishmentTransaction(Account account, double amount, double commission) {
        ReplenishmentTransaction transaction = new ReplenishmentTransaction(account, amount, commission, date);
        transactions.add(transaction);
    }

    /**
     * Creates transfer transaction and adds it to the system.
     * @param accountFrom account money were transferred from
     * @param accountTo account money were transferred to
     * @param amount amount of money in transaction
     * @param commission commission which was taken from client
     */
    private void formTransferTransaction(Account accountFrom, Account accountTo, double amount, double commission) {
        TransferTransaction transaction = new TransferTransaction(accountFrom, accountTo, amount, commission, date);
        transactions.add(transaction);
    }
}
