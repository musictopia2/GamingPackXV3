namespace BladesOfSteelCP.Logic;
public interface IFaceoffProcesses
{
    Task FaceOffCardAsync(RegularSimpleCard card);
}