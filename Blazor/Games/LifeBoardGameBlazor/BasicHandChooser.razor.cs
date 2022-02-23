namespace LifeBoardGameBlazor;
public partial class BasicHandChooser<B>
    where B : BasicSubmitViewModel
{
    [CascadingParameter]
    public B? DataContext { get; set; }
    [CascadingParameter]
    public LifeBoardGameVMData? GameContainer { get; set; }
}