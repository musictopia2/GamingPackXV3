global using CommandsGenerator;
namespace CommandsGenerator;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
internal class CommandAttribute : Attribute
{
    [Required]
    public EnumCommandCategory Category { get; set; }
}