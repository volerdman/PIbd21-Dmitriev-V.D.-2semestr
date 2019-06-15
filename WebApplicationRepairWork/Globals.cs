using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairsWorkServiceImplement.Implementations;
using AbstractRepairWorkServiceImplement.Implementations;
using AbstractRepairWorkServiceImplementDataBase;
using AbstractRepairWorkServiceImplementDataBase.Implementations;

namespace WebApplicationRepairWork
{
    public class Globals
    {
        public static AbstractRepairDbContext DbContext { get; } = new AbstractRepairDbContext();
        public static ICustomerService CustomerService { get; } = new CustomerServiceDB(DbContext);
        public static IMaterialService MaterialService { get; } = new MaterialServiceDB(DbContext);
        public static IRepairService RepairService { get; } = new RepairServiceDB(DbContext);
        public static IServiceMain MainService { get; } = new ServiceMainDB(DbContext);
        public static IStorageService StorageService { get; } = new StorageServiceDB(DbContext);
    }
}