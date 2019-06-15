using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairWorkServiceImplementDataBase;
using AbstractRepairWorkServiceImplementDataBase.Implementations;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractRepairWorkView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractRepairDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialService, MaterialServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRepairService, RepairServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceMain, ServiceMainDB>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
