using System;

namespace DTech.Extensions.Runtime
{
	public static class MathExtensions
	{
		public static int ThrowIfNegative(this int value, string fieldName = "")
		{
			if (value < 0)
			{
				if (string.IsNullOrEmpty(fieldName))
				{
					fieldName = nameof(value);
				}
				
				throw new ArgumentException("Value must be non-negative.", fieldName);
			}
			
			return value;
		}
		
		public static float ThrowIfNegative(this float value, string fieldName = "")
		{
			if (value < 0)
			{
				if (string.IsNullOrEmpty(fieldName))
				{
					fieldName = nameof(value);
				}
				
				throw new ArgumentException("Value must be non-negative.", fieldName);
			}
			
			return value;
		}

		public static int ThrowIfOutOfRange(this int value, int min, int max, string fieldName = "")
		{
			if (value < min || value > max)
			{
				if (string.IsNullOrEmpty(fieldName))
				{
					fieldName = nameof(value);
				}
				
				throw new ArgumentException($"Value must be between {min} and {max}.", fieldName);
			}
			
			return value;
		}
		
		public static float ThrowIfOutOfRange(this float value, float min, float max, string fieldName = "")
		{
			if (value < min || value > max)
			{
				if (string.IsNullOrEmpty(fieldName))
				{
					fieldName = nameof(value);
				}
				
				throw new ArgumentException($"Value must be between {min} and {max}.", fieldName);
			}
			
			return value;
		}
	}
}