namespace Histech_test.Model;

public class Engine
{
    public double Position { get; private set; }
    public double Speed { get; private set; }
    public double MaxStep { get; }

public Engine(double maxStep)
    {
        Position = 0;
        Speed = 2.0;
        MaxStep = maxStep;
    }

    public void MoveTo(double newPosition)
    {
        Position = newPosition;
    }

    public void IncreaseSpeed()
    {
        Speed += 1.0;
    }

    public void DecreaseSpeed()
    {
        if (Speed > 1)
        {
            Speed -= 1.0;
        }
    }
}