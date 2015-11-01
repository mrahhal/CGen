using System;
using System.Collections.Generic;
using System.IO;
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

		private static IDictionary<char, int> _hexCharMap =
			new Dictionary<char, int>()
			{
				{'0', 0 },
				{'1', 1 },
				{'2', 2 },
				{'3', 3 },
				{'4', 4 },
				{'5', 5 },
				{'6', 6 },
				{'7', 7 },
				{'8', 8 },
				{'9', 9 },
				{'a', 10 },
				{'b', 11 },
				{'c', 12 },
				{'d', 13 },
				{'e', 14 },
				{'f', 15 },
			};

		public static string ToHex(byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));

			var s = new StringBuilder(bytes.Length * 2);
			foreach (var v in bytes)
				s.Append(_hexMap[v]);
			return s.ToString();
		}

		public static byte[] FromHex(string hex)
		{
			if (hex == null)
				throw new ArgumentNullException(nameof(hex));
			if (hex.Length % 2 != 0)
				throw new ArgumentException("Hex length should be a multiplier of 2", nameof(hex));

			var bytes = new byte[hex.Length / 2];
			var j = 0;
			for (int i = 0; i < hex.Length; i += 2)
			{
				var leftmost = _hexCharMap[hex[i]];
				var rightmost = _hexCharMap[hex[i + 1]];

				bytes[j++] = (byte)((leftmost << 4) | rightmost);
			}
			return bytes;
		}

		public static byte[] RandomBytes(int length)
		{
			if (length < 1)
				throw new ArgumentOutOfRangeException(nameof(length));

			var crypto = new RNGCryptoServiceProvider();
			var bytes = new byte[length];
			crypto.GetBytes(bytes);
			return bytes;
		}

		public static byte[] ReadBytesToEnd(Stream stream)
		{
			var buffer = new byte[16];
			var offset = 0;
			while (true)
			{
				var toRead = buffer.Length - offset;
				var read = ReadEx(stream, buffer, offset, toRead);
				offset += read;
				if (read < buffer.Length - offset)
				{
					break;
				}
				var temp = new byte[buffer.Length * 2];
				Buffer.BlockCopy(buffer, 0, temp, 0, buffer.Length);
				buffer = temp;
			}
			var result = new byte[offset];
			Buffer.BlockCopy(buffer, 0, result, 0, result.Length);
			return result;
		}

		public static int ReadEx(Stream stream, byte[] buffer, int offset, int count)
		{
			var totalRead = default(int);
			while (totalRead != count)
			{
				int read = stream.Read(buffer, offset + totalRead, count - totalRead);
				if (read == 0)
					break;
				totalRead += read;
			}
			return totalRead;
		}

		public static bool IsInputPiped()
		{
			try
			{
				var t = Console.KeyAvailable;
				return false;
			}
			catch
			{
				return true;
			}
		}
	}
}