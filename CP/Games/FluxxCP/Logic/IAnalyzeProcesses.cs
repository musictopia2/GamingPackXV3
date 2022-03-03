namespace FluxxCP.Logic;
public interface IAnalyzeProcesses
{
    Task AnalyzeQueAsync();
    void AnalyzeRules();
}