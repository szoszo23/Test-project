using System;
using System.ComponentModel;
using System.Windows.Threading;
using Histech_test.Service;

namespace Histech_test.Model;

public class ViewModel : INotifyPropertyChanged
{
    public ObjectTable ObjectTable { get; set; }
    public double MoveAreaWidth;
    public double MoveAreaHeight;
    public double MoveAreaZHeight;
    private double _targetX;
    private double _targetY;
    private double _targetZ;
    private double _speedX;
    private double _speedY;
    private double _speedZ;
    private readonly DispatcherTimer _timer;
    public double ScaleX => MoveAreaWidth / ObjectTable.EngineX.MaxStep;
    public double ScaleY => MoveAreaHeight / ObjectTable.EngineY.MaxStep;
    private double ScaleZ => MoveAreaZHeight / ObjectTable.EngineZ.MaxStep;
    public double TableXPosition => ObjectTable.GetCurrentPosition().X;
    public double TableYPosition => ObjectTable.GetCurrentPosition().Y;
    public double TableZPosition => ObjectTable.GetCurrentPosition().Z;
    public double XPosition => ObjectTable.GetCurrentPixelPosition(ScaleX, ScaleY, ScaleZ).X;
    public double YPosition => ObjectTable.GetCurrentPixelPosition(ScaleX, ScaleY, ScaleZ).Y;
    public double ZPosition => ObjectTable.GetCurrentPixelPosition(ScaleX, ScaleY, ScaleZ).Z;

    public double XSpeed
    {
        get => ObjectTable.EngineX.Speed;
        set
        {
            _speedX = value;
            OnPropertyChanged(nameof(XSpeed));
        }
    }

    public double YSpeed
    {
        get => ObjectTable.EngineY.Speed;
        set
        {
            _speedY = value;
            OnPropertyChanged(nameof(YSpeed));
        }
    }

    public double ZSpeed
    {
        get => ObjectTable.EngineZ.Speed;
        set
        {
            _speedZ = value;
            OnPropertyChanged(nameof(ZSpeed));
        }
    }


    public double TableWidth => ObjectTable.Table.Width;
    public double TableHeight => ObjectTable.Table.Height;
    public double EngineXPosition { get; set; }
    public double EngineYPosition { get; set; }
    public double EngineZPosition { get; set; }

    public ViewModel()
    {
        ObjectTable = new ObjectTable();
        _speedX = XSpeed;
        _speedY = YSpeed;
        _speedZ = ZSpeed;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(16) // Display animation about on 60 FPS
        };
        _timer.Tick += TimerTick;
        _timer.Start();
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        ObjectTable.MoveTo(
            Movement.MoveEngineTowards(TableXPosition, _targetX, ObjectTable.EngineX.Speed, ObjectTable.Table.Width,
                ObjectTable.EngineX.MaxStep, ScaleX),
            Movement.MoveEngineTowards(TableYPosition, _targetY, ObjectTable.EngineY.Speed, ObjectTable.Table.Height,
                ObjectTable.EngineX.MaxStep, ScaleY),
            Movement.MoveEngineTowards(TableZPosition, _targetZ, ObjectTable.EngineZ.Speed, 0,
                ObjectTable.EngineX.MaxStep, ScaleZ)
        );

        EngineXPosition = XPosition + ObjectTable.Table.Width - ObjectTable.Table.Width / 4;
        EngineYPosition = YPosition + ObjectTable.Table.Height - ObjectTable.Table.Height / 4;
        EngineZPosition = ZPosition;

        XSpeed = ObjectTable.EngineX.Speed;
        YSpeed = ObjectTable.EngineY.Speed;
        ZSpeed = ObjectTable.EngineZ.Speed;

        OnPropertyChanged(nameof(XPosition));
        OnPropertyChanged(nameof(YPosition));
        OnPropertyChanged(nameof(ZPosition));

        OnPropertyChanged(nameof(TableXPosition));
        OnPropertyChanged(nameof(TableYPosition));
        OnPropertyChanged(nameof(TableZPosition));

