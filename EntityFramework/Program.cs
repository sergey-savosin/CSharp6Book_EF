using AutoLotDAL.EF;
using AutoLotDAL.Models;
using AutoLotDAL.Repos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new DataInitializer());
            Console.WriteLine("**** Fun with ADO.NET EF Code First ****\n");

            //PrintAllInventory();

            PrintAllCustomerAndCreditRisks();
            var customerRepo = new CustomerRepo();
            var customer = customerRepo.GetOne(5);
            customerRepo.Context.Entry(customer).State = EntityState.Detached;
            var risk = MakeCustomerARisk(customer);
            PrintAllCustomerAndCreditRisks();

            Console.ReadLine();
        }

        #region Helpers

        /// <summary>
        /// Helper: print all inventory to console
        /// </summary>
        private static void PrintAllInventory()
        {
            using (var repo = new InventoryRepo())
            {
                foreach (Inventory c in repo.GetAll())
                {
                    Console.WriteLine(c);
                }
            }
        }

        /// <summary>
        /// Helper: add a car
        /// </summary>
        /// <param name="car"></param>
        private static void AddNewRecord(Inventory car)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Add(car);
            }
        }

        /// <summary>
        /// Helper: add list of car
        /// </summary>
        /// <param name="cars"></param>
        private static void AddNewRecords(IList<Inventory> cars)
        {
            using (var repo = new InventoryRepo())
            {
                repo.AddRange(cars);
            }
        }

        private static void UpdateRecord(int carId)
        {
            using (var repo = new InventoryRepo())
            {
                var car = repo.GetOne(carId);
                if (car != null)
                {
                    Console.WriteLine("Before change: " + repo.Context.Entry(car).State);
                    car.Color = "Blue";
                    Console.WriteLine("After change: " + repo.Context.Entry(car).State);
                    repo.Save(car);
                    Console.WriteLine("After save: " + repo.Context.Entry(car).State);
                }
            }
        }

        private static void ShowAllOrders()
        {
            using (var repo = new OrderRepo())
            {
                Console.WriteLine("--- Pending orders ---");
                foreach(var item in repo.GetAll())
                {
                    Console.WriteLine($"->{item.Customer.FullName} is waiting on {item.Car.PetName}");
                }
            }
        }

        private static void ShowAllOrdersEagerlyFetched()
        {
            using (var context = new AutoLotEntities())
            {
                Console.WriteLine("--- Pending orders (eager load) ---");
                //context.Database.Log = Console.WriteLine;
                var orders = context.Orders
                    .Include(x => x.Customer)
                    .Include(y => y.Car)
                    .ToList();
                foreach(var item in orders)
                {
                    Console.WriteLine($"->{item.Customer.FullName} is waiting on {item.Car.PetName}");
                }
            }
        }

        private static CreditRisk MakeCustomerARisk(Customer customer)
        {
            var creditRisk = new CreditRisk()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
            var creditRiskDupe = new CreditRisk()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            using (var context = new AutoLotEntities())
            {
                context.Customers.Attach(customer);
                context.Customers.Remove(customer);

                context.CreditRisks.Add(creditRisk);
                context.CreditRisks.Add(creditRiskDupe);
                try
                {
                    context.SaveChanges();
                }
                catch(DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return creditRisk;
        }

        private static void PrintAllCustomerAndCreditRisks()
        {
            Console.WriteLine("----- Customers -----");
            using (var repo = new CustomerRepo())
            {
                foreach(var cust in repo.GetAll())
                {
                    Console.WriteLine($"->{cust.FirstName} {cust.LastName} is a Customer");
                }
            }

            Console.WriteLine("----- Credit risks -----");
            using (var repo = new CreditRiskRepo())
            {
                foreach(var risk in repo.GetAll())
                {
                    Console.WriteLine($"-> {risk.FirstName} {risk.LastName} is a Credit risk!");
                }
            }
        }

        #endregion Helpers

    }
}
