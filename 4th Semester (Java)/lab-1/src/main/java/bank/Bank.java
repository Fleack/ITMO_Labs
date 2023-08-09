package bank;

import accounts.Account;
import accounts.CreditAccount;
import accounts.DebitAccount;
import accounts.DepositAccount;
import client.Client;
import exceptions.BanksException;

import java.util.ArrayList;
import java.util.Objects;
import java.time.Duration;
import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.UUID;

/**
 * Provides customers with several types of accounts and the ability to interact with them.
 */
public class Bank {
    private final double MinCommissionsValue = 0;

    /**
     * List of all clients of this bank.
     */
    private final List<Client> clients;

    /**
     * Name of this bank.
     */
    private final String name;

    /**
     * Unique ID of bank.
     */
    private final UUID id;

    /**
     * The amount of money credited for the deposit in the bank on the deposit account.
     */
    private double depositInterest;

    /**
     * The amount of money charged to the customer per transaction from the credit account.
     */
    private double creditCommission;

    /**
     * The amount of money charged to the customer per transaction from the debit account.
     */
    private double debitCommission;

    /**
     * Transaction limit for unverified clients.
     */
    private double doubtfulClientLimit;

    /**
     * Bank constructor.
     * @param name bank name
     * @param depositInterest interest on the deposit
     * @param creditCommission the commission charged to the client of credit account
     * @param debitCommission the commission charged to the client of debit account
     * @param doubtfulClientLimit transaction limit for unverified clients
     * @throws BanksException if name param is empty, deposit interesting/creditCommission/debitCommission is negative
     */
    public Bank(
        String name,
        double depositInterest,
        double creditCommission,
        double debitCommission,
        double doubtfulClientLimit) throws BanksException {
        if (name.isEmpty()) {
            throw new BanksException("Failed to construct Bank, given value name can not be null or white space");
        }

        if (depositInterest < MinCommissionsValue) {
            throw new BanksException("Failed to construct Bank, given value: depositInterest can not <= " + MinCommissionsValue);
        }

        if (creditCommission < MinCommissionsValue) {
            throw new BanksException("Failed to construct Bank, given value: creditCommission can not <= " + MinCommissionsValue);
        }

        if (debitCommission < MinCommissionsValue) {
            throw new BanksException("Failed to construct Bank, given value: debitCommission can not <= " + MinCommissionsValue);
        }

        this.name = name;
        this.id = UUID.randomUUID();
        this.clients = new ArrayList<>();
        this.depositInterest = depositInterest;
        this.creditCommission = creditCommission;
        this.debitCommission = debitCommission;
        this.doubtfulClientLimit = doubtfulClientLimit;
    }

    /**
     * Adds new client to the bank system.
     * @param newClient the client you want to add
     * @throws BanksException if client with same passport data already exists
     */
    public void addNewClient(Client newClient) throws BanksException  {
        if (clients.stream().anyMatch(client ->
                Objects.equals(client.getPassportData(), newClient.getPassportData()) &&
                        newClient.getPassportData() != null)) {
            throw new BanksException("Failed to AddNewClient, client with same PassportData already exists");
        }

        clients.add(newClient);
    }

    /**
     * Removes client from bank system.
     * @param client client you want to remove
     */
    public void removeClient(Client client) {
        clients.remove(client);
    }

    /**
     * Operation of withdrawing from account.
     * @param accountId ID of account we want to withdraw from
     * @param amount Amount of money we want to withdraw
     * @throws BanksException if amount param is bigger than doubtfulClientLimit
     */
    public void withdrawFromAccount(UUID accountId, double amount) throws BanksException  {
        Account accountFrom = getAccountByID(accountId);
        Client accountOwner = getAccountOwnerByAccountID(accountId);

        if (!accountOwner.getConfirmedStatus() &&
            doubtfulClientLimit > 0 &&
            amount > doubtfulClientLimit) {
            throw new BanksException("Failed to WithdrawFromAccount, client has limit " + doubtfulClientLimit + "for Withdraws and Transfers");
        }

        accountFrom.withdraw(amount);
    }

    /**
     * Creates new credit account for client.
     * @param clientId ID of client
     * @return created CreditAccount for given client
     */
    public CreditAccount createCreditAccount(UUID clientId) {
        Client client = getClientByID(clientId);
        CreditAccount creditAccount = new CreditAccount(client, creditCommission);
        client.addNewAccount(creditAccount);

        return creditAccount;
    }

    /**
     * Creates new debit account for client.
     * @param clientId ID of client
     * @return created DebitAccount for given client
     */
    public DebitAccount createDebitAccount(UUID clientId) {
        Client client = getClientByID(clientId);
        DebitAccount debitAccount = new DebitAccount(client, debitCommission);
        client.addNewAccount(debitAccount);

        return debitAccount;
    }

    /**
     * Creates new deposit account for client.
     * @param clientId ID of client
     * @param depositPeriod the period during which the deposit will be opened
     * @param moneyAmount amount of money you want to add to account
     * @return created DepositAccount for given client
     */
    public DepositAccount createDepositAccount(UUID clientId, Duration depositPeriod, double moneyAmount) {
        Client client = getClientByID(clientId);
        DepositAccount depositAccount = new DepositAccount(client, depositPeriod, moneyAmount, depositInterest);
        client.addNewAccount(depositAccount);

        return depositAccount;
    }

