namespace Tugas2BE.DTO
{
    public class StudentWithCourseDTO
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public IEnumerable<CourseDTO>Enrollments { get; set; }
    }
}
