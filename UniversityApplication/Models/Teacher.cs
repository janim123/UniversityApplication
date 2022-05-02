using System.ComponentModel.DataAnnotations;
using UniversityApplication.Data;
namespace UniversityApplication.Models
{
    public class Teacher
    {
        public int teacherId { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Degree")]
        public string? degree { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Display(Name = "Academic Rank")]
        public string? academicRank { get; set; }

        [StringLength(10, MinimumLength = 2)]
        [Display(Name = "Office Number")]
        public string? officeNumber { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime? hireDate { get; set; }

        public string fullName
        {
            get
            {
                return string.Format("{0} {1}", firstName, lastName);
            }
        }

        public ICollection<Course>? Courses1 { get; set; }
        public ICollection<Course>? Courses2 { get; set; }
    }
}