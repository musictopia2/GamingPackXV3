namespace PaydayCP.ViewModels;
public class BuyDealViewModel : BasicSubmitViewModel
{
    private readonly PaydayVMData _model;
    private readonly IBuyProcesses _processes;
    public BuyDealViewModel(CommandContainer commandContainer, PaydayVMData model, IBuyProcesses processes, IEventAggregator aggregator) : base(commandContainer, aggregator)
    {
        _model = model;
        _processes = processes;
    }
    public override bool CanSubmit => _model.CurrentDealList.ObjectSelected() > 0;
    public override Task SubmitAsync()
    {
        return _processes.BuyerSelectedAsync(_model.CurrentDealList.ObjectSelected());
    }
}