using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Positions")]
public class Position
{
    public int Id { get; set; }
    public string Title { get; set; }
}