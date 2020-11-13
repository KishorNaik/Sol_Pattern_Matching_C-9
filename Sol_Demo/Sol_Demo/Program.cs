using System;
using System.Threading.Tasks;

namespace Sol_Demo
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Tax taxObj = new Tax();
            int taxRate1 = await taxObj.GetTaxRate_RelationalPattern(product: new()
            {
                ProductName = "Chai",
                Price = 25
            });
            Console.WriteLine(taxRate1);

            int taxRate2 = await taxObj.GetTaxRate_LogicalPattern(product: new()
            {
                ProductName = "Coffee",
                Price = 6
            });
            Console.WriteLine(taxRate2);

            PersonType personType = new();
            PersonType.PersonTypes personTypes = personType.GetPersonType(person: new Employee()
            {
                EmployeeId = 1,
                FirstName = "Kishor",
                LastName = "Naik"
            });
            Console.WriteLine(personTypes);

            var guessWho = personType.GuessType(person: new Student()
            {
                StudentId = 1,
                FirstName = "Kishor",
                LastName = "Naik"
            });
            Console.WriteLine(guessWho);
        }
    }

    public class Tax
    {
        // Relational Pattern
        public Task<int> GetTaxRate_RelationalPattern(Product product)
        {
            return Task.Run(() =>
            {
                return product.Price switch
                {
                    1 => 1,
                    <= 5 => 2,
                    >= 20 => 3,
                    not null => 0,
                    null => 0
                };
            });
        }

        // Logical Pattern
        public Task<int> GetTaxRate_LogicalPattern(Product product)
        {
            return Task.Run(() =>
            {
                return product.Price switch
                {
                    0 or 1 => 0,
                    > 1 and <= 5 => 10,
                    >= 20 => 30,
                    _ => 40
                };
            });
        }
    }

    public class PersonType
    {
        public enum PersonTypes
        {
            Employee = 0,
            Student = 1,
            NotSelected = 2
        };

        // Type Pattern
        public PersonTypes GetPersonType(Person person) => person switch
        {
            Employee => PersonTypes.Employee,
            Student => PersonTypes.Student,
            null => PersonTypes.NotSelected,
            _ => throw new NotImplementedException()
        };

        // Not Pattern
        public String GuessType(Person person) => person is not Employee ? "Yes He/She is Student" : "Not Employee";
    }

    public record Product
    {
        public String ProductName { get; init; }

        public double? Price { get; init; }
    }

    public record Person
    {
        public String FirstName { get; init; }

        public String LastName { get; init; }
    }

    public record Employee : Person
    {
        public int EmployeeId { get; init; }
    }

    public record Student : Person
    {
        public int StudentId { get; init; }
    }
}