global using CommandsGenerator;
namespace CommandsGenerator;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
internal class CommandAttribute : Attribute
{
    [Required]
    public EnumCommandCategory Category { get; set; }
    public string Name { get; set; } = ""; //this means that you are able to attach a command name so it can link up properly.  only needed if the category is control.
    //if you attach name and its not control, will raise exception at compile time.
}