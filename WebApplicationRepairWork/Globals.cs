using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairsWorkServiceImplement.Implementations;
using AbstractRepairWorkServiceImplement.Implementations;

namespace WebApplicationRepairWork
{
    public class Globals
    {
        public static ICustomerService CustomerService { get; } = new CustomerServiceList();
        public static IMaterialService MaterialService { get; } = new MaterialServiceList();
        public static IRepairService RepairService { get; } = new RepairServiceList();
        public static IServiceMain MainService { get; } = new ServiceMainList();
        public static IStorageService StorageService { get; } = new StorageServiceList();
    }
}