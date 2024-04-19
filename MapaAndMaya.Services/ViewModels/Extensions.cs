﻿using MapaAndMaya.Services.Models;

namespace MapaAndMaya.Services.ViewModels;

public static class Extensions
{
    public static void CopyToEntity(this FacultyViewModel model, Faculty entity)
    {
        entity.Id = model.Id;
        entity.Name = model.Name;
    }
}