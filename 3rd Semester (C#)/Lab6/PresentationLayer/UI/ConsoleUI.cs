using BusinessLayer.EmployeesLogic;
using BusinessLayer.Manager;
using DataAccessLayer.Messages;
using DataAccessLayer.Reports;

namespace PresentationLayer.UI;

public class ConsoleUI
{
    private BllManager manager = new ();

    public ConsoleUI()
    {
        LogInMenu();
    }

    private void LogInMenu()
    {
        AbstractEmployeeLogic? emp;
        while (true)
        {
            Console.WriteLine("Enter your login and password");
            string login = InputLogin();
            string password = InputPassword();
            emp = manager.LogIn(login, password);
            if (emp is not null)
                break;

            Console.WriteLine("Incorrect login or password! Try again");
        }

        if (emp is EmployeeLogic)
            EmployeeConsole((EmployeeLogic)emp);
        if (emp is BossLogic)
            BossConsole((BossLogic)emp);
    }

    private string InputLogin()
    {
        while (true)
        {
            Console.Write("login: ");
            string? login = Console.ReadLine();
            if (login is not null)
                return login;
        }
    }

    private string InputPassword()
    {
        while (true)
        {
            Console.Write("password: ");
            string? password = Console.ReadLine();
            if (password is not null)
                return password;
        }
    }

    private void EmployeeConsole(EmployeeLogic logic)
    {
        PrintEmployeeMenuHelp();
        InputEmployeeMenuOption(logic);
    }

    private void PrintEmployeeMenuHelp()
    {
        Console.WriteLine("Welcome back!");
        Console.WriteLine("What you can do:");
        Console.WriteLine("1) Get list of all messages");
        Console.WriteLine("2) Get list of new messages");
        Console.WriteLine("3) Get list of recieved messages");
        Console.WriteLine("4) Get list of processed messages");
        Console.WriteLine("5) Answer message");
        Console.WriteLine("6) Mark message as processed");
        Console.WriteLine("7) Exit");
    }

    private void InputEmployeeMenuOption(EmployeeLogic logic)
    {
        while (true)
        {
            Console.WriteLine("Type a number of action you want to do...");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num == 1)
                PrintAllMessagesOfEmployee(logic);
            else if (num == 2)
                PrintAllNewMessagesOfEmployee(logic);
            else if (num == 3)
                PrintAllRecievedMessagesOfEmployee(logic);
            else if (num == 4)
                PrintAllProcessedMessages(logic);
            else if (num == 5)
                AnswerMessage();
            else if (num == 6)
                MarkMessageAsProcessed();
            else if (num == 7)
                return;
            else
                Console.WriteLine("Incorrect input!");
        }
    }

    private void PrintAllMessagesOfEmployee(EmployeeLogic logic)
    {
        IReadOnlyList<DataAccessLayer.Messages.AbstractMessage> list = manager.GetAllMessagesOfEmployee(logic);
        PrintListOfMessages(list);
    }

    private void PrintAllNewMessagesOfEmployee(EmployeeLogic logic)
    {
        IReadOnlyList<DataAccessLayer.Messages.AbstractMessage> list = manager.GetAllNewMessagesOfEmployee(logic);
        PrintListOfMessages(list);
    }

    private void PrintAllRecievedMessagesOfEmployee(EmployeeLogic logic)
    {
        IReadOnlyList<DataAccessLayer.Messages.AbstractMessage> list = manager.GetAllRecievedMessages(logic);
        PrintListOfMessages(list);
    }

    private void PrintAllProcessedMessages(EmployeeLogic logic)
    {
        IReadOnlyList<DataAccessLayer.Messages.AbstractMessage> list = manager.GetAllProcessedMessages(logic);
        PrintListOfMessages(list);
    }

    private void AnswerMessage()
    {
        AbstractMessage msg = FindMessage();
        string answer = InputMessageAnswer();
        manager.AnswerMessage(msg, answer);
    }

    private void MarkMessageAsProcessed()
    {
        AbstractMessage msg = FindMessage();
        manager.MarkMessageAsProcessed(msg);
    }

    private void BossConsole(BossLogic logic)
    {
        PrintBossMenuHelp();
        InputBossMenuOption(logic);
    }

    private void PrintBossMenuHelp()
    {
        Console.WriteLine("Welcome back!");
        Console.WriteLine("What you can do:");
        Console.WriteLine("1) Get list of all reports");
        Console.WriteLine("2) Get list of all reports by date");
        Console.WriteLine("3) Form report");
        Console.WriteLine("4) Exit");
    }

    private void InputBossMenuOption(BossLogic logic)
    {
        while (true)
        {
            Console.WriteLine("Type a number of action you want to do...");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num == 0)
                return;
            else if (num == 1)
                PrintListOfAllReports(logic);
            else if (num == 2)
                PrintListOfAllReportsByDate(logic);
            else if (num == 3)
                FormReport(logic);
            else if (num == 4)
                return;
            else
                Console.WriteLine("Incorrect input!");
        }
    }

    private void FormReport(BossLogic logic)
    {
        DateOnly date = InputDate();
        manager.FromReport(logic, date);
    }

    private void PrintListOfAllReports(BossLogic logic)
    {
        IReadOnlyList<Report> list = manager.GetListOfReports(logic);
        PrintListOfReports(list);
    }

    private void PrintListOfAllReportsByDate(BossLogic logic)
    {
        DateOnly date = InputDate();
        IReadOnlyList<Report> list = manager.GetListOfReportsOfDate(logic, date);
        PrintListOfReports(list);
    }

    private DateOnly InputDate()
    {
        while (true)
        {
            DateOnly date;
            if (DateOnly.TryParse(Console.ReadLine(), out date))
                return date;
            Console.WriteLine("You have entered an incorrect value.");
        }
    }

    private string InputMessageAnswer()
    {
        while (true)
        {
            string? answer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(answer))
                return answer;
            Console.WriteLine("Input answer!");
        }
    }

    private AbstractMessage FindMessage()
    {
        while (true)
        {
            Guid msg_id = InputGuid();
            AbstractMessage? msg = manager.FindMessageByID(msg_id);
            if (msg is not null)
                return msg;
            Console.WriteLine("Message with given id doesn't exists!");
        }
    }

    private Guid InputGuid()
    {
        while (true)
        {
            Console.WriteLine("Enter id: ");

            string? id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("id can not be null or white space!");
                continue;
            }

            try
            {
                return Guid.Parse(id);
            }
            catch (Exception)
            {
                Console.WriteLine("Incorrect id! Try again");
            }
        }
    }

    private void PrintListOfReports(IReadOnlyList<Report> list)
    {
        foreach (Report report in list)
        {
            Console.WriteLine(report.ToString());
        }
    }

    private void PrintListOfMessages(IReadOnlyList<DataAccessLayer.Messages.AbstractMessage> list)
    {
        foreach (DataAccessLayer.Messages.AbstractMessage msg in list)
        {
            Console.WriteLine(msg.ToString());
        }
    }
}
