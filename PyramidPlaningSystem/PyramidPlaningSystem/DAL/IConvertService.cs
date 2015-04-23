using System;
using System.Collections.Generic;
using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;

namespace PyramidPlaningSystem.DAL
{
    public interface IConvertService
    {
        Contact ContactConvert(ContactInfoViewModel model);
        List<ToDoViewModel> ConvertChildTodos(Guid id);
    }
}