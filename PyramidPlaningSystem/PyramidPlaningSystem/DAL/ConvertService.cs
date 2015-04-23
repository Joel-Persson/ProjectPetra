using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PyramidPlaningSystem.API;
using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;
namespace PyramidPlaningSystem.DAL
{
    public class ConvertService : IConvertService
    {
        private readonly ToDoController _toDoController;

        //private ApplicationDbContext context = new ApplicationDbContext();

        public ConvertService()
        {            
        }
        public ConvertService(ToDoController toDoController)
        {
            _toDoController = toDoController;
        }
      
        public Contact ContactConvert(ContactInfoViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException();
            }

            foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
            {
                if (propertyInfo == null)
                {
                    throw new NullReferenceException();
                }
            }

            var contact = new Contact()
            {
                Address = model.Address,
                City = model.City,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Phone = model.Phone,
                ZipCode = model.ZipCode
            };

            return contact;
        }

        public List<ToDoViewModel> ConvertChildTodos(Guid id)
        {
            var childTodosModel = _toDoController.GetChildToDos(id);
            var childToDos = new List<ToDoViewModel>();

            if (childTodosModel != null && childTodosModel.Any())
            {
                foreach (var item in childTodosModel)
                {
                    var child = new ToDoViewModel
                    {
                        ToDo = item
                    };

                    childToDos.Add(child);
                }
            }
            return childToDos;
        }

    }
}