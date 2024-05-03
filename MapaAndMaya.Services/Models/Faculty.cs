﻿using System.ComponentModel.DataAnnotations;


namespace MapaAndMaya.Services.Models;

public class Faculty
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public ICollection<Degree> Degrees { get; } = new List<Degree>();
}