using System;

namespace DTech.Extensions.Runtime
{
	public static class StringExtensions
	{
		public static string ThrowIfNullOrEmpty(this string source)
		{
			if (string.IsNullOrEmpty(source))
			{
				throw new NullReferenceException();
			}

			return source;
		}
        
		public static string ThrowIfNullOrEmpty(this string source, string message)
		{
			if (string.IsNullOrEmpty(source))
			{
				throw new NullReferenceException(message);
			}

			return source;
		}
        
		public static string ThrowIfNullOrEmpty(this string source, Exception exception)
		{
			if (string.IsNullOrEmpty(source))
			{
				throw exception;
			}

			return source;
		}

		public static string GetDefaultIfNullOrEmpty(this string source, string defaultValue) => string.IsNullOrEmpty(source) ? defaultValue : source;
	}
}