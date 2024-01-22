using System.ComponentModel;
using static System.Console;


WriteLine("Press any key to run device...");
ReadKey();

IDevice device = new Device();
device.RunDevice();

ReadKey();





public class Device : IDevice
{
    const double Warning_Level = 27;
    const double Emergency_Level = 75;

    public double WarningTemperatureLevel => Warning_Level;

    public double EmergencyTemperatureLevel => Emergency_Level;

    public void HandleEmergency()
    {
        WriteLine($"\nSending out notifications to emergency services personal\n");
        ShutDownDevice();
        WriteLine();
    }

    private void ShutDownDevice()
    {
        WriteLine("Shutting down device...");
    }

    public void RunDevice()
    {
        WriteLine("Device is running...");

        ICoolingMechanism coolingMechanism = new CoolingMechanism();
        IHeatSensor sensor = new HeatSensor(Warning_Level, Emergency_Level);
        IThermostat thermostat = new Thermostat(this, sensor, coolingMechanism);

        thermostat.RunThermostat();
    }
}



public interface IHeatSensor
{
    event EventHandler<TemperatureEventArgs> TemperatureReachesEmergencyLevelEventHandler;
    event EventHandler<TemperatureEventArgs> TemperatureReachesWarningLevelEventHandler;
    event EventHandler<TemperatureEventArgs> TemperatureReachesBelowWarningLevelEventHandler;
    void RunHeatSensor();
}

public interface ICoolingMechanism
{
    void On();
    void Off();
}

public interface IDevice
{
    double WarningTemperatureLevel { get; }
    double EmergencyTemperatureLevel { get; }
    void RunDevice();
    void HandleEmergency(); 
}
public interface IThermostat
{
    void RunThermostat();
}

public class Thermostat : IThermostat
{
    private ICoolingMechanism _coolingMechanism = null;
    private IHeatSensor _heatSensor = null;
    private IDevice _device = null;

    private const double WarningLevel = 27;
    private const double EmergencyLevel = 75;

    public Thermostat(IDevice device, IHeatSensor heatSensor, ICoolingMechanism coolingMechanism)
    {
        _coolingMechanism = coolingMechanism;
        _heatSensor = heatSensor;
        _device = device;
    }

    private void WireUpEventsToEventHandlers()
    {
        _heatSensor.TemperatureReachesWarningLevelEventHandler += HeatSensor_TemperatureReachesWarningLevelEventHandler;
        _heatSensor.TemperatureReachesBelowWarningLevelEventHandler += HeatSensor_TemperatureReachesBelowWarningLevelEventHandler;
        _heatSensor.TemperatureReachesEmergencyLevelEventHandler += HeatSensor_TemperatureReachesEmergencyLevelEventHandler;
    }

    private void HeatSensor_TemperatureReachesEmergencyLevelEventHandler(object? sender, TemperatureEventArgs e)
    {
        ForegroundColor = ConsoleColor.DarkRed;
        WriteLine($"\nEmergency Alert ! (Emergency level is {_device.EmergencyTemperatureLevel} and above)\n");
        _device.HandleEmergency();
        ResetColor();
    }

    private void HeatSensor_TemperatureReachesBelowWarningLevelEventHandler(object? sender, TemperatureEventArgs e)
    {
        ForegroundColor = ConsoleColor.DarkBlue;
        WriteLine($"\nInformation Alert ! Temperature falls below warning level (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})\n");
        _coolingMechanism.Off();
        ResetColor();
    }

    private void HeatSensor_TemperatureReachesWarningLevelEventHandler(object? sender, TemperatureEventArgs e)
    {
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"\nWarning Alert ! (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})\n");
        _coolingMechanism.On();
        ResetColor();
    }

    public void RunThermostat()
    {
        WriteLine("Thermostat is running...");
        WireUpEventsToEventHandlers();
        _heatSensor.RunHeatSensor();
    }
}
public class CoolingMechanism : ICoolingMechanism
{
    public void Off()
    {
        WriteLine("\nSwitching cooling mechanism off...\n");
    }

