using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Newtonsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Backend_APIs.Models
{

    public class CourseModel
    {
        public CourseModel()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate {  get; set; }
        public DateTime EndDate {  get; set; }
        public string Description { get; set; }
        public int TheoryHours { get; set; }
        public int PracticeHours { get; set; }
        [JsonIgnore]
        public virtual ICollection<PrerequisiteCourseReference>? PrerequisiteCourses { get; set; }
        [JsonIgnore]
        public virtual ICollection<SkillTrainedByCourse>? TrainingSkills { get; set; }
    }

    public class PrerequisiteCourseReference
    {
        public PrerequisiteCourseReference()
        {
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int CourseID { get; set; }
        public CourseModel Course { get; set; }
        public int PrerequisiteID { get; set; }
        public virtual CourseModel Prerequisite { get; set; }
    }

    public class SkillTrainedByCourse
    {
        public SkillTrainedByCourse()
        {
        }
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
        public int SkillId { get; set; }
        public SkillModel Skill { get; set; }
        public string? Level { get; set; } 
    }

    public class PossibleCourseInput
    {
        public CourseModel Course { get; set; }
        public string Level { get; set; }
        [JsonIgnore]
        public int[] TrainingSkills { get; set; }
        [JsonIgnore]
        public int[] PrerequisiteCourses { get; set; }
    }
}
