// See https://aka.ms/new-console-template for more information
using MyDateOnlyAndTimeOnly.ConsoleApp;

Console.WriteLine("DateOnly Examples!");

MyDateOnly.DateOnlyExample();

MyDateOnly.DateOnlyFromDateTimeExample();

Console.WriteLine("TimeOnly Examples!");

MyTimeOnly.TimeOnlyExample();

MyTimeOnly.TimeOnlyFromDateTimeExample();

Console.WriteLine("Combine DateOnly and TimeOnly Examples!");

DateOnly dateOnly = new DateOnly(2023, 10, 07);
TimeOnly timeOnly = new TimeOnly(12, 30, 45);
DateTime combinedDateTime = dateOnly.ToDateTime(timeOnly);

Console.WriteLine($"Combined DateTime :  {combinedDateTime}");

Console.Read();