    public void On()
    {
        WriteLine("\nSwitching cooling mechanism on...\n");
    }
}

public class HeatSensor : IHeatSensor
{
    double _warningLevel = 0;
    double _emergencyLevel = 0;

    bool _hasReachedWarningLevel = false;

    protected EventHandlerList _listEventsDelegates = new EventHandlerList();

    static readonly object _temperatureReachesWarningLevelKey = new object();
    static readonly object _temperatureFallsBelowWarningLevelKey = new object();
    static readonly object _temperatureReacheEmergencyLevelKey = new object();

    private double[] _temperatureData = null;

    public HeatSensor(double warningLevel, double emergencyLevel)
    {
        _warningLevel = warningLevel;
        _emergencyLevel = emergencyLevel;

        SeedDate();
    }

    private void SeedDate()
    {
        _temperatureData = new double[] {16, 17, 16.5, 18, 19, 22, 24, 26.75, 28.7, 27.6, 26, 24, 22, 45, 68, 86.45 };
    }

    private void MonitorTemperature()
    {
        foreach(double temperature in _temperatureData)
        {
            ResetColor();
            WriteLine($"DateTime: {DateTime.Now} Temperature: {temperature}");

            if(temperature >= _emergencyLevel)
            {
                TemperatureEventArgs args = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    CurrentDateTime = DateTime.Now,
                };
                OnTemperatureReachesEmergencyLevel(args);
            }
            else if(temperature >= _warningLevel)
            {
                _hasReachedWarningLevel = true;
                TemperatureEventArgs args = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    CurrentDateTime = DateTime.Now,
                };
                OnTemperatureReachesWarningLevel(args);
            }
            else if (temperature < _warningLevel && _hasReachedWarningLevel)
            {
                _hasReachedWarningLevel = false;

                TemperatureEventArgs args = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    CurrentDateTime = DateTime.Now,
                };
                OnTemperatureFallsBelowWarningLevel(args);
            }
            System.Threading.Thread.Sleep(1000);
        }
    }

    protected void OnTemperatureReachesWarningLevel(TemperatureEventArgs args)
    {
        EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>) _listEventsDelegates[_temperatureReachesWarningLevelKey];

        if(handler != null)
        {
            handler(this, args);
        }
    }
    protected void OnTemperatureFallsBelowWarningLevel(TemperatureEventArgs args)
    {
        EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventsDelegates[_temperatureFallsBelowWarningLevelKey];

        if (handler != null)
        {
            handler(this, args);
        }
    }
    protected void OnTemperatureReachesEmergencyLevel(TemperatureEventArgs args)
    {
        EventHandler<TemperatureEventArgs> handler = (EventHandler<TemperatureEventArgs>)_listEventsDelegates[_temperatureReacheEmergencyLevelKey];

        if (handler != null)
        {
            handler(this, args);
        }
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesEmergencyLevelEventHandler
    {
        add
        {
            _listEventsDelegates.AddHandler(_temperatureReacheEmergencyLevelKey, value);
        }

        remove
        {
            _listEventsDelegates.RemoveHandler(_temperatureReacheEmergencyLevelKey, value);
        }
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesWarningLevelEventHandler
    {
        add
        {
            _listEventsDelegates.AddHandler(_temperatureReachesWarningLevelKey, value);
        }

        remove
        {
            _listEventsDelegates.RemoveHandler(_temperatureReachesWarningLevelKey, value);
        }
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesBelowWarningLevelEventHandler
    {
        add
        {
            _listEventsDelegates.AddHandler(_temperatureFallsBelowWarningLevelKey, value);
        }

        remove
        {
            _listEventsDelegates.RemoveHandler(_temperatureFallsBelowWarningLevelKey, value);
        }
    }

    public void RunHeatSensor()
    {
        WriteLine("Heat sensor is running...");
        MonitorTemperature();
    }
}

public class TemperatureEventArgs : EventArgs
{   
    public double Temperature { get; set; } 

    public DateTime CurrentDateTime { get; set; }
}