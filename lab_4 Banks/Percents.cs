using System;
using System.Collections.Generic;

namespace Banks
{
    internal class Percents
    {
        internal byte debit_percent;

        internal List<float> deposit_percent = new List<float>();
        internal List<uint> deposit_limit = new List<uint>();
        internal byte deposit_term;

        internal byte credit_commission;

        internal uint client_restriction;

        internal void Change_debit_percent(byte new_percent)
        {
            if (new_percent >= 0)
            {
                this.debit_percent = new_percent;
            }
            else
            {
                Console.WriteLine("ERROR percent");
            }
        }

        internal void Change_deposit_percent(List<float> new_percent)
        {
            if (new_percent.Count == this.deposit_percent.Count)
            {
                bool check = true;
                for (byte i = 0; i < new_percent.Count; i++)
                {
                    if (new_percent[i] >= 0)
                    {
                        continue;
                    }
                    else
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    this.deposit_percent = new_percent;
                }
                else
                {
                    Console.WriteLine("ERROR percent");
                }
            }
            else
            {
                Console.WriteLine("ERROR percent");
            }
        }
        internal void Change_deposit_limit(List<uint> sum)
        {
            if (sum.Count == this.deposit_limit.Count)
            {
                bool check = true;
                for (byte i = 0; i < sum.Count; i++)
                {
                    if (sum[i] > 0 & i > 0 & sum[i] > sum[i - 1])
                    {
                        continue;
                    }
                    else
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    this.deposit_limit = sum;
                }
                else
                {
                    Console.WriteLine("ERROR limits");
                }
            }
            else
            {
                Console.WriteLine("ERROR limit count");
            }
        }
        internal void Change_deposit_term(byte new_term)
        {
            if (new_term > 0)
            {
                this.deposit_term = new_term;
            }
            else
            {
                Console.WriteLine("ERROR term");
            }
        }

        internal void Change_credit_commission(byte new_commission)
        {
            if (new_commission >= 0)
            {
                this.credit_commission = new_commission;
            }
            else
            {
                Console.WriteLine("ERROR commission");
            }
        }

        internal void Change_client_restriction(uint client_restriction)
        {
            if (client_restriction > 0)
            {
                this.client_restriction = client_restriction;
            }
            else
            {
                Console.WriteLine("ERROR restriction");
            }
        }
    }
}
