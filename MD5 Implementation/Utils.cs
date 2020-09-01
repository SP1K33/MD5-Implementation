using System.Collections.Generic;
using System.IO;

namespace MD5_Implementation
{
	public static class Utils
	{
		public static string[] GetValidFiles(string[] filePaths)
		{
			var result = new List<string>();

			foreach (var path in filePaths)
			{
				if (File.Exists(path))
				{
					result.Add(path);
				}
			}

			return result.ToArray();
		}

		public static byte[][] GetFilesBytes(string[] filePaths)
		{
			var result = new List<byte[]>();
			foreach (var path in filePaths)
			{
				if (File.Exists(path))
				{
					result.Add(File.ReadAllBytes(path));
				}
			}
			return result.ToArray();
		}
	}
}
