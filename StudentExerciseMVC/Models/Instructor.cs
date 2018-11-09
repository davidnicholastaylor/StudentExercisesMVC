using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercisesAPI.Data
{
    public class Instructor
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "How to find me on Slack")]
        public string SlackHandle { get; set; }

        [Display(Name = "My specialty")]
        public string Specialty { get; set; }

        public Cohort Cohort { get; set; }
        public int CohortId { get; set; }
    }
}
