using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var datasource = new SQLConnectionLib.SQLConnection())
            {
                foreach (var item in datasource.GetPayTypes())
                {
                    Console.WriteLine(item.payTypeName);
                }
            }
            Console.ReadLine();
        }
    }
}
