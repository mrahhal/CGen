using Konsola.Parser;

namespace CGen.CommandLine
{
	[ContextOptions(
		Description = "Hashes, encrypts, decrypts data using various kinds of algorithms.",
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