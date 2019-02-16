using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairsWorkServiceImplement.Implementations;
using AbstractRepairWorkServiceImplement.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            currentContainer.RegisterType<ICustomerService, CustomerServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialService, MaterialServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRepairService, RepairServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceMain, ServiceMainList>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
