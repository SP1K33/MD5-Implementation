using System.Collections.Generic;
using System.IO;

namespace MD5_Implementation
{
	public static class Utils
	{
		private static bool CheckFileAvailability(string path)
		{
			bool result = true;
			try
			{
				var stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
				stream.Close();
			}
			catch (IOException)
			{
				result = false;
			}
			return result;
		}

		public static string[] GetValidFiles(string[] filePaths)
		{
			var result = new List<string>();
			foreach (var path in filePaths)
			{
				if (File.Exists(path) && CheckFileAvailability(path))
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
				result.Add(File.ReadAllBytes(path));
			}
			return result.ToArray();
		}
	}
}
