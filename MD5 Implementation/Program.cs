using System;
using System.IO;
using System.Text;

namespace MD5_Implementation
{
	public static class Program
	{
		private static readonly MD5 _md5 = new MD5();

		private static void ProcessFiles(string[] paths)
		{
			var validFiles = Utils.GetValidFiles(paths);
			var fileBytes = Utils.GetFilesBytes(validFiles);
			var results = _md5.Calculate(fileBytes);
			for (var i = 0; i < results.Length; i++)
			{
				Console.WriteLine($"File -> {Path.GetFileName(validFiles[i])}\nMD5 Hash -> {results[i]}");
			}
			Console.ReadLine();
		}

		private static void ProcessInput()
		{
			while (true)
			{
				Console.Write("String to hash -> ");
				var byteArray = Encoding.ASCII.GetBytes(Console.ReadLine());
				var hash = _md5.Calculate(byteArray);
				Console.WriteLine($"MD5 hash -> {hash}");
			}
		}

		private static void Main(string[] arguments)
		{
			if (arguments.Length > 0)
			{
				ProcessFiles(arguments);
			}
			else
			{
				ProcessInput();
			}
		}
	}
}
