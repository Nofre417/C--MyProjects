using static System.Console;
using System.IO;
using System.Linq;
using System;




CarFactoryDel carFactoryDel = CarFactory.ReturnOfficeCar;
Car officeCar = carFactoryDel(1, "DHL 1");

carFactoryDel = CarFactory.ReturnPersonalCar;
Car personalCar = carFactoryDel(2, "BMW M5");

LogOfficeCarDel logOfficeCarDel = LogCarDetails;
LogCarDetails(officeCar as OfficeCar);

LogPersonalCarDel logPersonalCarDel = LogCarDetails;
LogCarDetails(personalCar as PersonalCar);



static void LogCarDetails(Car car)
{
    if(car is OfficeCar)
    {
        using(StreamWriter sw = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "OfficeCars.txt")))
        {
            sw.WriteLine($"ID: {car.Id} - Name: {car.Name}");
            sw.WriteLine($"Car details: {car.GetCarDetails()}");
        }
    }
    else if(car is PersonalCar)
    {
       WriteLine($"ID: {car.Id} - Name: {car.Name}");
       WriteLine($"Car details: {car.GetCarDetails()}");
    }
    else
    {
        throw new ArgumentException();
    }
}



delegate Car CarFactoryDel(int id, string name);
delegate void LogOfficeCarDel(OfficeCar car);
delegate void LogPersonalCarDel(PersonalCar car);

public static class CarFactory
{
    public static OfficeCar ReturnOfficeCar(int id, string name)
    {
        return new OfficeCar { Id = id, Name = name };
    }

    public static PersonalCar ReturnPersonalCar(int id, string name)
    {
        return new PersonalCar { Id = id, Name = name };
    }
}


public abstract class Car
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual string GetCarDetails()
    {
        return $"{Id} - {Name}";
    }
}

public class OfficeCar : Car
{
    public virtual string GetCarDetails()
    {
        return $"{base.GetCarDetails()} - Office car";
    }
}

public class PersonalCar : Car
{
    public virtual string GetCarDetails()
    {
        return $"{base.GetCarDetails()} - Personal car";
    }
}

