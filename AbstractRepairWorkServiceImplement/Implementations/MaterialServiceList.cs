using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkServiceImplement.Implementations
{
    public class MaterialServiceList:IMaterialService
    {
        private DataSingletonList source;
        public MaterialServiceList()
        {
            source = DataSingletonList.GetInstance();
        }
        public List<MaterialViewModel> ListGet()
        {
            List<MaterialViewModel> result = new List<MaterialViewModel>();
            for (int i = 0; i < source.Materials.Count; ++i)
            {
                result.Add(new MaterialViewModel
                {
                    Id = source.Materials[i].Id,
                    MaterialName = source.Materials[i].MaterialName
                });
            }
            return result;
        }
        public MaterialViewModel ElementGet(int id)
        {
            for (int i = 0; i < source.Materials.Count; ++i)
            {
                if (source.Materials[i].Id == id)
                {
                    return new MaterialViewModel
                    {
                        Id = source.Materials[i].Id,
                        MaterialName = source.Materials[i].MaterialName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(MaterialBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Materials.Count; ++i)
            {
                if (source.Materials[i].Id > maxId)
                {
                    maxId = source.Materials[i].Id;
                }
                if (source.Materials[i].MaterialName == model.MaterialName)
                {
                    throw new Exception("Уже есть материал с таким названием");
                }
            }
            source.Materials.Add(new Material
            {
                Id = maxId + 1,
                MaterialName = model.MaterialName
            });
        }
        public void UpdateElement(MaterialBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Materials.Count; ++i)
            {
                if (source.Materials[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Materials[i].MaterialName == model.MaterialName &&
                source.Materials[i].Id != model.Id)
                {
                    throw new Exception("Уже есть материал с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Materials[index].MaterialName = model.MaterialName;
        }
        public void DeleteElement(int id)
        {
            for (int i = 0; i < source.Materials.Count; ++i)
            {
                if (source.Materials[i].Id == id)
                {
                    source.Materials.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
