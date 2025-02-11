using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models;

[Table("Addresses")]
public class Address
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }

    public bool isValidAddress()
    {
        return !String.IsNullOrWhiteSpace(this.Country)
            && !String.IsNullOrWhiteSpace(this.City)
            && !String.IsNullOrWhiteSpace(this.Street)
            && !String.IsNullOrWhiteSpace(this.PostalCode);
    }
}