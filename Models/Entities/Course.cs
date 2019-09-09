using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SkillsTest.Models.Entities
{

    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Info is required.")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Course theme is required.")]
        public Theme CourseTheme { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }

        public Course()
        {
            Enrollments = new List<Enrollment>();
        }
        public int TotalMonths()
        {
            DateTime dt1 = StartDate.Date, dt2 = EndDate.Date;
            if (dt1 > dt2) throw new ArgumentException("Начальная дата не может быть больше конечной");
            if (dt1 == dt2) return 0;

            var m = ((dt2.Year - dt1.Year) * 12)
                + dt2.Month - dt1.Month
                + (dt2.Day >= dt1.Day - 1 ? 0 : -1)//поправка на числа
                + ((dt1.Day == 1 && DateTime.DaysInMonth(dt2.Year, dt2.Month) == dt2.Day) ? 1 : 0);//если начальная дата - 1-е число меяца, а конечная - последнее число, добавляется 1 месяц
            return m;
        }
    }
}