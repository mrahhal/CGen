using System;
using Konsola.Parser;

namespace CGen.CommandLine
{
	[Command("guid", Description = "generate a new guid")]
	public class GuidGeneratorCommand : CommandBase<Context>
	{
		public override void ExecuteCommand()
		{
			var guid = Guid.NewGuid().ToString();
			Console.WriteLine(guid);
		}
	}
}