using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SunburstStrings
{
    public class SunburstStrings
    {

		public static void Main(string[] args)
        {
			if (args.Any())
            {
				foreach (var arg in args)
                {
					Console.WriteLine(DecryptShort(arg));
                }
            }
        }

		private static string DecryptShort(string domain)
		{
			//if (domain.All((char c) => OrionImprovementBusinessLayer.ZipHelper.Unzip("MzA0MjYxNTO3sExMSk5JTUvPyMzKzsnNyy8oLCouKS0rr6is0o3XAwA=").Contains(c)))
			if (domain.All((char c) => Unzip("MzA0MjYxNTO3sExMSk5JTUvPyMzKzsnNyy8oLCouKS0rr6is0o3XAwA=").Contains(c)))
			{
				//return OrionImprovementBusinessLayer.CryptoHelper.Base64Decode(domain);
				return Base64Decode(domain);
			}
			//return "00" + OrionImprovementBusinessLayer.CryptoHelper.Base64Encode(Encoding.UTF8.GetBytes(domain), false);
			return "00" + Base64Encode(Encoding.UTF8.GetBytes(domain), false);
		}

		public static string Unzip(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			string result;
			try
			{
				//byte[] bytes = OrionImprovementBusinessLayer.ZipHelper.Decompress(Convert.FromBase64String(input));
				byte[] bytes = Decompress(Convert.FromBase64String(input));
				result = Encoding.UTF8.GetString(bytes);
			}
			catch (Exception)
			{
				result = input;
			}
			return result;
		}

		public static byte[] Decompress(byte[] input)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(input))
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
					{
						deflateStream.CopyTo(memoryStream2);
					}
					result = memoryStream2.ToArray();
				}
			}
			return result;
		}

		private static string Base64Decode(string s)
		{
			//string text = OrionImprovementBusinessLayer.ZipHelper.Unzip("Kyo0Ti9OzCkxKzXMrEyryi8wNTdKMbFMyquwSC7LzU4tz8gCAA==");
			string text = Unzip("Kyo0Ti9OzCkxKzXMrEyryi8wNTdKMbFMyquwSC7LzU4tz8gCAA==");
			//string text2 = OrionImprovementBusinessLayer.ZipHelper.Unzip("M4jX1QMA");
			string text2 = Unzip("M4jX1QMA");
			string text3 = "";
			Random random = new Random();
			foreach (char value in s)
			{
				int num = text2.IndexOf(value);
				text3 = ((num < 0) ? (text3 + text[(text.IndexOf(value) + 4) % text.Length].ToString()) : (text3 + text2[0].ToString() + text[num + random.Next() % (text.Length / text2.Length) * text2.Length].ToString()));
			}
			return text3;
		}

		private static string Base64Encode(byte[] bytes, bool rt)
		{
			//string text = OrionImprovementBusinessLayer.ZipHelper.Unzip("K8gwSs1MyzfOMy0tSTfMskixNCksKkvKzTYoTswxN0sGAA==");
			string text = Unzip("K8gwSs1MyzfOMy0tSTfMskixNCksKkvKzTYoTswxN0sGAA==");
			string text2 = "";
			uint num = 0u;
			int i = 0;
			foreach (byte b in bytes)
			{
				num |= (uint)((uint)b << i);
				for (i += 8; i >= 5; i -= 5)
				{
					text2 += text[(int)(num & 31u)].ToString();
					num >>= 5;
				}
			}
			if (i > 0)
			{
				if (rt)
				{
					num |= (uint)((uint)new Random().Next() << i);
				}
				text2 += text[(int)(num & 31u)].ToString();
			}
			return text2;
		}
	}
}
