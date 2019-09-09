using SkillsTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkillsTest.Models.ViewModels
{
    public class CourseViewModel
    {
        public string Order { get; set; }
        public string Theme { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}