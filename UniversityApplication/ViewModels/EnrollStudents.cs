
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityApplication.Models;

namespace UniversityApplication.ViewModels
{
    public class EnrollStudents
    {
        public Course course { get; set; }

        public IEnumerable<long>? selectedStudents { get; set; }

        public IEnumerable<SelectListItem>? studentsEnrolledList { get; set; }

        public int? year { get; set; }
    }
}