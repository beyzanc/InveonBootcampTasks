namespace InveonBootcamp.App.SectionOne.SRP
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void AddStudentToDB()
        {
            Console.WriteLine($"Student {Name} is recorded.");
        }

        public void PrintStudentTranscript()
        {
            Console.WriteLine($"Student transcript for {Name}");
        }
    }
}

