namespace A21DiceGameCP.Data;
[SingletonGame]
public class A21DiceGameSaveInfo : BasicSavedDiceClass<SimpleDice, A21DiceGamePlayerItem>, IMappable, ISaveInfo
{
    public bool IsFaceOff { get; set; }
}