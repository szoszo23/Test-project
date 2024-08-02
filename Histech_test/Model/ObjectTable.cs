namespace Histech_test.Model;

public class ObjectTable
{

    public readonly Table Table = new Table(60, 60);
    public Engine EngineX { get; } = new(400);
    public Engine EngineY { get; } = new(400);
    public Engine EngineZ { get; } = new(200);

    public void MoveTo(double x, double y, double z)
    {
        EngineX.MoveTo(x);
        EngineY.MoveTo(y);
        EngineZ.MoveTo(z);
    }
    
    public (double X, double Y, double Z) GetCurrentPosition()
    {
        return (EngineX.Position, EngineY.Position, EngineZ.Position);
    }
    
    public (double X, double Y, double Z) GetCurrentPixelPosition(double scaleX, double scaleY, double scaleZ)
    {
        var x = EngineX.Position * scaleX;
        var y = EngineY.Position * scaleY;
        var z = EngineZ.Position * scaleZ;
        return (x, y, z);
    }
}