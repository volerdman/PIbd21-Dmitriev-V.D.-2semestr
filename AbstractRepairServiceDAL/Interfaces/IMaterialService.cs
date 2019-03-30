using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.Interfaces
{
    public interface IMaterialService
    {
        List<MaterialViewModel> ListGet();

        MaterialViewModel ElementGet(int id);

        void AddElement(MaterialBindingModel model);

        void UpdateElement(MaterialBindingModel model);

        void DeleteElement(int id);
    }
}
