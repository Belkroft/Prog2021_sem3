using System;
using System.Collections.Generic;

namespace Banks
{
    class Bank
    {
        internal string name;
        internal int bank_id;

        internal Percents b_percents;

        internal List<Client> client_list = new List<Client>();

        internal Bank(string c_name)
        {
            name = c_name;
        }

        internal void Add_client(Client client)
        {
            client_list.Add(client);
            if (!client.client_check)
            {
                client.limit = this.b_percents.client_restriction;
            }

            Debit debit = new Debit();

            client.debit_list.Add(debit);
            client.debit_banks.Add(bank_id);
        }
    }
}
