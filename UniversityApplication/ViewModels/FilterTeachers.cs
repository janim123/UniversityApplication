using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityApplication.Models;

namespace UniversityApplication.ViewModels
{
    public class FilterTeachers
    {
        public IList<Teacher> teachers { get; set; }

        public SelectList academicRanks { get; set; }

        public SelectList degrees { get; set; }

        public string fullName { get; set; }

        public string academicRank { get; set; }

        public string degree { get; set; }
    }
}