    /**
     * Replenished given amount of money from given account.
     * @param accountId ID of account
     * @param amount amount of money you want to replenish
     */
    public void replenishAccount(UUID accountId, double amount) {
        Account accountTo = getAccountByID(accountId);
        accountTo.replenish(amount);
    }

    /**
     * Sets new depositInterest.
     * @param newValue new depositInterest value
     * @throws BanksException if new depositInterest is negative
     */
    public void setDepositInterest(double newValue) throws BanksException  {
        if (newValue < MinCommissionsValue) {
            throw new BanksException("Given value: depositInterest can not <= " + "MinCommissionsValue");
        }

        depositInterest = newValue;
        notifySubscribedClients();
    }

    /**
     * Sets new creditCommission.
     * @param newValue new creditCommission value
     * @throws BanksException if new creditCommission is negative
     */
    public void setCreditCommission(double newValue) throws BanksException  {
        if (newValue < MinCommissionsValue) {
            throw new BanksException("Given value: depositInterest can not <= " + "MinCommissionsValue");
        }

        creditCommission = newValue;
        notifySubscribedClients();
    }

    /**
     * Sets new debitCommission.
     * @param newValue new debitCommission value
     * @throws BanksException if new debitCommission is negative
     */
    public void setDebitCommission(double newValue) throws BanksException  {
        if (newValue < MinCommissionsValue) {
            throw new BanksException("Given value: depositInterest can not <= " + "MinCommissionsValue");
        }

        debitCommission = newValue;
        notifySubscribedClients();
    }

    /**
     * Sets new doubtfulClientLimit.
     * @param newValue new doubtfulClientLimit value
     * @throws BanksException if new doubtfulClientLimit is negative
     */
    public void setDoubtfulClientLimit(double newValue) throws BanksException {
        if (newValue < MinCommissionsValue) {
            throw new BanksException("Given value: depositInterest can not <= " + "MinCommissionsValue");
        }

        doubtfulClientLimit = newValue;
        notifySubscribedClients();
    }

    /**
     * Adds daily commission to all accounts.
     */
    public void dailyAccountNotification() {
        clients.stream().flatMap(client -> client.getAccounts().stream()).forEach(Account::addDailyCommission);
    }

    /**
     * Adds monthly commission to all accounts.
     */
    public void monthlyAccountNotification() {
        clients.stream().flatMap(client -> client.getAccounts().stream()).forEach(Account::addMonthlyCommission);
    }

    public double getMinCommissionsValue() {
        return MinCommissionsValue;
    }

    public String getName() {
        return name;
    }

    public UUID getId() {
        return id;
    }

    public double getDepositInterest() {
        return depositInterest;
    }

    public double getCreditCommission() {
        return creditCommission;
    }

    public double getDebitCommission() {
        return debitCommission;
    }

    public double getDoubtfulClientLimit() {
        return doubtfulClientLimit;
    }

    public Collection<Client> getClients() {
        return Collections.unmodifiableCollection(clients);
    }

    /**
     * Notifies all subscribed clients of rules changing.
     */
    private void notifySubscribedClients() {
        clients.stream().filter(Client::getChangesSubscription).forEach(Client::accountRulesChanged);
    }

    /**
     * Gets account by ID of account.
     * @param accountId ID of account
     * @return Account found by its ID
     * @throws BanksException if account with given id doesn't exist
     */
    private Account getAccountByID(UUID accountId) throws BanksException  {
        Account result = clients.stream()
                .flatMap(client -> client.getAccounts().stream())
                .filter(account -> account.getId().equals(accountId))
                .findFirst().orElse(null);

        if (result == null) {
            throw new BanksException("Failed to FindAccountByID, account with given id " + accountId + " doesn't exist");
        }

        return  result;
    }

    /**
     * Gets client by ID of account.
     * @param accountId ID of account
     * @return client, owner of account
     * @throws BanksException if account owner doesn't exist
     */
    private Client getAccountOwnerByAccountID(UUID accountId) throws BanksException  {
        Client accountOwner = clients.stream()
                .filter(client -> client.getAccounts().stream()
                        .anyMatch(account -> account.getId().equals(accountId)))
                .findFirst()
                .orElse(null);

        if (accountOwner == null) {
            throw new BanksException("Failed to GetAccountOwnerByAccountID, accountOwner with given id doesn't exist");
        }

        return accountOwner;
    }

    /**
     * Gets client by ID of client.
     * @param clientId ID of client
     * @return client found by its ID
     * @throws BanksException if client with given ID doesn't exist
     */
    private Client getClientByID(UUID clientId) throws BanksException  {
        Client result = clients.stream()
                .filter(client -> client.getId().equals(clientId))
                .findFirst()
                .orElse(null);

        if (result == null) {
            throw new BanksException("Failed to GetClientID, clientId with given id doesn't exist");
        }

        return result;
    }
}
