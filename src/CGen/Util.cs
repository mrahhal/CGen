using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CGen
{
	public static class Util
	{
		private static string[] s_hexMap = Enumerable.Range(0, 256).Select(v => v.ToString("X2")).ToArray();

		public static string ToHex(byte[] array)
		{
			var s = new StringBuilder(array.Length * 2);
			foreach (var v in array)
				s.Append(s_hexMap[v]);
			return s.ToString();
		}

		public static byte[] RandomBytes(int length)
		{
			var crypto = new RNGCryptoServiceProvider();
			var bytes = new byte[length];
			crypto.GetBytes(bytes);
			return bytes;
		}
	}
}