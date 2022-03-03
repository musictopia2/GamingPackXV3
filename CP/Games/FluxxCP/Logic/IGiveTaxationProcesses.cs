namespace FluxxCP.Logic;
public interface IGiveTaxationProcesses
{
    Task GiveCardsForTaxationAsync(IDeckDict<FluxxCardInformation> list);
}