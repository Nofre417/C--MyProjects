using HRAdministrationAPI;
using static System.Console;
using System.Linq;
using System;


Clear();

decimal totalSalary = 0;
List<IEmployee> employees = new();

SeedData(employees);

WriteLine($"Total salary with bonus: {employees.Sum(e => e.Salary):N2}");
WriteLine($"Total max salary: {employees.Max(e => e.Salary):N2}");


ReadLine();


static void SeedData(List<IEmployee> employees)
{

    IEmployee teacher1 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 1, "Bob", "Mario", 40000);
    employees.Add(teacher1);

    IEmployee teacher2 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 2, "Jack", "Lorion", 42000);
    employees.Add(teacher2);

    IEmployee headOfDepartament = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadOfDepartament, 3, "Lori", "Moris", 65000);
    employees.Add(headOfDepartament);

    IEmployee deputyHeadMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 4, "Jasika", "Carnaval", 69000);
    employees.Add(deputyHeadMaster);

    IEmployee headMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadMaster, 5, "Dima", "Belik", 92000);
    employees.Add(headMaster);

    IEmployee director = EmployeeFactory.GetEmployeeInstance(EmployeeType.Director, 6, "David", "Movsesian", 150000); 
    employees.Add(director);
}
public class Teacher : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.02m); }
}

public class HeadOfDepartment : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.03m); }

}
public class DeputyHeadMaster : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.04m); }

}
public class HeadMaster : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.05m); }

}
public class Director : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.1m); }
}
class EmployeeFactory
{
    public static IEmployee GetEmployeeInstance(EmployeeType employeeType, int id, string firstName, string lastName, decimal salary)
    {
        IEmployee employee = null;

        switch (employeeType)
        {
            case EmployeeType.Teacher:
                employee = FactoryPattern<IEmployee, Teacher>.GetInstance();
                break;
            case EmployeeType.HeadOfDepartament:
                employee = FactoryPattern<IEmployee, HeadOfDepartment>.GetInstance();
                break;
            case EmployeeType.DeputyHeadMaster:
                employee = FactoryPattern<IEmployee, DeputyHeadMaster>.GetInstance();
                break;
            case EmployeeType.HeadMaster:
                employee = FactoryPattern<IEmployee, HeadMaster>.GetInstance();
                break;
            case EmployeeType.Director:
                employee = FactoryPattern<IEmployee, Director>.GetInstance();
                break;
            default:
                break;
        }
        if(employee != null)
        {
            employee.Id = id;
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Salary = salary;
        }
        else
        {
            throw new NullReferenceException();
        }


        return employee;
    }
}
public enum EmployeeType
{
    Teacher,
    HeadOfDepartament,
    DeputyHeadMaster,
    HeadMaster,
    Director
}

