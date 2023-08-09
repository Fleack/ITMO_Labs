package client;

import accounts.Account;
import models.Address;
import models.FullName;
import models.PassportData;

import java.util.Collection;
import java.util.Collections;
import java.util.UUID;
import java.util.List;


/**
 * Represents a basic client logic.
 */
public abstract class Client {
    /**
     * Unique id of client.
     */
    protected UUID Id;

    /**
     * Name and surname of client.
     */
    protected FullName fullName;

    /**
     * Passport info of client.
     */
    protected PassportData passportData;

    /**
     * Address info of client.
     */
    protected Address address;

    /**
     * Status of client.
     */
    protected Boolean confirmedStatus;

    /**
     * Flag if client subscribed to bank changes.
     */
    protected Boolean changesSubscription;

    /**
     * List of all accounts client has.
     */
    protected List<Account> accounts;

    /**
     * Some actions client does if bank changed any rules.
     */
    public abstract void accountRulesChanged();

    public UUID getId() {
        return Id;
    }

    public FullName getFullName() {
        return fullName;
    }

    public PassportData getPassportData() {
        return passportData;
    }

    public Address getAddress() {
        return address;
    }

    public Boolean getConfirmedStatus() {
        return confirmedStatus;
    }

    public Boolean getChangesSubscription() {
        return changesSubscription;
    }

    public Collection<Account> getAccounts() {
        return Collections.unmodifiableCollection(accounts);
    }

    /**
     * Adding new account to list of all accounts of this client.
     * @param account account we want to add
     */
    public void addNewAccount(Account account) {
        accounts.add(account);
    }

    /**
     * Sets client status. Client gets confirmed status if address and passport data are specified.
     */
    protected void setStatus() {
        confirmedStatus = address != null && passportData != null;
    }
}
