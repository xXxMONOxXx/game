using System;
using System.Security.Cryptography;
using System.Text;

namespace game
{
	class HMACGenerator
	{

		private static string HEX_NUMBERS = "1234567890ABCDEF";

		private static int HEX_SIZE = 16;

		private static int KEY_LENGTH = 64;
		public string HMACValue { get; set; }

		public string HMACKey { get; set; }

		public int Move { get; set; }

		private static int GetRandomNumber()
		{
			var random = RandomNumberGenerator.Create();
			var bytes = new byte[sizeof(ulong)];
			random.GetNonZeroBytes(bytes);
			return Math.Abs(BitConverter.ToInt32(bytes));
		}

		private static string ByteToString(byte[] buff)
		{
			string sbinary = "";
			for (int i = 0; i < buff.Length; i++)
			{
				sbinary += buff[i].ToString("X2");
			}
			return (sbinary);
		}
		private void CreateKey()
		{
			HMACKey = "";
			for (int i = 0; i < KEY_LENGTH; i++)
			{
				HMACKey += HEX_NUMBERS[GetRandomNumber() % HEX_SIZE];
			}
		}

		public HMACGenerator(int numberOfArgs)
		{
			CreateKey();
			Move = GetRandomNumber() % numberOfArgs;
			ASCIIEncoding encoding = new ASCIIEncoding();
			HMACSHA256 hmacsha256 = new HMACSHA256(encoding.GetBytes(HMACKey));
			HMACValue = ByteToString(hmacsha256.ComputeHash(encoding.GetBytes(Move.ToString())));
		}
	}
}
