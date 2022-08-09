namespace Tugas2BE.DTO
{
    public class CourseWithStudentDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<StudentDTO> Students { get; set; }
    }
}
