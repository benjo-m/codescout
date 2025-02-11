﻿using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Companies")]
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
}