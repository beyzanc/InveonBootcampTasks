using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionOne.SRP
{
    public class StudentX
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentService
    {
        public void AddStudentToDB(StudentX student)
        {
            Console.WriteLine($"Student {student.Name} is recorded.");
        }
    }

    public class StudentReportService
    {
        public void PrintTranscript(StudentX student)
        {
            Console.WriteLine($"Student transcript for {student.Name}");
        }
    }

}
