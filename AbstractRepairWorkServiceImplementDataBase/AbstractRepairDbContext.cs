using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkServiceImplementDataBase
{
    public class AbstractRepairDbContext : DbContext
    {
        public AbstractRepairDbContext() : base("AbstractRepairDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Material> Materials { get; set; }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<Repair> Repairs { get; set; }

        public virtual DbSet<Executor> Executors { get; set; }

        public virtual DbSet<MaterialRepair> MaterialRepairs { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<StorageMaterial> StorageMaterials { get; set; }

        public virtual DbSet<InfoMessage> InfoMessages { get; set; }
    }
}