        OnPropertyChanged(nameof(EngineXPosition));
        OnPropertyChanged(nameof(EngineYPosition));
        OnPropertyChanged(nameof(EngineZPosition));
    }

    public void MoveDownX()
    {
        var newPosition = ObjectTable.GetCurrentPosition().X - ObjectTable.EngineX.Speed;
        SetTargetPosition(newPosition);
    }

    public void MoveUpX()
    {
        var newPosition = ObjectTable.GetCurrentPosition().X + ObjectTable.EngineX.Speed;
        SetTargetPosition(newPosition);
    }

    public void MoveUpY()
    {
        var newPosition = ObjectTable.GetCurrentPosition().Y - ObjectTable.EngineY.Speed;
        SetTargetPositionOnY(newPosition);
    }

    public void MoveDownY()
    {
        var newPosition = ObjectTable.GetCurrentPosition().Y + ObjectTable.EngineY.Speed;
        SetTargetPositionOnY(newPosition);
    }

    public void MoveUpZ()
    {
        var newPosition = ObjectTable.GetCurrentPosition().Z - ObjectTable.EngineZ.Speed;
        SetTargetPositionOnZ(newPosition);
    }

    public void MoveDownZ()
    {
        var newPosition = ObjectTable.GetCurrentPosition().Z + ObjectTable.EngineY.Speed;
        SetTargetPositionOnZ(newPosition);
    }

    public void IncreaseSpeedX() => ObjectTable.EngineX.IncreaseSpeed();
    public void DecreaseSpeedX() => ObjectTable.EngineX.DecreaseSpeed();
    public void IncreaseSpeedY() => ObjectTable.EngineY.IncreaseSpeed();
    public void DecreaseSpeedY() => ObjectTable.EngineY.DecreaseSpeed();
    public void IncreaseSpeedZ() => ObjectTable.EngineZ.IncreaseSpeed();
    public void DecreaseSpeedZ() => ObjectTable.EngineZ.DecreaseSpeed();

    public void SetTargetPosition(double x, double y, double z)
    {
        _targetX = Movement.SetBounds(x, ObjectTable.Table.Width, ObjectTable.EngineX.MaxStep, ScaleX);
        _targetY = Movement.SetBounds(y, ObjectTable.Table.Height, ObjectTable.EngineY.MaxStep, ScaleY);
        _targetZ = Movement.SetBounds(z, 1, ObjectTable.EngineZ.MaxStep, ScaleZ);
    }

    public void SetTargetPosition(double y, double z)
    {
        _targetY = Movement.SetBounds(y, ObjectTable.Table.Height, ObjectTable.EngineY.MaxStep, ScaleY);
        _targetZ = Movement.SetBounds(z, 1, ObjectTable.EngineZ.MaxStep, ScaleZ);
    }

    public void SetTargetPositionZlock(double x, double y)
    {
        _targetX = Movement.SetBounds(x, ObjectTable.Table.Width, ObjectTable.EngineX.MaxStep, ScaleX);
        _targetY = Movement.SetBounds(y, 0, ObjectTable.EngineY.MaxStep, ScaleY);
    }

    public void SetTargetPositionYLock(double x, double z)
    {
        _targetX = Movement.SetBounds(x, ObjectTable.Table.Width, ObjectTable.EngineX.MaxStep, ScaleX);
        _targetZ = z;
    }

    public void SetTargetPosition(double x)
    {
        _targetX = Movement.SetBounds(x, ObjectTable.Table.Width, ObjectTable.EngineX.MaxStep, ScaleX);
    }

    public void SetTargetPositionOnY(double y)
    {
        _targetY = Movement.SetBounds(y, 0, ObjectTable.EngineY.MaxStep, ScaleY);
    }

    public void SetTargetPositionOnZ(double z)
    {
        _targetZ = Movement.SetBounds(z, 0, ObjectTable.EngineZ.MaxStep, ScaleZ);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}