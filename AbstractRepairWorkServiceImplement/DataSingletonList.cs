using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkServiceImplement
{
    public class DataSingletonList
    {
        private static DataSingletonList instance;

        public List<Customer> Customers { get; set; }

        public List<Material> Materials { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Repair> Repairs { get; set; }

        public List<MaterialRepair> MaterialRepairs { get; set; }

        public List<Storage> Storages { get; set; }

        public List<StorageMaterial> StorageMaterials { get; set; }

        private DataSingletonList()
        {
            Customers = new List<Customer>();
            Materials = new List<Material>();
            Bookings = new List<Booking>();
            Repairs = new List<Repair>();
            MaterialRepairs = new List<MaterialRepair>();
            Storages = new List<Storage>();
            StorageMaterials = new List<StorageMaterial>();
        }

        public static DataSingletonList GetInstance()
        {
            if (instance == null)
            {
                instance = new DataSingletonList();
            }
            return instance;
        }

    }
}
