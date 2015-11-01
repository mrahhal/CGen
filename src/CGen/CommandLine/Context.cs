using Konsola.Parser;

namespace CGen.CommandLine
{
	[ContextOptions(
		Description = "Generates different kinds of cryptographic keys.",
		HandleEmptyInvocationAsHelp = true,
		InvokeMethods = true)]
	[IncludeCommands(
		typeof(RandomGeneratorCommand),
		typeof(GuidGeneratorCommand),
		typeof(HashCommand),
		typeof(EncryptionCommand),
		typeof(DecryptionCommand),
		typeof(GenerateCommand))]
	public class Context : ContextBase
	{
	}
}