namespace XactikaCP.Logic;
public interface IShapeProcesses
{
    Task ShapeChosenAsync(EnumShapes shape);
    Task FirstCallShapeAsync();
}