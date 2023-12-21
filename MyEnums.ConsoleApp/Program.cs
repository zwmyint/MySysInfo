// See https://aka.ms/new-console-template for more information
using static MyEnums.ConsoleApp.Helper;

Console.WriteLine("Hello, World!");


FileAccessEnum _FileAccessEnum = FileAccessEnum.Read | FileAccessEnum.Write;
Console.WriteLine(_FileAccessEnum);

var _StatusDes = GetDescriptions<Status>();
foreach (var item in _StatusDes)
{
    Console.WriteLine(item);
}

foreach (var item in Enum.GetValues(typeof(WeekDays)))
{
    Console.WriteLine($"{item} == {(int)item}");
}

var orderStatus = Status.Pending;
Console.WriteLine(orderStatus.IsInProcess());

Console.WriteLine(LogLevel.Error + ": An error occurred");

// Nullable enum
TrafficLight? currentLight = null;
Console.WriteLine(currentLight != null ? "NullTrue": "NullFalse");

