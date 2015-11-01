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
		private Encoding _encoding;

		[Parameter("output,o", Description = "the path to output the result to")]
		public string PathName { get; set; }

		[Parameter("encoding,e", Description = "the encoding to work with", Default = EncodingKind.Unicode)]
		public EncodingKind EncodingKind { get; set; }

		protected Encoding Encoding
		{
			get
			{
				if (_encoding != null)
				{
					return _encoding;
				}
				switch (EncodingKind)
				{
					case EncodingKind.ASCII:
						return Encoding.ASCII;

					case EncodingKind.Unicode:
					default:
						return Encoding.Unicode;
				}
			}
			set
			{
				_encoding = value;
			}
		}

		public override void ExecuteCommand()
		{
			var result = ExecuteCore();
			if (string.IsNullOrWhiteSpace(PathName))
			{
				Console.WriteLine(Encoding.GetString(result));
			}
			else
			{
				File.WriteAllBytes(PathName, result);
			}
		}

		protected abstract byte[] ExecuteCore();
	}
}