using System;

namespace MD5_Implementation
{
	public class MD5
	{
		private uint[] _tConstantTable;
		private const int _blockLength = 512;
		private uint[] _x;

		public enum LittleEndianConstants : uint
		{
			A = 0x67452301,
			B = 0xEFCDAB89,
			C = 0x98BADCFE,
			D = 0X10325476
		}

		private uint[] CalcConstantTable()
		{
			const int tableSize = 64;
			var result = new uint[tableSize];

			for (int i = 0; i < tableSize; ++i)
			{
				result[i] = (uint)(Math.Pow(2f, 32f) * Math.Abs(Math.Sin(i + 1f)));
			}

			return result;
		}

		private byte[] PadBytes(byte[] byteArray)
		{
			var block64 = (ulong)(byteArray.Length * 8);
			
			var numberOfPaddingBits = (uint)((448 - block64 % _blockLength + _blockLength) % _blockLength);
			if (numberOfPaddingBits == 0)
			{
				numberOfPaddingBits = _blockLength;
			}

			var bufferSizeMultipleBytes = (uint) (byteArray.Length + numberOfPaddingBits / 8 + 8);

			var result = new byte[bufferSizeMultipleBytes];

			for (int i = 0; i < byteArray.Length; ++i)
			{
				result[i] = byteArray[i];
			}

			result[byteArray.Length] |= 0x80;

			for (int i = 8; i > 0; --i)
			{
				result[bufferSizeMultipleBytes - i] = (byte)(block64 >> ((8 - i) * 8) & 0xFF);
			}

			return result;
		}

		private void CopyBlock(byte[] byteArray, uint i)
		{
			i <<= 6;

			for (uint j = 0; j < 61; j += 4)
			{
				_x[j >> 2] = ((uint)byteArray[i + j + 3] << 24) |
				             ((uint)byteArray[i + j + 2] << 16) |
				             ((uint)byteArray[i + j + 1] << 8) |
				             byteArray[i + j];
			}
		}

		private uint RotateLeft(uint number, ushort shift)
		{
			return (number >> 32 - shift) | (number << shift);
		}

		private void FunF(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + RotateLeft(a + ((b & c) | (~b & d)) + _x[k] + _tConstantTable[i - 1], s);
		}

		private void FunG(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + RotateLeft(a + ((b & d) | (c & ~d)) + _x[k] + _tConstantTable[i - 1], s);
		}

		private void FunH(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + RotateLeft(a + (b ^ c ^ d) + _x[k] + _tConstantTable[i - 1], s);
		}

		private void FunI(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + RotateLeft(a + (c ^ (b | ~d)) + _x[k] + _tConstantTable[i - 1], s);
		}

