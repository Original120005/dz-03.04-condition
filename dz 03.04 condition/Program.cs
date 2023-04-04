using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz03_04_condition
{
    abstract class State
    {
        public Account account;
        public double balance;
        public double LowerLimit;
        public double UpperLimit;

        public double GetBalance()
        {
            return balance;
        }
        public void SetBalance()
        {
            Console.Write("Enter balance: ");
            string b = Console.ReadLine();
            balance = Convert.ToDouble(b);
        }

        public abstract void Deposit();
        public abstract void Withdraw();
    }


    class Account
    {
        public string owner { get; set; }
        public State state;

        public Account(string owner)
        {
            this.owner = owner;
        }

        public void SetState(State state)
        {
            this.state = state;
        }
        public void Deposit()
        {
            Console.Write("Enter the replenishment amount: ");
            string n = Console.ReadLine();
            state.balance += Convert.ToDouble(n);
            Console.WriteLine("Refill completed successfully...");
        }
        public void GetBalance()
        {
            Console.WriteLine($"Balance: {state.balance}");
        }
        public void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            string s = Console.ReadLine();
            state.balance -= Convert.ToDouble(s);
        }
    }


    class RedState : State
    {
        public RedState(double balance, Account account)
        {
            this.balance = balance;
            this.account = account;
            Initialize();
        }

        public void StateChange()
        {
            if (balance > UpperLimit)
            {
                account.SetState(new SilverState(balance, account));
            }
        }
        public void Initialize()
        {
            LowerLimit = -100.0;
            UpperLimit = 0.0;
        }
        public override void Deposit()
        {
            Console.Write("Enter the replenishment amount: ");
            string n = Console.ReadLine();
            balance += Convert.ToDouble(n);
            Console.WriteLine("Refill completed successfully...");
        }
        public override void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            string s = Console.ReadLine();
            balance -= Convert.ToDouble(s);
        }
    }

    class SilverState : State
    {
        public SilverState(double balance, Account account)
        {
            this.balance = balance;
            this.account = account;
            Initialize();
        }

        public void StateChange()
        {
            if (balance < LowerLimit)
            {
                account.SetState(new RedState(balance, account));
            }
            else if (balance > UpperLimit)
            {
                account.SetState(new GoldState(balance, account));
            }
        }
        public void Initialize()
        {
            LowerLimit = 0.0;
            UpperLimit = 1000.0;
        }
        public override void Deposit()
        {
            Console.Write("Enter the replenishment amount: ");
            string n = Console.ReadLine();
            balance += Convert.ToDouble(n);
            Console.WriteLine("Refill completed successfully...");
        }
        public override void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            string s = Console.ReadLine();
            balance -= Convert.ToDouble(s);
        }
    }

    class GoldState : State
    {
        public GoldState(double balance, Account account)
        {
            this.balance = balance;
            this.account = account;
            Initialize();
        }

        public void StateChange()
        {
            if (balance < 0.0)
            {
                account.SetState(new RedState(balance, account));
            }
            else if (balance < LowerLimit)
            {
                account.SetState(new SilverState(balance, account));
            }
        }
        public void Initialize()
        {
            LowerLimit = 1000.0;
            UpperLimit = 100000000.0;
        }
        public override void Deposit()
        {
            Console.Write("Enter the replenishment amount: ");
            string n = Console.ReadLine();
            balance += Convert.ToDouble(n);
            Console.WriteLine("Refill completed successfully...");
        }
        public override void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            string s = Console.ReadLine();
            balance -= Convert.ToDouble(s);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("bank");
            account.SetState(new SilverState(0.0, account));
            account.Deposit();




        }
    }
}
