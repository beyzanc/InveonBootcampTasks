namespace InveonBootcamp.App.SectionOne.LSP
{

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }

        public virtual decimal CalculateDividendPayment(decimal salary)
        {
            return salary * 0.5m;
        }

    }

    public class FullTimeEmployee : Employee
    {
        public override decimal CalculateDividendPayment(decimal salary)
        {
            return salary * 0.5m;
        }
    }

    public class Intern : Employee
    {
        public override decimal CalculateDividendPayment(decimal salary)
        {
            throw new NotSupportedException("Interns do not receive a share of the annual profit.");
        }
    }
}
