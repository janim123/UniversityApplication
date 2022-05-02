using UniversityApplication.Models;

namespace UniversityApplication.ViewModels
{
    public class FilterStudents
    {
        public IList<Student> students { get; set; }

        public string fullName { get; set; }

        public string studentId { get; set; }
    }
}
