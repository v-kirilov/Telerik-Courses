using System;

namespace UnitTestsDemo.Models
{
    public class BankAccount
    {
        private string customerName;
        private double balance;

        public BankAccount(string customerName, double balance)
        {
            this.CustomerName = customerName;
            this.balance = balance;
        }

        public string CustomerName
        {
            get
            {
                return this.customerName;
            }
            set
            {
                if (value.Length < 4 || value.Length > 10)
                {
                    throw new ArgumentException("Customer name should be between 4 and 10 chars in length");
                }
                this.customerName = value;
            }
        }

        public double Balance
        {
            get { return balance; }
        }

        public void Debit(double amount)
        {
            if (amount > this.balance)
            {
                throw new ArgumentOutOfRangeException("Not enough funds");
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Amount must be positive");
            }

            this.balance -= amount;
        }

        public void Credit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Amount must be positive");
            }

            this.balance += amount;
        }
    }
}
