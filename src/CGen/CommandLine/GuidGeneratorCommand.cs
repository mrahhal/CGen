using System;
using Konsola;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("guid", Description = "generate a new guid")]
	public class GuidGeneratorCommand : Command
	{
		public override string ExecuteCore()
		{
			return Guid.NewGuid().ToString();
		}
	}
}