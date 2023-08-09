package client;

import exceptions.BanksException;
import models.Address;
import models.FullName;
import models.PassportData;

/**
 * Class to build client step-by-step.
 */
public class ClientBuilder {
    /**
     * Client address.
     */
    private Address clientAddress;

    /**
     * Client passport info.
     */
    private PassportData clientPassportData;

    /**
     * Client name and surname.
     */
    private FullName clientFullName;

    /**
     * ClientBuilder constructor, sets all field to null.
     */
    public ClientBuilder() {
        clientAddress = null;
        clientPassportData = null;
        clientFullName = null;
    }

    /**
     * Sets address of client.
     * @param address address of client
     */
    public void setAddress(Address address) {
        clientAddress = address;
    }

    /**
     * Sets passport info of client.
     * @param passportData passport info of client
     */
    public void setPassportData(PassportData passportData) {
        clientPassportData = passportData;
    }

    /**
     * Sets name and surname of client.
     * @param fullName name and surname of client
     * @throws BanksException if FullName param is null
     */
    public void setFullName(FullName fullName) throws BanksException {
        if (fullName == null) {
            throw new BanksException("Failed to setFullName, fullName can not be null!");
        }

        clientFullName = fullName;
    }

    /**
     * Creates client instance and return it.
     * @return created client instance
     * @throws BanksException if FullName was not set
     */
    public Client createClient() throws BanksException {
        if (clientFullName == null) {
            throw new BanksException("Failed to create client, client has to have a name!");
        }

        return new ClientImpl(clientFullName, clientAddress, clientPassportData);
    }
}
