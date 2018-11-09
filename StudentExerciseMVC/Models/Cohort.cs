using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercisesAPI.Data
{
    public class Cohort
    {
        public int Id { get; set; }

        [Display(Name="Cohort Name")]
        public string Name { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
    }

}