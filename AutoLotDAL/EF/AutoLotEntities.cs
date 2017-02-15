namespace AutoLotDAL.EF
{
    using AutoLotDAL.Models;
    using Interception;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Linq;

    public class AutoLotEntities : DbContext
    {
        static readonly DatabaseLogger DatabaseLogger = new DatabaseLogger("sqllog.txt", true);
        public AutoLotEntities() : base("name=AutoLotConnection")
        {
            //DbInterception.Add(new ConsoleWriterInterceptor());

            //DatabaseLogger.StartLogging();
            //DbInterception.Add(DatabaseLogger);

            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += OnObjectMaterialized;
            context.SavingChanges += OnSavingChanges;
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        protected override void Dispose (bool disposing)
        {
            //DbInterception.Remove(DatabaseLogger);
            //DatabaseLogger.StopLogging();
            base.Dispose(disposing);
        }
        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {

        }
        private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {

        }
    }

}