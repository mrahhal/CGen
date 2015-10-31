using Konsola;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[ContextOptions(
		Description = "Generates different kinds of cryptographic keys",
		HandleEmptyInvocationAsHelp = true,
		InvokeMethods = true)]
	[IncludeCommands(
		typeof(RandomGeneratorCommand)
		)]
	public class Context : ContextBase
	{
	}
}