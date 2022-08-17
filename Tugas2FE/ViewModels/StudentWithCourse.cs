namespace Tugas2FE.ViewModels
{
    public class StudentWithCourse
    {
        public int id { get; set; }
        public string lastName { get; set; }
        public string firstMidName { get; set; }
        public List<Course> enrollments { get; set; }
    }
}
