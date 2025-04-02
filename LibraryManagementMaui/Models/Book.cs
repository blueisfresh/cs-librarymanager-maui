using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementMaui.Models;
public class Book
{
    [Key]
    [StringLength(10)]
    public string BookNum { get; set; } // e.g., "00001-2023"

    [Required]
    [StringLength(100)]
    public string Title { get; set; } // Title of the book

    [Required]
    [StringLength(50)]
    public string Author { get; set; } // Author's name

    [Required]
    [StringLength(50)]
    public string Publisher { get; set; } // Publisher's name

    [Required]
    [RegularExpression(@"\d{13}")]
    public string ISBN { get; set; } // ISBN, fixed-length 13

    [Required]
    [StringLength(50)]
    public string PublicationPlace { get; set; } // Place of publication

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublicationDate { get; set; } // Date the book was published
}
