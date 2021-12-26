using System;
using System.Threading;

namespace Banks
{
    class Program
    {
        static void Main(string[] args)
        {
            Central_bank central = new Central_bank();

            central.New_bank("Imperial");
            central.New_bank("MMM");

            Client friend1 = new Client("Max", "Gold", false);
            central.bank_list[0].Add_client(friend1);

            Client friend2 = new Client("Nina", "Nano", "", "", true);
            central.bank_list[1].Add_client(friend2);

            //INTERFACE
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your surname: ");
            string surname = Console.ReadLine();

            Client client = new Client();

            bool check = true;
            while (check) {
                Console.Write("Would you like to enter your address and passport information?\n");
                string answer = Console.ReadLine();
                if (answer == "yes" || answer == "Yes")
                {
                    Console.Write("Enter your address: ");
                    string address = Console.ReadLine();
                    Console.Write("Enter your passport information: ");
                    string passport = Console.ReadLine();
                    client.name = name;
                    client.surname = surname;
                    client.address = address;
                    client.passport_info = passport;
                    client.client_check = true;
                    check = false;
                }
                else if (answer == "no" || answer == "No")
                {
                    client.name = name;
                    client.surname = surname;
                    client.client_check = false;
                    check = false;
                }
                else
                {
                    Console.WriteLine("Wrong answer");
                }
            }

            Console.Write("Choose bank:\n");
            for (byte i = 0; i < central.bank_list.Count; i++)
            {
                Console.WriteLine((i + 1).ToString() + ") " + central.bank_list[i].name);
            }

            bool check_num = true;
            string bank_num = "";
            while (check_num)
            {
                bank_num = Console.ReadLine();
                if (ushort.Parse(bank_num) >= 1 & ushort.Parse(bank_num) <= central.bank_list.Count)
                {
                    central.bank_list[ushort.Parse(bank_num) - 1].Add_client(client);
                    check_num = false;
                }
                else
                {
                    Console.WriteLine("Wrong bank num");
                }
            }
            client.Client_info();

            //TIMER
            check = true;
            ushort day = 0;
            byte month = 0;
            Console.WriteLine("0.05 second = 1 day");
            while (check)
            {
                Thread.Sleep(50);

                day++;
                Console.WriteLine("DAY " + day);

                for (int i = 0; i < central.bank_list.Count; i++)
                {
                    for (int j = 0; j < central.bank_list[i].client_list.Count; j++)
                    {
                        for (int x = 0; x < central.bank_list[i].client_list[j].debit_list.Count; x++)
                        {
                            central.bank_list[i].client_list[j].debit_list[x].sum_plus += central.bank_list[i].client_list[j].debit_list[x].sum
                                * ((central.bank_list[i].b_percents.debit_percent+ 100)/100);
                            if (month == 30)
                            {
                                central.bank_list[i].client_list[j].debit_list[x].sum += central.bank_list[i].client_list[j].debit_list[x].sum_plus;
                                Console.WriteLine("Debit " + central.bank_list[i].client_list[j].name + " " + central.bank_list[i].client_list[j].surname
                                    + " change " + central.bank_list[i].name + ": "
                                    + central.bank_list[i].client_list[j].debit_list[x].sum.ToString());
                            }
                            
                        }
                        for (int x = 0; x < central.bank_list[i].client_list[j].deposit_list.Count; x++)
                        {
                            central.bank_list[i].client_list[j].deposit_list[x].term -= 1;
                            if (central.bank_list[i].client_list[j].deposit_list[x].term == 0)
                            {
                                int v_id = central.bank_list[i].b_percents.deposit_limit.Count - 1;
                                for (int v = 0; v < central.bank_list[i].b_percents.deposit_limit.Count; v++)
                                {
                                    if (central.bank_list[i].client_list[j].deposit_list[x].sum < central.bank_list[i].b_percents.deposit_limit[v])
                                    {
                                        v_id = v;
                                    }
                                }
                                central.bank_list[i].client_list[j].deposit_list[x].sum *= (central.bank_list[i].b_percents.deposit_percent[v_id] + 100) / 100;
                            }
                        }
                    }
                }

                month++;
                if (month == 30)
                {
                    month = 0;
                    string step = "";
                    while (step != "7")
                    {
                        client.Client_info();

                        Console.WriteLine("End of month. Next step: ");
                        Console.WriteLine("1)Add debit");
                        Console.WriteLine("2)Add deposit");
                        Console.WriteLine("3)Add credit");
                        Console.WriteLine("4)Add_money");
                        Console.WriteLine("5)Take money");
                        Console.WriteLine("6)Transit");
                        Console.WriteLine("7)Next");
                        Console.WriteLine("8)Exit programm");
                        step = Console.ReadLine();

                        if (step == "1")
                        {
                            client.Add_debit();
                        }
                        else if (step == "2")
                        {
                            client.Add_deposit(central.bank_list[ushort.Parse(bank_num) - 1]);
                        }
                        else if (step == "3")
                        {
                            client.Add_credit();
                        }
                        else if (step == "4")
                        {
                            Console.WriteLine("\n Money sum: ");
                            string s_money = Console.ReadLine();
                            float f_money = float.Parse(s_money);
                            client.debit_list[0].Debit_add_money(f_money, client);
                        }
                        else if (step == "5")
                        {
                            Console.WriteLine("\n Money sum: ");
                            string s_money = Console.ReadLine();
                            float f_money = float.Parse(s_money);
                            client.debit_list[0].Debit_take_money(f_money, client);
                        }
                        else if (step == "6")
                        {
                            Console.WriteLine("\n Money sum: ");
                            string s_money = Console.ReadLine();
                            float f_money = float.Parse(s_money);
                            client.debit_list[0].Debit_transit_debit(f_money, friend1.debit_list[0], client);
                        }
                        else if (step == "8")
                        {
                            check = false;
                            step = "7";
                        }
                    }
                }
            }
        } 
    }
}