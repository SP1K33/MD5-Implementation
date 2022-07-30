using MD5Algorithm;
using System;
using System.IO;

namespace MD5Wrapper
{
	internal class Program
	{
		private static void ProcessFiles(string[] paths)
		{
			var validFiles = Utils.GetValidFiles(paths);
			var fileBytes = Utils.GetFilesBytes(validFiles);
			string[] hashes = new string[fileBytes.Length];

			for (int i = 0; i < fileBytes.Length; i++)
			{
				hashes[i] = MD5.Calculate(fileBytes[i]);
			}

			for (var i = 0; i < hashes.Length; i++)
			{
				string result = string.Concat(hashes[i], " *", validFiles[i], '\n');
				Console.Write(result);
				File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".md5"), result);
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
