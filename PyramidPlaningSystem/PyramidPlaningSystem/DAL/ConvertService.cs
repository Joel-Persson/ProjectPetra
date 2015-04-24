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

        private readonly ApplicationDbContext _db;

        public ConvertService()
        {            
        }
        public ConvertService(ApplicationDbContext db)
        {
            _db = db;
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
            var childTodosModel = RetriveChildToDos(id);
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

        public List<ToDo> RetriveChildToDos(Guid id)
        {
            var childTodosModel = _db.ToDos.Where(x => x.ParentId == id && x.Deleted == false).ToList();
            return childTodosModel;
        }

    }
}