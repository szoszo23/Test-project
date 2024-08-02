using System;

namespace Histech_test.Service;

public static  class Movement
{
    public static double MoveEngineTowards(double current, double target, double speed, double tableSize, double max, double scale)
    {
        double newValue;
        if (current < target)
        {
            
            newValue = Math.Min(current + speed, target);
        }
        else if (current > target)
        {
            newValue = Math.Max(current - speed, target);
        }
        else
        {
            newValue = current;
        }

        return SetBounds(newValue, tableSize, max,  scale);
    }
    
    public static double SetBounds(double value, double elementSize, double maxSize, double scale)
    {
        if (value < 0)
        {
            return 0;
        }
    
        if (value > maxSize - elementSize / scale)
        {
            return maxSize - elementSize / scale ;
        }
    
        return value;
    }
}