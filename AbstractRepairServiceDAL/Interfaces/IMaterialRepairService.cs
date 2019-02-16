using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.Interfaces
{
    public interface IMaterialRepairRepairService
    {
        List<MaterialRepairViewModel> ListGet();

        MaterialRepairViewModel ElementGet(int id);

        void AddElement(MaterialRepairBindingModel model);

        void UpdateElement(MaterialRepairBindingModel model);

        void DeleteElement(int id);
    }
}
