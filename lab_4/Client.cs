using System;
using System.Collections.Generic;

namespace Banks
{
    internal class Client
    {
        internal string name;
        internal string surname;

        internal string address;
        internal string passport_info;

        internal bool client_check;
        internal float limit = -1;

        internal List<Debit> debit_list = new List<Debit>();
        internal List<int> debit_banks = new List<int>();

        internal List<Deposit> deposit_list = new List<Deposit>();
        internal List<int> deposit_banks = new List<int>();

        internal List<Credit> credit_list = new List<Credit>();
        internal List<int> credit_banks = new List<int>();

        internal Client() { }
        internal Client(string c_name, string c_surname, bool check)
        {
            name = c_name;
            surname = c_surname;
            client_check = check;
        }
        internal Client(string c_name, string c_surname, string c_address, string c_passport, bool check)
        {
            name = c_name;
            surname = c_surname;
            address = c_address;
            passport_info = c_passport;
            client_check = check;
        }

        internal void Client_info()
        {
            Console.WriteLine(this.name + " " + this.surname);

            for (int i = 0; i < debit_list.Count; i++)
            {
                Console.WriteLine("Debit: " + this.debit_list[i].sum);
            }
            for (int i = 0; i < deposit_list.Count; i++)
            {
                Console.WriteLine("Deposit: " + this.deposit_list[i].sum);
            }
            for (int i = 0; i < credit_list.Count; i++)
            {
                    Console.WriteLine("Credi: " + this.credit_list[i].sum);
            }
        }
        
        internal void Add_debit()
        {
            Debit debit = new Debit();
            this.debit_list.Add(debit);
        }
        internal void Add_deposit(Bank bank)
        {
            Deposit deposit = new Deposit();
            deposit.term = bank.b_percents.deposit_term;

            this.deposit_list.Add(deposit);
        }
        internal void Add_credit()
        {
            Credit credit = new Credit();
            this.credit_list.Add(credit);
        }
    }
}