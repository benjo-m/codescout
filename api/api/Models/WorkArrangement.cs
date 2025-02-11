using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("WorkArrangements")]
public class WorkArrangement
{
    public int Id { get; set; }
    public string Name { get; set; }
}
