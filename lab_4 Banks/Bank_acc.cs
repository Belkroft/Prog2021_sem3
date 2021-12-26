using System;

namespace Banks
{
    internal class Debit
    {
        internal float sum = 0;
        internal float sum_plus = 0;

        internal void Debit_add_money(float money, Client client)
        {
            if (money > 0 & (client.limit > money || client.limit == -1))
            {
                sum += money;
                if (client.limit != -1)
                {
                    client.limit -= money;
                }
            }
            else
            {
                Console.WriteLine("ERROR money num");
            }
        }
        internal void Debit_take_money(float money, Client client)
        {
            if (sum >= money & (client.limit > money || client.limit == -1))
            {
                sum -= money;
                if (client.limit != -1)
                {
                    client.limit -= money;
                }
            }
            else
            {
                Console.WriteLine("ERROR money num");
            }
        }
        private void Debit_transit(float money, float sum_from, float sum_to, Client client)
        {
            if (sum_from >= money & (client.limit > money || client.limit == -1))
            {
                sum_from -= money;
                if (client.limit != -1)
                {
                    client.limit -= money;
                }
                sum_to += money;
            }
            else
            {
                Console.WriteLine("ERROR money num");
            }
        }
        internal void Debit_transit_debit(float money, Debit diff_debit, Client client)
        {
            Debit_transit(money, this.sum, diff_debit.sum, client);
        }
        internal void Debit_transit_deposit(float money, Deposit diff_debit, Client client)
        {
            Debit_transit(money, this.sum, diff_debit.sum, client);
        }
        internal void Debit_transit_credit(float money, Credit diff_debit, Client client)
        {
            Debit_transit(money, this.sum, diff_debit.sum, client);
        }
    }

    internal class Deposit
    {
        internal bool check = false;

        internal ushort term;
        internal float sum = 0;

        internal void Deposit_add_money(float money, Client client)
        {
            if (money > 0 & (client.limit > money || client.limit == -1))
            {
                sum += money;
                if (client.limit != -1)
                {
                    client.limit -= money;
                }
            }
            else
            {
                Console.WriteLine("ERROR money num");
            }
        }
        internal void Deposit_take_money(float money, Client client)
        {
            if (sum >= money & this.check & (client.limit > money || client.limit == -1))
            {
                sum -= money;
                if (client.limit != -1)
                {
                    client.limit -= money;
                }
            }
            else
            {
                Console.WriteLine("ERROR money num");
            }
        }
        private void Debit_transit(float money, float sum_from, float sum_to, Client client)
        {
            if (sum_from >= money & this.check & (client.limit > money || client.limit == -1))
            {
                sum_from -= money;
                if (client.limit != -1)
                {
                    client.limit -= money;
                }
                sum_to += money;
            }
            else
            {
                Console.WriteLine("ERROR money num");
            }
        }
        internal void Deposit_transit_debit(float money, Debit diff_debit, Client client)
        {
            Debit_transit(money, this.sum, diff_debit.sum, client);
        }
        internal void Deposit_transit_deposit(float money, Deposit diff_debit, Client client)
        {
            Debit_transit(money, this.sum, diff_debit.sum, client);
        }
        internal void Deposit_transit_credit(float money, Credit diff_debit, Client client)
        {
            Debit_transit(money, this.sum, diff_debit.sum, client);
        }
    }

    internal class Credit
    {
        internal float sum = 0;

        internal void Credit_add_money(float money, Client client)
        {
            if (money > 0 & (client.limit > money || client.limit == -1))
            {
                sum += money;
                if (client.limit != -1)
                {
                    client.limit -= money;
                }
            }
            else
            {
                Console.WriteLine("ERROR money num");
            }
        }
        internal void Credit_take_money(float money, Client client)
        {
            sum -= money;
            if (client.limit != -1)
            {
                client.limit -= money;
            }
        }
        private void Credit_transit(float money, float sum_from, float sum_to, Client client)
        {
            sum_from -= money;
            if (client.limit != -1)
            {
                client.limit -= money;
            }
            sum_to += money;
        }
        internal void Credit_transit_debit(float money, Debit diff_debit, Client client)
        {
            Credit_transit(money, this.sum, diff_debit.sum, client);
        }
        internal void Credit_transit_deposit(float money, Deposit diff_debit, Client client)
        {
            Credit_transit(money, this.sum, diff_debit.sum, client);
        }
        internal void Credit_transit_credit(float money, Credit diff_debit, Client client)
        {
            Credit_transit(money, this.sum, diff_debit.sum, client);
        }
    }
}