﻿using Konsola;

namespace CGen.CommandLine
{
	[ContextOptions(Description = "Generates different kinds of cryptographic keys", ExitOnException = true)]
	[IncludeCommands(
		)]
	public class Context : ContextBase
	{
	}
}