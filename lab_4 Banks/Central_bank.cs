using System;
using System.Collections.Generic;

namespace Banks
{
    internal class Central_bank
    {
        Percents c_percents = new Percents();

        internal List<Bank> bank_list = new List<Bank>();

        internal Central_bank()
        {
            this.c_percents.debit_percent = 1;

            this.c_percents.deposit_percent = new List<float>() { 3, 3, 5, 4 };
            this.c_percents.deposit_limit = new List<uint>() { 50000, 100000 };
            this.c_percents.deposit_term = 3;

            this.c_percents.credit_commission = 5;

            this.c_percents.client_restriction = 100000;
        }

        internal void New_bank(string name)
        {
            Bank bank = new Bank(name);
            bank.b_percents = c_percents;
            bank_list.Add(bank);
            bank.bank_id = bank_list.Count - 1;
        }
    }
}