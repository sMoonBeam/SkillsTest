using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkillsTest.Models.Entities
{
    public class Theme
    {
        public int ThemeId { get; set; }

        [Required(ErrorMessage = "Theme name is required.")]
        public string ThemeName { get; set; }
        public ICollection<Course> Courses { get; set; }
        public Theme()
        {
            Courses = new List<Course>();
        }
    }
}