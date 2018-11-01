using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse
{
    public class Entities : DbContext
    {
        internal class EfDbMigrationConfiguration : DbMigrationsConfiguration<Entities>
        {
            public EfDbMigrationConfiguration()
            {
                AutomaticMigrationsEnabled = true;
                AutomaticMigrationDataLossAllowed = true;
            }
        }
        static Entities()
        {
            Database.SetInitializer<Entities>(new MigrateDatabaseToLatestVersion<Entities, EfDbMigrationConfiguration>());
        }
        public Entities()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public void Save<TEntity>(TEntity entity) where TEntity : ModelBase
        {
            var entry = this.Entry(entity);
            if (entity.Id > 0)
            {
                entity.UpdatedOn = DateTime.Now;
                if (entry.State == EntityState.Detached)
                {
                    TEntity entityToUpdate = this.Set<TEntity>().Find(entity.Id);
                    EmitMapper.ObjectMapperManager.DefaultInstance.GetMapper<TEntity, TEntity>().Map(entity, entityToUpdate);
                }
            }
            else
            {
                this.Set<TEntity>().Add(entity);
            }
            this.SaveChanges();
        }
        public void Delete<TEntity>(IList<int> ids) where TEntity : ModelBase
        {
            foreach (int id in ids)
            {
                TEntity entity = this.Set<TEntity>().Find(id);
                var entry = this.Entry<TEntity>(entity);
                entry.State = EntityState.Deleted;
            }
            this.SaveChanges();
        }
        public void Delete<TEntity>(int id) where TEntity : ModelBase
        {
            var tmp = this.Set<TEntity>().Find(id);
            
            var entry = this.Entry<TEntity>(tmp);
            
            entry.State = EntityState.Deleted;
            this.SaveChanges();
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<WarehouseIn> WarehouseIn { get; set; }
        public DbSet<WarehouseInItem> WarehouseInItem { get; set; }
        public DbSet<WarehouseOut> WarehouseOut { get; set; }
        public DbSet<WarehouseOutItem> WarehouseOutItem { get; set; }
        //public DbSet<RepairedBill> RepairedBill { get; set; }
        //public DbSet<RepairedBillItem> RepairedBillItem { get; set; }
    }
}
