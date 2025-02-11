﻿using System.Text.Json.Serialization;

namespace api.Models;

public class ProjectUser
{
    public int ProjectId { get; set; }

    [JsonIgnore]
    public Project Project { get; set; }

    public int UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }
}