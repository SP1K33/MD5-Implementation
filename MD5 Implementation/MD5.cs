using System;

namespace MD5_Implementation
{
	public class MD5
	{
		public string Calculate(byte[] source)
		{
			var result = string.Empty;

			return result;
		}

		public string[] Calculate(byte[][] sources)
		{
			var result = new string[sources.Length];

			for (var i = 0; i < sources.Length; i++)
			{
				result[i] = Calculate(sources[i]);
			}

			return result;
		}
	}
}