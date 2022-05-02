using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityApplication.Models;

namespace UniversityApplication.ViewModels
{
    public class FilterEnrollments
    {
        public IList<Enrollment> Enrollments { get; set; }

        public SelectList yearsList { get; set; }
        public int year { get; set; }
    }
}
