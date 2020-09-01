using System;
using System.Text;

namespace MD5_Implementation
{
	public static class Program
	{
		private static void Main(string[] arguments)
		{
			var md5 = new MD5();

			if (arguments.Length > 0)
			{
				var validFiles = Utils.GetValidFiles(arguments);
				var fileBytes = Utils.GetFilesBytes(validFiles);
				var results = md5.Calculate(fileBytes);
				for (var i = 0; i < results.Length; i++)
				{
					Console.WriteLine($"{validFiles[i]} -> {results[i]}");
				}
			}
			else
			{
				Console.WriteLine("String to hash -> ");
				var byteArray = Encoding.ASCII.GetBytes(Console.ReadLine());
				var hash = md5.Calculate(byteArray);
				Console.WriteLine($"Hash -> {hash}");
			}
		}
	}
}
