namespace InveonBootcamp.App.SectionOne.LSP
{
    public class EmployeeX
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
    }

    public interface IDividendPayable
    {
        decimal CalculateDividendPayment(decimal salary);
    }

    public class FullTimeEmployeeX : EmployeeX, IDividendPayable
    {
        public decimal CalculateDividendPayment(decimal salary)
        {
            return salary * 0.5m;
        }
    }

    public class InternX : Employee
    {
        // There is no method to implement
    }
}
