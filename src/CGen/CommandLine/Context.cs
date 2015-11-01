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
		typeof(SHA1Command),
		typeof(SHA256Command),
		typeof(SHA512Command),
		typeof(MD5Command),
		typeof(AesCommand),
		typeof(RijndaelCommand),
		typeof(RSACommand))]
	public class Context : ContextBase
	{
	}
}