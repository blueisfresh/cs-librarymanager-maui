using System.ComponentModel.DataAnnotations;

namespace LibraryManagementMaui.Models;
public class Student
{
    [Key]
    [Required]
    [Range(100000, 999999)]
    public int LibraryCardNum { get; set; } // Unique 6-digit library card number

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } // First name of the student

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } // Last name of the student
}
