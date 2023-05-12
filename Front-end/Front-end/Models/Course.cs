namespace Front_end.Models
{
    public class Course
    {
        public int CourseID;
        public string Title { get; set; }
        //public int Prerequisite { get; set; }
        //public string CourseTarget {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
