using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Konsola;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class Command : CommandBase<Context>
	{
		[Parameter("name", Description = "the file name to output the result to")]
		public string FileName { get; set; }

		[Parameter("encoding,e", Description = "the encoding to work with", Default = EncodingKind.Unicode)]
		public EncodingKind EncodingKind { get; set; }

		protected Encoding Encoding
		{
			get
			{
				switch (EncodingKind)
				{
					case EncodingKind.UTF8:
						return Encoding.UTF8;

					case EncodingKind.Unicode:
					default:
						return Encoding.Unicode;
				}
			}
		}

		public override void ExecuteCommand()
		{
			var result = ExecuteCore();
			if (string.IsNullOrWhiteSpace(FileName))
			{
				Console.WriteLine(result);
			}
			else
			{
				File.WriteAllText(FileName, result);
			}
		}

		public abstract string ExecuteCore();
	}
}