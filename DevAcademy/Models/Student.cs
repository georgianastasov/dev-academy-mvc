using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevAcademy.Models
{
    public class Student
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Username")]
        [StringLength(50, MinimumLength = 6)]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-mail is not valid.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [DisplayName("Account Type")]
        public string? AccountType { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm the Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Created Date")]
        public string? CreatedDate { get; set; }

        [StringLength(300, MinimumLength = 6)]
        public string? Bio { get; set; }

        public int? Points { get; set; }

        public int? Progress { get; set; }

        public string? TimeSpent { get; set; }

        //------------------------------------------------------
        public virtual List<Course>? Courses { get; set; }

        //------------------------------------------------------

        [StringLength(2000)]
        public string? CoursesIDs { get; set; }
    }
}
