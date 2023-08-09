package test;

import accounts.CreditAccount;
import accounts.DebitAccount;
import accounts.DepositAccount;
import bank.Bank;
import bank.CentralBank;
import client.Client;
import client.ClientBuilder;
import models.FullName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.Assertions;

import java.time.Duration;

/**
 * Class for Banks testing.
 */
public class BanksTests {
    /**
     * Central bank for management of the general banking system.
     */
    private final CentralBank centralBank = CentralBank.getCentralBank();

    /**
     * Testing if new bank is in central bank system, if bank has correct name, if all commissions, interests and limits are set correctly.
     */
    @Test
    public void bankCreation() {
        final String name = "Bank1";
        final double depositInterest = 10;
        final double creditCommission = 100;
        final double debitCommission = 5;
        final double doubtfulClientLimit = 50000;

        Bank newBank = centralBank.createNewBank(name, depositInterest, creditCommission, debitCommission, doubtfulClientLimit);

        Assertions.assertTrue(centralBank.getBanks().contains(newBank));
        Assertions.assertEquals(newBank.getName(), name);
        Assertions.assertEquals(newBank.getDepositInterest(), depositInterest);
        Assertions.assertEquals(newBank.getCreditCommission(), creditCommission);
        Assertions.assertEquals(newBank.getDebitCommission(), debitCommission);
        Assertions.assertEquals(newBank.getDoubtfulClientLimit(), doubtfulClientLimit);
    }

    /**
     * Testing if creating client by ClientBuilder works correctly(correct name, surname, passport info, address) and if client was added to bank system.
     */
    @Test
    public void creatingClientAndAddingToBank() {
        final String bankName = "Bank2";
        final double depositInterest = 10;
        final double creditCommission = 100;
        final double debitCommission = 5;
        final double doubtfulClientLimit = 50000;

        Bank bank = centralBank.createNewBank(bankName, depositInterest, creditCommission, debitCommission, doubtfulClientLimit);

        final String client_name = "Ivan";
        final String client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.setFullName(new FullName(client_name, client_surname));
        Client client = clientBuilder.createClient();

        bank.addNewClient(client);

        Assertions.assertEquals(client_name, client.getFullName().getName());
        Assertions.assertEquals(client_surname, client.getFullName().getSurname());
        Assertions.assertNull(client.getPassportData());
        Assertions.assertNull(client.getAddress());
        Assertions.assertTrue(bank.getClients().contains(client));
    }

    /**
     * Testing if creating different account works correctly and client has all created account(debit, deposit, credit).
     */
    @Test
    public void creatingClientsAccounts() {
        final String bank_name = "Bank3";
        final double depositInterest = 10;
        final double creditCommission = 100;
        final double debitCommission = 5;
        final double doubtfulClientLimit = 50000;

        Bank bank = centralBank.createNewBank(bank_name, depositInterest, creditCommission, debitCommission, doubtfulClientLimit);

        final String client_name = "Ivan";
        final String client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.setFullName(new FullName(client_name, client_surname));
        Client client = clientBuilder.createClient();

        bank.addNewClient(client);

        var deposit_period = Duration.ofDays(60);
        final double money_amount = 100000;
        DebitAccount debitAccount = bank.createDebitAccount(client.getId());
        CreditAccount creditAccount = bank.createCreditAccount(client.getId());
        DepositAccount depositAccount = bank.createDepositAccount(client.getId(), deposit_period, money_amount);

        Assertions.assertTrue(client.getAccounts().contains(debitAccount));
        Assertions.assertTrue(client.getAccounts().contains(creditAccount));
        Assertions.assertTrue(client.getAccounts().contains(depositAccount));
    }

