using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.Interfaces
{
    public interface IInfoMessageService
    {
        List<InfoMessageViewModel> GetList();

        InfoMessageViewModel GetElement(int id);

        void AddElement(InfoMessageBindingModel model);
    }
}
