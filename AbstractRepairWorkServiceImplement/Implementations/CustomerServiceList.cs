﻿using AbstractRepairServiceDAL.BindingModel;
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
    public class CustomerServiceList : ICustomerService
    {
        private DataSingletonList source;
        public CustomerServiceList()
        {
            source = DataSingletonList.GetInstance();
        }
        public List<CustomerViewModel> ListGet()
        {
            List<CustomerViewModel> result = new List<CustomerViewModel>();
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                result.Add(new CustomerViewModel
                {
                    Id = source.Customers[i].Id,
                    CustomerFIO = source.Customers[i].CustomerFIO
                });
            }
            return result;
        }
        public CustomerViewModel ElementGet(int id)
        {
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].Id == id)
                {
                    return new CustomerViewModel
                    {
                        Id = source.Customers[i].Id,
                        CustomerFIO = source.Customers[i].CustomerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CustomerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].Id > maxId)
                {
                    maxId = source.Customers[i].Id;
                }
                if (source.Customers[i].CustomerFIO == model.CustomerFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.Customers.Add(new Customer
            {
                Id = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }
        public void UpdateElement(CustomerBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Customers[i].CustomerFIO == model.CustomerFIO &&
                source.Customers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Customers[index].CustomerFIO = model.CustomerFIO;
        }
        public void DeleteElement(int id)
        {
            for (int i = 0; i < source.Customers.Count; ++i)
            {
                if (source.Customers[i].Id == id)
                {
                    source.Customers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
