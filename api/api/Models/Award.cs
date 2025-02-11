using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models;

[Table("Awards")]
public class Award
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public List<User>? Users { get; set; }
}