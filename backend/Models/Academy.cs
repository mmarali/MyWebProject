using System.ComponentModel.DataAnnotations;

namespace Sylabus.Models;

public class Academy
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string City { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Street { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Postcode { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Country { get; set; }
}