namespace Tugas2FE.ViewModels
{
    public class CourseWithStudent
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
