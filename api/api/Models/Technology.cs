using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models;

[Table("Technologies")]
public class Technology
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore]
    public List<Project> Projects { get; set; }
}
