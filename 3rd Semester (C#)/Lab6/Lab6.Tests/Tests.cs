using BusinessLayer.EmployeesLogic;
using BusinessLayer.Manager;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;
using DataAccessLayer.MessageSources;
using Xunit;

namespace Lab6.Tests;

public class Lab6Tests
{
    private readonly BllManager manager = new ();

    [Fact]
    public void SendingMessage()
    {
        const int ExceptedAmountOfMessages = 3;
        const string text = "BOBA";
        const uint empAcess = 99999;
        const uint accessLevel = 0;
        EmailMessageSource email = new ("mail@mail.mail");
        PhoneMessageSource phone = new ("88005553535");
        MessagerMessageSource messager = new ("messager3000");

        BossLogic bossLogic = manager.CreateNewBoss("a", "a", empAcess, new List<Employee>());
        EmployeeLogic empLogic = manager.CreateNewEmployee("b", "b", empAcess, new List<Employee>(), bossLogic.Boss);

        AbstractMessage msg1 = manager.SendMessageToSystem(email, text, accessLevel);
        AbstractMessage msg2 = manager.SendMessageToSystem(phone, text, accessLevel);
        AbstractMessage msg3 = manager.SendMessageToSystem(messager, text, accessLevel);

        Assert.Equal(ExceptedAmountOfMessages, manager.GetAllMessagesOfEmployee(empLogic).Count);
        Assert.Contains(msg1, manager.GetAllMessagesOfEmployee(empLogic));
        Assert.Contains(msg2, manager.GetAllMessagesOfEmployee(empLogic));
        Assert.Contains(msg3, manager.GetAllMessagesOfEmployee(empLogic));
    }

    [Fact]
    public void AnswerMessage()
    {
        const string answer = "BIBA";
        const string text = "BOBA";
        const uint empAcess = 99999;
        const uint accessLevel = 0;
        EmailMessageSource email = new ("mail@mail.mail");

        BossLogic bossLogic = manager.CreateNewBoss("c", "c", empAcess, new List<Employee>());
        EmployeeLogic empLogic = manager.CreateNewEmployee("d", "d", empAcess, new List<Employee>(), bossLogic.Boss);

        AbstractMessage msg = manager.SendMessageToSystem(email, text, accessLevel);
        manager.AnswerMessage(empLogic, msg, answer);

        Assert.Equal(manager.FindMessageByID(msg.ID)?.Answer, answer);
    }

    [Fact]
    public void MarkMessageAsProcessed()
    {
        const string text = "BOBA";
        const uint empAcess = 99999;
        const uint accessLevel = 0;
        EmailMessageSource email = new ("mail@mail.mail");

        BossLogic bossLogic = manager.CreateNewBoss("e", "e", empAcess, new List<Employee>());
        EmployeeLogic empLogic = manager.CreateNewEmployee("f", "f", empAcess, new List<Employee>(), bossLogic.Boss);

        AbstractMessage msg = manager.SendMessageToSystem(email, text, accessLevel);
        manager.MarkMessageAsProcessed(empLogic, msg);

        Assert.Contains(msg, manager.GetAllProcessedMessages(empLogic));
    }

    [Fact]
    public void FormReport()
    {
        const int ExpectedAmountOfReports = 1;
        const int ExpectedAmountOfReportsWithCorrectDate = 1;
        const int ExpectedAmountOfReportsWithInCorrectDate = 0;
        const string text = "BOBA";
        const string answer = "Otvali";
        const uint empAcess = 99999;
        const uint accessLevel = 0;
        EmailMessageSource email = new ("mail@mail.mail");

        BossLogic bossLogic = manager.CreateNewBoss("g", "g", empAcess, new List<Employee>());
        EmployeeLogic empLogic1 = manager.CreateNewEmployee("k", "k", empAcess, new List<Employee>(), bossLogic.Boss);
        EmployeeLogic empLogic2 = manager.CreateNewEmployee("n", "n", empAcess, new List<Employee>(), bossLogic.Boss);

        AbstractMessage msg1 = manager.SendMessageToSystem(email, text + "1", accessLevel);
        AbstractMessage msg2 = manager.SendMessageToSystem(email, text + "2", accessLevel);
        AbstractMessage msg3 = manager.SendMessageToSystem(email, text + "3", accessLevel);
        AbstractMessage msg4 = manager.SendMessageToSystem(email, text + "4", accessLevel);
        manager.MarkMessageAsProcessed(empLogic1, msg1);
        manager.MarkMessageAsProcessed(empLogic2, msg2);
        manager.AnswerMessage(empLogic1, msg3, answer);
        manager.AnswerMessage(empLogic1, msg4, answer);

        var date = DateOnly.FromDateTime(DateTime.Now);
        var incorrectDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5));
        manager.FormReport(bossLogic, date);

        Assert.Equal(ExpectedAmountOfReports, manager.GetListOfReports(bossLogic).Count);
        Assert.Equal(ExpectedAmountOfReportsWithCorrectDate, manager.GetListOfReportsOfDate(bossLogic, date).Count);
        Assert.Equal(ExpectedAmountOfReportsWithInCorrectDate, manager.GetListOfReportsOfDate(bossLogic, incorrectDate).Count);
    }
}