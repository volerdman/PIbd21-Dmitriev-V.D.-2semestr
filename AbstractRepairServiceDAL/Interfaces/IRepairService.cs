using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.Interfaces
{
    public interface IRepairService
    {
        List<RepairViewModel> ListGet();

        RepairViewModel ElementGet(int id);

        void AddElement(RepairBindingModel model);

        void UpdateElement(RepairBindingModel model);

        void DeleteElement(int id);
    }
}
