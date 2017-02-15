namespace AutoLotDAL.EF
{
    using AutoLotDAL.Models;
    using Interception;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Linq;

    public class AutoLotEntities : DbContext
    {
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            //DbInterception.Add(new ConsoleWriterInterceptor());
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }

}