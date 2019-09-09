using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SkillsTest.Models.Entities
{
    public class Student
    {
        public int StudentID { get; set; }

        [Display(Name = "User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }



        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [MaxLength(50)]
        public string FirstMidName { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public Student()
        {
            Enrollments = new List<Enrollment>();
        }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}