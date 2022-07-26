using System;
using System.IO;

namespace MD5_Implementation
{
	public static class Program
	{
		private static readonly MD5 _md5 = new MD5();

		private static void ProcessFiles(string[] paths)
		{
			var validFiles = Utils.GetValidFiles(paths);
			var fileBytes = Utils.GetFilesBytes(validFiles);
			var hashes = _md5.Calculate(fileBytes);
			for (var i = 0; i < hashes.Length; i++)
			{
				string result = string.Concat(hashes[i], " *", validFiles[i], '\n');
				Console.Write(result);
				File.AppendAllText(".md5", result);
			}

			Console.WriteLine();
			Console.WriteLine("Press Enter to exit...");
			Console.ReadLine();
		}

		private static void Main(string[] arguments)
		{
			if (arguments.Length > 0)
			{
				ProcessFiles(arguments);
			}
		}
	}
}
