using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercisesAPI.Data
{
    public class Exercise
    {
        public int Id { get; set; }

        [Display(Name="Exercise Name")]
        public string Name { get; set; }

        [Display(Name="Language")]
        public string Language { get; set; }

        public List<Student> AssignedStudents { get; set; } = new List<Student>();
    }
}