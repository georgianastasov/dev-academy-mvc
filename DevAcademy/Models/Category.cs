using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevAcademy.Models
{
    public class Category
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Title")]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        //Not used
        public virtual List<Course>? Courses { get; set; }

        [DisplayName("Teacher")]
        public int? TeacherID { get; set; }

        [DisplayName("Admin")]
        public int? AdminID { get; set; }

    }
}