    /**
     * Testing to replenish money to account, if we replenish money to account they actually appear there.
     */
    @Test
    public void replenishToAccount() {
        final String bankName = "Bank4";
        final double depositInterest = 10;
        final double creditCommission = 100;
        final double debitCommission = 5;
        final double doubtfulClientLimit = 50000;

        Bank bank = centralBank.createNewBank(bankName, depositInterest, creditCommission, debitCommission, doubtfulClientLimit);

        final String client_name = "Ivan";
        final String client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.setFullName(new FullName(client_name, client_surname));
        Client client = clientBuilder.createClient();

        bank.addNewClient(client);

        final double money_amount = 100000;
        final double money_amount_twice = 200000;
        final double ZeroMoney = 0;
        DebitAccount debitAccount = bank.createDebitAccount(client.getId());

        Assertions.assertEquals(debitAccount.getMoney(), ZeroMoney);

        centralBank.replenishAccount(debitAccount.getId(), money_amount);
        Assertions.assertEquals(debitAccount.getMoney(), money_amount);

        centralBank.replenishAccount(debitAccount.getId(), money_amount);
        Assertions.assertEquals(debitAccount.getMoney(), money_amount_twice);
    }

    /**
     * Testing to withdraw from account, if we withdraw from account money should disappear from account.
     */
    @Test
    public void withdrawFromAccount() {
        final String bank_name = "Bank5";
        final double depositInterest = 10;
        final double creditCommission = 100;
        final double debitCommission = 5;
        final double doubtfulClientLimit = 50000;

        Bank bank = centralBank.createNewBank(bank_name, depositInterest, creditCommission, debitCommission, doubtfulClientLimit);

        final String client_name = "Ivan";
        final String client_surname = "Ivanov";

        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.setFullName(new FullName(client_name, client_surname));
        Client client = clientBuilder.createClient();

        bank.addNewClient(client);

        final double repl_money_amount = 100000;
        final double withdraw_money_amount = 40000;
        final double expected_money_left = repl_money_amount - withdraw_money_amount;

        DebitAccount debitAccount = bank.createDebitAccount(client.getId());
        centralBank.replenishAccount(debitAccount.getId(), repl_money_amount);

        centralBank.withdrawFromAccount(debitAccount.getId(), withdraw_money_amount);
        Assertions.assertEquals(expected_money_left, debitAccount.getMoney());
    }

    /**
     * Testing to transfer money from one account to another, from first account they should disappear and should appear at the second account.
     */
    @Test
    public void transferBetweenAccounts() {
        final String bank_name = "Bank6";
        final double depositInterest = 10;
        final double creditCommission = 100;
        final double debitCommission = 5;
        final double doubtfulClientLimit = 50000;

        Bank bank = centralBank.createNewBank(bank_name, depositInterest, creditCommission, debitCommission, doubtfulClientLimit);

        final String client1_name = "Ivan";
        final String client1_surname = "Ivanov";

        ClientBuilder client1Builder = new ClientBuilder();
        client1Builder.setFullName(new FullName(client1_name, client1_surname));
        Client client1 = client1Builder.createClient();

        final String client2_name = "Name";
        final String client2_surname = "Surname";

        ClientBuilder client2Builder = new ClientBuilder();
        client2Builder.setFullName(new FullName(client2_name, client2_surname));
        Client client2 = client2Builder.createClient();

        bank.addNewClient(client1);
        bank.addNewClient(client2);

        final double repl_money_amount = 100000;
        final double transfer_money_amount = 30000;
        final double expected_money_left = repl_money_amount - transfer_money_amount;

        DebitAccount debitAccount1 = bank.createDebitAccount(client1.getId());
        DebitAccount debitAccount2 = bank.createDebitAccount(client2.getId());
        centralBank.replenishAccount(debitAccount1.getId(), repl_money_amount);

        centralBank.transferMoneyBetweenAccounts(debitAccount1.getId(), debitAccount2.getId(), transfer_money_amount);
        Assertions.assertEquals(expected_money_left, debitAccount1.getMoney());
        Assertions.assertEquals(transfer_money_amount, debitAccount2.getMoney());
    }
}
