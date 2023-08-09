package client;

import accounts.Account;
import exceptions.BanksException;
import models.Address;
import models.FullName;
import models.PassportData;

import java.util.ArrayList;
import java.util.UUID;

/**
 * Client implementation with basic logic.
 */
public class ClientImpl extends Client {
    /**
     * Flag if bank rules have changed.
     */
    private Boolean areRulesChanged;

    /**
     * ClientImpl constructor.
     * @param fullName Name and surname of client
     * @param address Address of client
     * @param passportData Passport of client
     * @throws BanksException if FullName param is null
     */
    public ClientImpl(FullName fullName, Address address, PassportData passportData) throws BanksException {
        if (fullName == null) {
            throw new BanksException("Failed to construct Client, given value: fullName can not be null");
        }

        this.fullName = fullName;
        this.address = address;
        this.passportData = passportData;
        changesSubscription = false;
        areRulesChanged = false;
        accounts = new ArrayList<Account>();
        Id = UUID.randomUUID();
        setStatus();
    }

    /**
     * Sets flag for client subscription to true.
     */
    public void subscribeToChanges() {
        changesSubscription = true;
    }

    /**
     * Sets flag for client subscription to false.
     */
    public void unsubscribeToChanges() {
        changesSubscription = false;
    }

    /**
     * Sets flag for changed rules to true.
     */
    public void accountRulesChanged() {
        areRulesChanged = true;
    }

    public Boolean getAreRulesChanged() {
        return areRulesChanged;
    }
}
