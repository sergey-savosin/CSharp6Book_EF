using AutoLotDAL.EF;
using AutoLotDAL.Models;
using AutoLotDAL.Repos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DataInitializer());
            Console.WriteLine("**** Fun with ADO.NET EF Code First ****\n");
            PrintAllInventory();
            Console.ReadLine();
        }

        private static void PrintAllInventory()
        {
            using (var repo = new InventoryRepo())
            {
                foreach(Inventory c in repo.GetAll())
                {
                    Console.WriteLine(c);
                }
            }
        }
    }
}
