using System;
using System.Collections.Generic;

namespace game
{
	class Program
	{

		public static bool IsValidMove(string move, int numberOfArgs)
		{
			int moveInt;
			if (!int.TryParse(move, out moveInt))
			{
				return move == "?" ? true : false;
			}
			else
			{
				if (moveInt >= 0 && moveInt <= numberOfArgs)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public static string GetValidMove(int numberOfArgs)
		{
			string move = Console.ReadLine();

			while (!IsValidMove(move, numberOfArgs))
			{
				Console.WriteLine("Invalid input, please try again.");
				move = Console.ReadLine();
			}

			return move;
		}

		public static void OutMovesMenu(string[] args)
		{
			Console.WriteLine("Available moves:");
			for (int i = 0; i < args.Length; i++)
			{
				Console.WriteLine($"{i + 1} - {args[i]}");
			}
			Console.WriteLine("0 - exit");
			Console.WriteLine("? - help");
		}

		public static string GetResult(int humanMove, int computerMove, int numberOfArgs)
		{
			if (computerMove == humanMove)
			{
				return "It's a Tie!";
			}
			else
			{
				string row = Table.GetStartString(numberOfArgs);
				while (row[humanMove] != '0')
				{
					row = Table.ShiftString(row);
				}
				if (row[computerMove] == '1')
				{
					return "You Win!";
				}
				else
				{
					return "You Lose!";
				}
			}
		}
		public static void Menu(string[] args)
		{
			HMACGenerator computer = new HMACGenerator(args.Length);
			Console.WriteLine($"HMAC: {computer.HMACValue}");
			bool flag = true;
			while (flag)
			{
				OutMovesMenu(args);
				string humanMove = GetValidMove(args.Length);
				switch (humanMove)
				{
					case ("0"):
						{
							flag = false;
							break;
						}
					case ("?"):
						{
							Table table = new Table(args);
							Console.WriteLine(table.ToString());
							break;
						}
					default:
						{
							Console.WriteLine($"Your move: {args[int.Parse(humanMove) - 1]}");
							Console.WriteLine($"Computer move: {args[computer.Move]}");
							Console.WriteLine(GetResult(int.Parse(humanMove) - 1, computer.Move, args.Length));
							Console.WriteLine($"HMAC key: {computer.HMACKey}");
							flag = false;
							break;
						}
				}
			}
		}

		public static bool AreUnique(string[] arr)
		{
			return (new HashSet<string>(arr).Count == arr.Length);
		}

		public static void OutError()
		{
			Console.WriteLine("Invalid start parameters. Number of parameters must be more than 2 and not multiple of 2.");
			Console.WriteLine("Also, the parameters must be unique");
			Console.WriteLine("Examples of valid parameters:");
			Console.WriteLine("rock paper scissors");
			Console.WriteLine("rock paper scissors lizard Spock");
			Console.WriteLine("1 2 3 4 5 6 7");
		}

		public static bool IsValidArgs(int numberOfArgs)
		{
			if (numberOfArgs % 2 == 0 || numberOfArgs < 3)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		static void Main(string[] args)
		{
			if (IsValidArgs(args.Length))
			{
				if (AreUnique(args))
				{
					Menu(args);
				}
				else
				{
					OutError();
				}
				
			}
			else
			{
				OutError();
			}
		}
	}
}
