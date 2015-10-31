using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CGen
{
	public static class Util
	{
		private static string[] _hexMap =
			Enumerable.Range(0, 256)
			.Select(v => v.ToString("x2"))
			.ToArray();

		public static string ToHex(byte[] array)
		{
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));

			var s = new StringBuilder(bytes.Length * 2);
			foreach (var v in bytes)
				s.Append(_hexMap[v]);
			return s.ToString();
		}

		public static byte[] RandomBytes(int length)
		{
			if (length < 2)
				throw new ArgumentOutOfRangeException(nameof(length));

			var crypto = new RNGCryptoServiceProvider();
			var bytes = new byte[length];
			crypto.GetBytes(bytes);
			return bytes;
		}
	}
}