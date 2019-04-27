using AbstractRepairServiceDAL.BindingModel;
using AbstractRepairServiceDAL.Interfaces;
using AbstractRepairServiceDAL.ViewModel;
using AbstractRepairWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractRepairWorkServiceImplementDataBase.Implementations
{
    public class CustomerServiceDB : ICustomerService
    {
        private AbstractRepairDbContext context;

        public CustomerServiceDB(AbstractRepairDbContext context)
        {
            this.context = context;
        }

        public List<CustomerViewModel> ListGet()
        {
            List<CustomerViewModel> result = context.Customers.Select(rec => new
           CustomerViewModel
            {
                Id = rec.Id,
                CustomerFIO = rec.CustomerFIO,
                Mail = rec.Mail
            })
            .ToList();
            return result;
        }

        public CustomerViewModel ElementGet(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    Id = element.Id,
                    CustomerFIO = element.CustomerFIO,
                    Mail = element.Mail,
                    Messages = context.InfoMessages
                                 .Where(recM => recM.CustomerId == element.Id)
                                .Select(recM => new InfoMessageViewModel
                                {
                                    MessageId = recM.MessageId,
                                    DateDelivery = recM.DateDelivery,
                                    Subject = recM.Subject,
                                    Body = recM.Body
                                })
                                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerFIO ==
           model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Customers.Add(new Customer
            {
                CustomerFIO = model.CustomerFIO,
                Mail = model.Mail
            });
            context.SaveChanges();
        }

        public void UpdateElement(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerFIO ==
           model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Customers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
            element.Mail = model.Mail;
            context.SaveChanges();
        }

        public void DeleteElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Customers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
