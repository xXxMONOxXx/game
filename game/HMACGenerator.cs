using System.Security.Cryptography;
using System.Text;

namespace game
{
	class HMACGenerator
	{
		public string HMACValue { get; set; }

		public string HMACKey { get; set; }

		public int Move { get; set; }

		private static int GetRandomNumber(int max)
		{
			var number = RandomNumberGenerator.GetInt32(max);
			return number;
		}

		private static int GetRandomNumber()
		{
			var number = RandomNumberGenerator.GetInt32(System.Int32.MaxValue);
			return number;
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
		private void CreateKey(int numberOfArgs)
		{
			HMACKey = "";
			for (int i = 0; i < 4; i++)
			{
				HMACKey += GetRandomNumber().ToString("X");
			}
		}

		public HMACGenerator(int numberOfArgs)
		{
			CreateKey(numberOfArgs);
			Move = GetRandomNumber(numberOfArgs);
			ASCIIEncoding encoding = new ASCIIEncoding();
			HMACSHA256 hmacsha256 = new HMACSHA256(encoding.GetBytes(HMACKey));
			HMACValue = ByteToString(hmacsha256.ComputeHash(encoding.GetBytes((Move + 1).ToString())));
		}
	}
}
