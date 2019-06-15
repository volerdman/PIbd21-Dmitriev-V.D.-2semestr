using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractRepairWorkServiceImplementDataBase.Implementations
{
    public class MaterialServiceDB: IMaterialService
    {
        private AbstractRepairDbContext context;

        public MaterialServiceDB(AbstractRepairDbContext context)
        {
            this.context = context;
        }

        public List<MaterialViewModel> ListGet()
        {
            List<MaterialViewModel> result = context.Materials.Select(rec => new
           MaterialViewModel
            {
                Id = rec.Id,
                MaterialName = rec.MaterialName
            })
            .ToList();
            return result;
        }

        public MaterialViewModel ElementGet(int id)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MaterialViewModel
                {
                    Id = element.Id,
                    MaterialName = element.MaterialName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(MaterialBindingModel model)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.MaterialName ==
           model.MaterialName);
            if (element != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            context.Materials.Add(new Material
            {
                MaterialName = model.MaterialName
            });
            context.SaveChanges();
        }

        public void UpdateElement(MaterialBindingModel model)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.MaterialName ==
           model.MaterialName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            element = context.Materials.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.MaterialName = model.MaterialName;
            context.SaveChanges();
        }

        public void DeleteElement(int id)
        {
            Material element = context.Materials.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Materials.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
