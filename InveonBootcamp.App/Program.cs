using InveonBootcamp.App.SectionOne.DIP;
using InveonBootcamp.App.SectionOne.ISP;
using InveonBootcamp.App.SectionOne.LSP;
using InveonBootcamp.App.SectionOne.OCP;
using InveonBootcamp.App.SectionOne.SRP;
using InveonBootcamp.App.SectionTwo;

// 1. SOLID Principles 

// 1.1 Single Responsibility Practices
// 1.1.1 SRP Bad Usage: 

Student student = new Student { Id = 1, Name = "Beyza Cabuk" };
student.AddStudentToDB();
student.PrintStudentTranscript();

// 1.1.2. SRP Best Practice: 

StudentX studentX = new StudentX { Id = 1, Name = "Beyza Cabuk" };

StudentService studentService = new StudentService();
StudentReportService studentReportService = new StudentReportService();

studentService.AddStudentToDB(studentX);
studentReportService.PrintTranscript(studentX);

// 1.2. Open/Closed Practices
// 1.2.1. OCP Bad Usage: 

MessageSender messageSender = new MessageSender();
messageSender.SendMessage("Hi with SMS", "SMS");
messageSender.SendMessage("Hi with Email", "Email");
messageSender.SendMessage("Hi with MMS", "MMS");

// 1.2.2. OCP Best Practise: 

IMessageSender smsSender = new SMSSender();
IMessageSender mmsSender = new MMSSender();
IMessageSender emailSender = new EmailSender();

smsSender.SendMessage("Hi with SMS");
mmsSender.SendMessage("Hi with MMS");
emailSender.SendMessage("Hi with Email");

// 1.3. Liskov Substitution Principle
// 1.3.1. LSP Bad Usage: 

Employee fullTimeEmployee = new InveonBootcamp.App.SectionOne.LSP.FullTimeEmployee {Id = 1, FirstName = "Beyza", LastName = "Cabuk", Salary = 60000};
Console.WriteLine($"Dividend for {fullTimeEmployee.FirstName} {fullTimeEmployee.LastName}: {fullTimeEmployee.CalculateDividendPayment(fullTimeEmployee.Salary)}");

Employee intern = new Intern { Id = 2, FirstName = "Lale", LastName = "Muldur", Salary = 6000 };
// Console.WriteLine($"Dividend for {intern.FirstName} {intern.LastName}: {intern.CalculateDividendPayment(intern.Salary)}"); // Throws exception 

// 1.3.2. LSP Best Practice:

InveonBootcamp.App.SectionOne.LSP.FullTimeEmployeeX fullTimeEmployeeX = new InveonBootcamp.App.SectionOne.LSP.FullTimeEmployeeX { Id = 1, FirstName = "Beyza", LastName = "Cabuk", Salary = 60000 };
Console.WriteLine($"Dividend for {fullTimeEmployeeX.FirstName} {fullTimeEmployeeX.LastName}: {fullTimeEmployeeX.CalculateDividendPayment(fullTimeEmployeeX.Salary)}");

InternX internX = new InternX { Id = 2, FirstName = "Lale", LastName = "Muldur", Salary = 6000 };
Console.WriteLine($"{internX.FirstName} {internX.LastName} does not receive dividend payments.");


// 1.4. Interface Segregation Principle
// 1.4.1. ISP Bad Usage: 

ILeaveManager fullTime= new InveonBootcamp.App.SectionOne.ISP.FullTimeEmployee();
fullTime.ApplyAnnualLeave(10);
fullTime.ApplyHourlyLeave(5);
fullTime.ApplyFlexibleLeave("Disease");

ILeaveManager partTimeEmployee = new PartTimeEmployee();
// partTimeEmployee.ApplyAnnualLeave(10); // Throws exception 
// partTimeEmployee.ApplyHourlyLeave(5); // Throws exception 
partTimeEmployee.ApplyHourlyLeave(3);

// 1.4.2 ISP Best Practise

IAnnualLeave fullTimeAnnualLeave = new InveonBootcamp.App.SectionOne.ISP.FullTimeEmployeeX();
fullTimeAnnualLeave.ApplyAnnualLeave(10);

IHourlyLeave fullTimeHourlyLeave = new InveonBootcamp.App.SectionOne.ISP.FullTimeEmployeeX();
fullTimeHourlyLeave.ApplyHourlyLeave(5);

IFlexibleLeave fullTimeFlexibleLeave = new InveonBootcamp.App.SectionOne.ISP.FullTimeEmployeeX();
fullTimeFlexibleLeave.ApplyFlexibleLeave("Disease");

IHourlyLeave partTimeHourlyLeave = new PartTimeEmployeeX();
partTimeHourlyLeave.ApplyHourlyLeave(3);

// 1.5. Dependency Inversion Principle
// 1.5.1. DIP Bad Usage: 

DataManager dataManager = new DataManager();
dataManager.Save("Data to save to SQL");

// 1.5.2. DIP Best Practise:

IDatabase sqlDatabase = new SqlDatabaseX();
DataManagerX sqlDataManager = new DataManagerX(sqlDatabase);
sqlDataManager.Save("Data to save to SQL");

IDatabase mongoDatabase = new MongoDBDatabase();
DataManagerX mongoDataManager = new DataManagerX(mongoDatabase);
mongoDataManager.Save("Data to save to MongoDB");



// 2. Asynchronous Programming 
// 2. 1. Asynchronous Programming Tasks

Console.WriteLine("Synchronous Task: ");
AsyncProgrammingTasks.ImplementLongTaskSync();

Console.WriteLine("\nAsynchronous Task: ");
await AsyncProgrammingTasks.ImplementLongTaskAsync();

Console.WriteLine("\nTask.WhenAll: ");
await AsyncProgrammingTasks.ImplementTaskAllMethod();

Console.WriteLine("\nTask with Error: ");
await AsyncProgrammingTasks.PerformTaskWithErrorHandling();

Console.WriteLine("\nTask.WhenAny: ");
await AsyncProgrammingTasks.DemonstrateTaskWhenAny();

Console.WriteLine("\nTask Cancellation: ");
using (var cancellationTokenSource = new CancellationTokenSource())
{
    var cancellationTask = Task.Run(async () =>
    {
        await Task.Delay(2000);
        cancellationTokenSource.Cancel();
        Console.WriteLine("Cancellation requested.");
    });

    await AsyncProgrammingTasks.DemonstrateTaskCancellation(cancellationTokenSource.Token);
}

Console.WriteLine("\nHTTP Request: ");
await AsyncProgrammingTasks.PerformHttpRequestAsync();

Console.WriteLine("\nComplex Calculation: ");
int result = await AsyncProgrammingTasks.PerformComplexCalculationAsync();
Console.WriteLine($"Result of complex calculation: {result}");

Console.WriteLine("\nMultistep Async Operation: ");
await AsyncProgrammingTasks.PerformMultiStepAsyncOperationAsync();

Console.WriteLine("\nParallel Collection Processing: ");
List<string> items = new List<string> { "Item1", "Item2", "Item3", "Item4" };
await AsyncProgrammingTasks.ProcessCollectionInParallelAsync(items);