		private (uint, uint, uint, uint) Transform()
		{
			uint A = (uint)LittleEndianConstants.A;
			uint B = (uint)LittleEndianConstants.B;
			uint C = (uint)LittleEndianConstants.C;
			uint D = (uint)LittleEndianConstants.D;

			
			FunF(ref A, B, C, D, 0, 7, 1); 
			FunF(ref D, A, B, C, 1, 12, 2); 
			FunF(ref C, D, A, B, 2, 17, 3); 
			FunF(ref B, C, D, A, 3, 22, 4);
			FunF(ref A, B, C, D, 4, 7, 5); 
			FunF(ref D, A, B, C, 5, 12, 6); 
			FunF(ref C, D, A, B, 6, 17, 7); 
			FunF(ref B, C, D, A, 7, 22, 8);
			FunF(ref A, B, C, D, 8, 7, 9); 
			FunF(ref D, A, B, C, 9, 12, 10); 
			FunF(ref C, D, A, B, 10, 17, 11); 
			FunF(ref B, C, D, A, 11, 22, 12);
			FunF(ref A, B, C, D, 12, 7, 13); 
			FunF(ref D, A, B, C, 13, 12, 14); 
			FunF(ref C, D, A, B, 14, 17, 15);
			FunF(ref B, C, D, A, 15, 22, 16);

			
			FunG(ref A, B, C, D, 1, 5, 17); 
			FunG(ref D, A, B, C, 6, 9, 18); 
			FunG(ref C, D, A, B, 11, 14, 19); 
			FunG(ref B, C, D, A, 0, 20, 20);
			FunG(ref A, B, C, D, 5, 5, 21); 
			FunG(ref D, A, B, C, 10, 9, 22); 
			FunG(ref C, D, A, B, 15, 14, 23); 
			FunG(ref B, C, D, A, 4, 20, 24);
			FunG(ref A, B, C, D, 9, 5, 25); 
			FunG(ref D, A, B, C, 14, 9, 26); 
			FunG(ref C, D, A, B, 3, 14, 27); 
			FunG(ref B, C, D, A, 8, 20, 28);
			FunG(ref A, B, C, D, 13, 5, 29); 
			FunG(ref D, A, B, C, 2, 9, 30); 
			FunG(ref C, D, A, B, 7, 14, 31); 
			FunG(ref B, C, D, A, 12, 20, 32);

			
			FunH(ref A, B, C, D, 5, 4, 33); 
			FunH(ref D, A, B, C, 8, 11, 34); 
			FunH(ref C, D, A, B, 11, 16, 35);
			FunH(ref B, C, D, A, 14, 23, 36);
			FunH(ref A, B, C, D, 1, 4, 37);
			FunH(ref D, A, B, C, 4, 11, 38);
			FunH(ref C, D, A, B, 7, 16, 39); 
			FunH(ref B, C, D, A, 10, 23, 40);
			FunH(ref A, B, C, D, 13, 4, 41); 
			FunH(ref D, A, B, C, 0, 11, 42); 
			FunH(ref C, D, A, B, 3, 16, 43); 
			FunH(ref B, C, D, A, 6, 23, 44);
			FunH(ref A, B, C, D, 9, 4, 45); 
			FunH(ref D, A, B, C, 12, 11, 46); 
			FunH(ref C, D, A, B, 15, 16, 47); 
			FunH(ref B, C, D, A, 2, 23, 48);


			FunI(ref A, B, C, D, 0, 6, 49); 
			FunI(ref D, A, B, C, 7, 10, 50);
			FunI(ref C, D, A, B, 14, 15, 51);
			FunI(ref B, C, D, A, 5, 21, 52);
			FunI(ref A, B, C, D, 12, 6, 53);
			FunI(ref D, A, B, C, 3, 10, 54); 
			FunI(ref C, D, A, B, 10, 15, 55);
			FunI(ref B, C, D, A, 1, 21, 56);
			FunI(ref A, B, C, D, 8, 6, 57); 
			FunI(ref D, A, B, C, 15, 10, 58);
			FunI(ref C, D, A, B, 6, 15, 59); 
			FunI(ref B, C, D, A, 13, 21, 60);
			FunI(ref A, B, C, D, 4, 6, 61); 
			FunI(ref D, A, B, C, 11, 10, 62);
			FunI(ref C, D, A, B, 2, 15, 63);
			FunI(ref B, C, D, A, 9, 21, 64);

			A += (uint)LittleEndianConstants.A;
			B += (uint)LittleEndianConstants.B;
			C += (uint)LittleEndianConstants.C;
			D += (uint)LittleEndianConstants.D;

			return (A, B, C, D);
		}

		private uint ReverseByte(uint number)
		{
			return ((number & 0xFF) << 24) |
			       (number >> 24) |
			       ((number & 0xFF0000) >> 8) |
			       ((number & 0xFF00) << 8);
		}

		public string Calculate(byte[] source)
		{
			var paddedBytes = PadBytes(source);
			var block64 = (ulong)(paddedBytes.Length * 8);
			var block32 = (uint)(block64 / 32);

			if (_tConstantTable == null)
			{
				_tConstantTable = CalcConstantTable();
			}

			_x = new uint[16];

			(uint, uint, uint, uint) values = (0, 0, 0, 0);

			for (uint i = 0; i < block32 / 16; ++i)
			{
				CopyBlock(paddedBytes, i);
				values = Transform();
			}

			var result = (ReverseByte(values.Item1).ToString("x8")
			             + ReverseByte(values.Item2).ToString("x8")
			             + ReverseByte(values.Item3).ToString("x8")
			             + ReverseByte(values.Item4).ToString("x8")).ToUpper();

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