using AbstractRepairServiceDAL.Attributies;
using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractRepairServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с материалами")]
    public interface IMaterialService
    {
        [CustomMethod("Метод получения списка материалов")]
        List<MaterialViewModel> ListGet();

        [CustomMethod("Метод получения материала по id")]
        MaterialViewModel ElementGet(int id);

        [CustomMethod("Метод добавления материалу")]
        void AddElement(MaterialBindingModel model);

        [CustomMethod("Метод изменения данных по материалу")]
        void UpdateElement(MaterialBindingModel model);

        [CustomMethod("Метод удаления материала")]
        void DeleteElement(int id);
    }
}
