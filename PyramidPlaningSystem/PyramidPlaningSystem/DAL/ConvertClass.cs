using System;
using System.Reflection;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.DAL
{
    public class ConvertClass
    {
        private ApplicationDbContext context = new ApplicationDbContext();
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
    }
}