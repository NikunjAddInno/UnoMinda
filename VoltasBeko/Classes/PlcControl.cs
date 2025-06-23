using System;
using System.Linq;
using System.Windows.Forms;
using EasyModbus;
using VoltasBeko;
using VoltasBeko.Classes;

public static class PlcControl
{
    public enum PlcReg
    {
        Xaxis = 700,
        Yaxis = 780,
        Zaxis = 860,
        AllCordLoaded = 260,
        TotalPose = 262,
        Light = 264,
        CameraTrigger = 266,
        PartResult = 268,
        Communication = 270,
        NewCycle = 272,
        CycleComplete = 274,
        CurrentPose = 276,
        SetupMode = 278,
        Move = 280,
        XLoc = 282,
        YLoc = 284,
        ZLoc = 286,
        SoftwareReady = 288,
        ReCheck = 290,
        ByPass = 292
    }

    private static ModbusClient modbusClient = new ModbusClient();
    public static event Action<bool> PlcConnected;
    public static event Action NewCycle;
    public static event Action CycleComplete;
    private static Timer timer = new Timer();

    static PlcControl()
    {
        timer.Interval = 100;
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private static void Timer_Tick(object sender, EventArgs e)
    {
        ReadNewCycle();
        Connect();
    }

    public static void Connect(string ipAddress = "192.168.1.5", int port = 502)
    {
        try
        {
            if (!modbusClient.Connected)
            {
                modbusClient.IPAddress = ipAddress;
                modbusClient.Port = port;
                modbusClient.Connect();
                PlcConnected?.Invoke(true);
            }
        }
        catch (Exception ex)
        {
            PlcConnected?.Invoke(false);
            ConsoleExtension.WriteError(ex);
        }
    }

    public static void Disconnect()
    {
        if (modbusClient.Connected)
        {
            modbusClient.Disconnect();
            PlcConnected?.Invoke(false);
        }
    }

    public static void WriteValue(PlcReg reg, int value)
    {
        try
        {
            if (!modbusClient.Connected) Connect();
            modbusClient.WriteSingleRegister((int)reg, value);
        }
        catch (Exception)
        {
            ReconnectAndWrite((int)reg, value);
        }
    }

    public static int ReadValue(PlcReg reg)
    {
        return ReadValue((int)reg);
    }

    public static int ReadValue(int reg)
    {
        try
        {
            if (!modbusClient.Connected) Connect();
            return modbusClient.ReadHoldingRegisters(reg, 1)[0];
        }
        catch (Exception)
        {
            return ReconnectAndRead(reg);
        }
    }

    public static PointT ReadLocation()
    {
        return new PointT
        {
            X = ReadValue((int)PlcReg.XLoc) / 100,
            Y = ReadValue((int)PlcReg.YLoc) / 100,
            Z = ReadValue((int)PlcReg.ZLoc) / 100
        };
    }

    public static void MoveLocation(PointT point)
    {
        WriteValue(PlcReg.Xaxis, point.X * 100);
        WriteValue(PlcReg.Yaxis, point.Y * 100);
        WriteValue(PlcReg.Zaxis, point.Z * 100);
        WriteValue(PlcReg.Move, 1);
    }

    public static void WriteBlock(int startAddress, int[] values)
    {
        try
        {
            if (!modbusClient.Connected) Connect();
            modbusClient.WriteMultipleRegisters(startAddress, values);
        }
        catch (Exception)
        {
            ReconnectAndWriteBlock(startAddress, values);
        }
    }

    public static int[] ReadBlock(int startAddress, int count)
    {
        try
        {
            if (!modbusClient.Connected) Connect();
            return modbusClient.ReadHoldingRegisters(startAddress, count);
        }
        catch (Exception)
        {
            return ReconnectAndReadBlock(startAddress, count);
        }
    }

    private static int _newCycle = 0;
    public static int ReadNewCycle()
    {
        int value = ReadValue(PlcReg.NewCycle);
        if (value == 1 && _newCycle != value)
        {
            ConsoleExtension.WriteWithColor($"New cycle bit {value}", ConsoleColor.Magenta);
            NewCycle?.Invoke();
            _newCycle = value;
        }
        else if (value == 0)
        {
            _newCycle = value;

        }

        return value;
    }

    public static int ReadCycleComplete()
    {
        int value = ReadValue(PlcReg.CycleComplete);
        if (value == 1)
        {
            CycleComplete?.Invoke();
        }
        return value;
    }

    private static void ReconnectAndWrite(int address, int value)
    {
        Disconnect();
        Connect();
        modbusClient.WriteSingleRegister(address, value);
    }

    private static int ReconnectAndRead(int address)
    {
        Disconnect();
        Connect();
        return modbusClient.ReadHoldingRegisters(address, 1)[0];
    }

    private static void ReconnectAndWriteBlock(int startAddress, int[] values)
    {
        Disconnect();
        Connect();
        modbusClient.WriteMultipleRegisters(startAddress, values);
    }

    private static int[] ReconnectAndReadBlock(int startAddress, int count)
    {
        Disconnect();
        Connect();
        return modbusClient.ReadHoldingRegisters(startAddress, count);
    }
}
