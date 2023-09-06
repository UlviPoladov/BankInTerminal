using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public class Bank
    {
        public Bank(string? bankName, int bankId, string? bankAddress, Client[] clients)
        {
            BankName = bankName;
            BankId = bankId;
            BankAddress = bankAddress;
            Clients = clients;
        }

        public string? BankName { get; set; } = null;
        public int BankId { get; set; }
        public string? BankAddress { get; set; } = null;

        public Client[] Clients { get; set; }

        public void ShowAllClients()
        {
            foreach (var client in Clients)
            {
                Console.WriteLine($"Client Id: {client.Id}");
                Console.WriteLine($"Client Name: {client.Name}");
                Console.WriteLine($"Client Surname: {client.Surname}");
                Console.WriteLine($"Client Age: {client.Age}");
                Console.WriteLine($"Client Salary: {client.Salary}");

                Console.WriteLine($"Client BankCard PAN: {client.Card.PAN}");
                Console.WriteLine($"Client PIN: {client.Card.PIN}");
                Console.WriteLine($"Client CVC: ***");
                Console.WriteLine($"Client Expires Date: {client.Card.ExpiresDate}");
                Console.WriteLine($"Client Balance: {client.Card.CurrentBalance:C}");
            }
        }
    }

    public class Client
    {
        public Client(string id, string name, string surname, int age, int salary, BankCard card)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            Card = card;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 18)
                {
                    Console.WriteLine("You Can't Take Card You're Under 18");
                }
                else
                {
                    age = value;
                }
            }
        }

        public int Salary { get; set; }
        public BankCard Card { get; set; }
    }

    public class BankCard
    {
        public BankCard(string? pAN, string? pIN, int cVC, DateTime expiresDate, decimal currentBalance)
        {
            PAN = pAN;
            PIN = pIN;
            CVC = cVC;
            ExpiresDate = expiresDate;
            CurrentBalance = currentBalance;
        }

        public string? PAN { get; set; }
        public string? PIN { get; set; }

        private int cvc;
        public int CVC
        {
            get { return cvc; }
            set
            {
                if (value > 0 && value < 1000)
                {
                    cvc = value;
                }
            }
        }

        public DateTime ExpiresDate { get; set; }
        public decimal CurrentBalance { get; set; }
    }

    public class Program
    {
        static void WithDrawCash(Client client)
        {
            Console.WriteLine("Select: ");
            Console.WriteLine("1. 10 AZN");
            Console.WriteLine("2. 20 AZN");
            Console.WriteLine("3. 50 AZN");
            Console.WriteLine("4. 100 AZN");
            Console.WriteLine("5. Other");

            int choice = int.Parse(Console.ReadLine());
            decimal amount = 0;
            Console.Clear();
            switch (choice)
            {
                case 1:
                    amount = 10.00m;
                    break;
                case 2:
                    amount = 20.00m;
                    break;
                case 3:
                    amount = 50.00m;
                    break;
                case 4:
                    amount = 100.00m;
                    break;
                case 5:
                    Console.WriteLine("Enter the amount you want to withdraw:");
                    amount = decimal.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Error. Have Some issue Your Operations.");
                    return;
            }

            if (amount > client.Card.CurrentBalance)
            {
                Console.WriteLine("Not enough money to withdraw. Please try again.");
            }
            else
            {
                client.Card.CurrentBalance -= amount;
                Console.WriteLine($"{amount} cekildi. Pul: {client.Card.CurrentBalance:C}");
            }
        }

        static void TransferCash(Client client, Bank bank)
        {
            Console.Write("Enter PAN For Transfer: ");
            string paninput = Console.ReadLine();
            bool FlagForPan = false;
            Client client2 = null;
            foreach (var item in bank.Clients)
            {
                if (item.Card.PAN == paninput)
                {
                    FlagForPan = true;
                    client2 = item;
                    break;
                }
            }

            if (!FlagForPan)
            {
                Console.WriteLine("Can't Find PAN. Try Again, Please.");
            }
            else
            {
                Console.Write("Enter the Amount: ");
                int amount = int.Parse(Console.ReadLine());

                if (amount > client.Card.CurrentBalance)
                {
                    Console.WriteLine("You Don't Have Enough Money. Try Again, Please.");
                }
                else
                {
                    client.Card.CurrentBalance -= amount;
                    client2.Card.CurrentBalance += amount;
                    Console.WriteLine("Transfer Successful");
                }
            }
        }

        static void Main(string[] args)
        {
            BankCard card1 = new BankCard("1111 2222 3333 4444", "1234", 123, new DateTime(2025, 12, 31), 6000.00m);
            BankCard card2 = new BankCard("5555 6666 7777 8888", "5678", 456, new DateTime(2024, 10, 31), 2000.00m);
            BankCard card3 = new BankCard("4141 3333 444 5123", "5678", 512, new DateTime(2025, 05, 05), 3000.00m);
            BankCard card4 = new BankCard("5555 6666 7777 1321", "5678", 762, new DateTime(2026, 10, 31), 6000.00m);
            BankCard card5 = new BankCard("3131 2412 1612 5123", "5678", 812, new DateTime(2025, 10, 31), 2000.00m);

            Client client1 = new Client("1", "Ulvi", "Poladov", 25, 3000, card1);
            Client client2 = new Client("2", "Nihat", "Qocalanev", 21, 4000, card2);
            Client client3 = new Client("3", "Ahmad", "Poladov", 19, 4000, card3);
            Client client4 = new Client("4", "Ilkin", "Agazada", 23, 4000, card4);
            Client client5 = new Client("5", "muhammad", "elhakim", 28, 4000, card5);

            Client[] clients = { client1, client2, client3, client4, client5 };

            Bank bank = new Bank("KapitalBank", 1, "28 May", clients);

            Client BaseClient = null;

            Console.Write("Enter your PAN: ");
            string enteredPAN = Console.ReadLine();
            foreach (var c in bank.Clients)
            {
                if (c.Card.PAN == enteredPAN)
                {
                    BaseClient = c;
                    break;
                }
            }

            if (BaseClient != null)
            {
                Console.Clear();
                Console.WriteLine($"Name {BaseClient.Name} \nSurname {BaseClient.Surname}");
                while (true)
                {
                    Console.WriteLine("Bank Operation Menu");
                    Console.WriteLine("1. Current Balance");
                    Console.WriteLine("2. Withdraw Cash");
                    Console.WriteLine("3. Transfer Cash");
                    Console.WriteLine("4. Exit");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"Your Current Balance: {BaseClient.Card.CurrentBalance:C}");
                            break;
                        case 2:
                            Console.Clear();
                            WithDrawCash(BaseClient);
                            break;
                        case 3:
                            Console.Clear();
                            TransferCash(BaseClient, bank);
                            break;
                        case 4:
                            Console.WriteLine("Exit...");
                            return;
                        default:
                            Console.WriteLine("Incorrect Choice. Try Again");
                            break;
                    }
                }
            }
        }
    }
}
