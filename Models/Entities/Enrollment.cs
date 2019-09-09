using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkillsTest.Models.Entities
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public decimal? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}