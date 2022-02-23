namespace RiskCP.Data;
public record struct SendAttackResult(BasicList<BasicList<AttackDice>> AttackList, BasicList<BasicList<SimpleDice>> DefenseList);