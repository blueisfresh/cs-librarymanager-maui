using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementMaui.Models;
public class Borrow
{
    [Key]
    public int BorrowID { get; set; } // Unique ID for each borrow transaction

    [Required]
    public int StudentLibraryCardNum { get; set; } // Foreign key referencing Student.LibraryCardNum

   

    [Required]
    [StringLength(10)]
    public string BookBookNum { get; set; } // Foreign key referencing Book.BookNum
    [Required]
    [DataType(DataType.Date)]
    public DateTime BorrowDate { get; set; } // Date the book was borrowed

    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; } // Date the book was returned

    [Required]
    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; } // Due date for returning the book



    [ForeignKey(nameof(BookBookNum))]
    public virtual Book? Book { get; set; } // Navigation Property

    [ForeignKey(nameof(StudentLibraryCardNum))]
    public virtual Student? Student { get; set; } // Navigation Property

}
