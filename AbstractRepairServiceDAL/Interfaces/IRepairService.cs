using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с услугами")]
    public interface IRepairService
    {
        [CustomMethod("Метод получения списка услуг")]
        List<RepairViewModel> ListGet();

        [CustomMethod("Метод получения услуги по id")]
        RepairViewModel ElementGet(int id);

        [CustomMethod("Метод добавления услуги")]
        void AddElement(RepairBindingModel model);

        [CustomMethod("Метод изменения данных по услуге")]
        void UpdateElement(RepairBindingModel model);

        [CustomMethod("Метод удаления услуги")]
        void DeleteElement(int id);
    }
}
