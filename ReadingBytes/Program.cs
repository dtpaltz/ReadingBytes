using System;
using System.Linq;
using System.Text;

namespace ReadingBytes
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] arr = new byte[] { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };

			//DecodeBytes2(arr);

			EncodeLabel();
		}


		public static string Label = "270";

		public static void EncodeLabel()
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in Label)
			{
				int n = byte.Parse(c.ToString());
				sb.Append(Convert.ToString(n, 2).PadLeft(3, '0'));
			}

			byte[] bytes = BinaryStringToBytes(sb.ToString().Substring(1));

			byte encodedLabel = ReverseByteBits(bytes[0]);

			Console.ReadLine();
		}




		public static byte[] BinaryStringToBytes(string binaryStr)
		{
			int numOfBytes = binaryStr.Length / 8;
			byte[] bytes = new byte[numOfBytes];
			for (int i = 0; i < numOfBytes; ++i)
			{
				bytes[i] = Convert.ToByte(binaryStr.Substring(8 * i, 8), 2);
			}

			return bytes;
		}

		/// <summary>
		/// Not tested
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string ReverseString(string s)
		{
			char[] arr = s.ToCharArray();
			Array.Reverse(arr);
			return new string(arr);
		}


		public static void DecodeBytes1(byte[] array)
		{
			for (int i = 0; i < 192; i += 3)
			{
				byte[] subset = (from b in (array.Skip(i).Take(3)) select ReverseByteBits(b)).ToArray();


				int r = subset[0] << 16 | subset[1] << 8 | subset[2];
				string binStr = Convert.ToString(r, 2).PadLeft(24, '0');

				int chunkSize = 3;
				var arr2 = Enumerable.Range(0, binStr.Length / chunkSize).Select(j => binStr.Substring(j * chunkSize, chunkSize).PadLeft(8, '0'));

				byte[] binsI = (from s in arr2 select Convert.ToByte(s, 2)).ToArray();

				Console.WriteLine(r + "  ==  " + binStr);
			}

			Console.ReadLine();
		}


		public static void DecodeBytes2(byte[] array)
		{
			Console.WriteLine("Starting bytes: " + array.Length);
			if (array.Length * 8 % 3 == 0)
			{
				byte[] reversedBytes = (from b in array select ReverseByteBits(b)).ToArray();
				string binaryStr = string.Join("", (from b in reversedBytes select Convert.ToString(b, 2).PadLeft(8, '0')).ToArray());

				Console.WriteLine(binaryStr);

				int chunkSize = 3;
				string[] arr2 = (Enumerable.Range(0, binaryStr.Length / chunkSize).Select(j => binaryStr.Substring(j * chunkSize, chunkSize).PadLeft(8, '0'))).ToArray();

				byte[] bins = (from s in arr2 select Convert.ToByte(s, 2)).ToArray();

				Console.WriteLine("Bins: " + bins.Length);
			}
			else
			{
				Console.WriteLine("array length invalid");
			}

			Console.ReadLine();
		}


		public static byte ReverseByteBits(byte b)
		{
			int a = 0;
			for (int i = 0; i < 8; i++)
				if ((b & (1 << i)) != 0)
					a |= 1 << (7 - i);
			return (byte)a;
		}

	}
}
