using System.IO;
using System.Security.Cryptography;
using Konsola.Parser;

namespace CGen.CommandLine
{
	public abstract class HashAlgorithmCommandBase : Command
	{
		[Parameter("data", Description = "the data to hash", Position = 1)]
		public string Data { get; set; }

		[Parameter("input", Description = "the file's name or path that contains the data to hash")]
		public string InputFileName { get; set; }

		[OnParsed]
		public virtual void OnParsed()
		{
			if (Data == null && InputFileName == null)
			{
				throw new CommandLineException("Either data or a file name should be provided.")
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}

			if (InputFileName != null && !File.Exists(InputFileName))
			{
				throw new CommandLineException(
					string.Format("The file at \"{0}\" does not exist.", InputFileName))
				{
					Kind = CommandLineExceptionKind.Message,
				};
			}
		}

		public override string ExecuteCore()
		{
			var data =
				InputFileName != null ?
				File.ReadAllText(InputFileName) :
				Data;
			return ComputeHash(data);
		}

		private string ComputeHash(string data)
		{
			using (var algorithm = CreateHashAlgorithm())
			{
				var bytes = Encoding.GetBytes(data);
				return Util.ToHex(algorithm.ComputeHash(bytes));
			}
		}

		public abstract HashAlgorithm CreateHashAlgorithm();
	}
}