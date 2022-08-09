namespace Tugas2BE.DTO
{
    
        public enum Grade
        {
            A, B, C, D, F
        }

        public class EnrollmentCreateDTO
        {
            
            public int CourseID { get; set; }
            public int StudentID { get; set; }
            public Grade? Grade { get; set; }
           
        }
    
}
