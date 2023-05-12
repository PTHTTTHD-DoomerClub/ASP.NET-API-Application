using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Newtonsoft;
namespace Backend_APIs.Models
{
    public class CourseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate {  get; set; }
        public DateTime EndDate {  get; set; }
        public string Description { get; set; }
        public int TheoryHours { get; set; }
        public int PracticeHours { get; set; }

        [ForeignKey("PrerequisiteID")]
        public virtual ICollection<PrerequisiteCourseReference>? PrerequisiteCourses { get; set; }
        //public virtual ICollection<SkillModel>? TrainingSkills { get; set; }
    }

    public class PrerequisiteCourseReference
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int CourseID { get; set; }
        public CourseModel Course { get; set; }
        public int PrerequisiteID { get; set; }
        public virtual CourseModel Prerequisite { get; set; }
    }
}
