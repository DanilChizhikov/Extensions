using System;

namespace MbsCore.Extensions
{
    public static class ClassExtensions
    {
        public static T ThrowIfNull<T>(this T source) where T : class
        {
            if (source == null)
            {
                throw new NullReferenceException();
            }

            return source;
        }
        
        public static T ThrowIfNull<T>(this T source, string message) where T : class
        {
            if (source == null)
            {
                throw new NullReferenceException(message);
            }

            return source;
        }
        
        public static T ThrowIfNull<T>(this T source, Exception exception)
            where T : class
        {
            if (source == null)
            {
                throw exception;
            }

            return source;
        }
        
        public static T GetDefaultIfNull<T>(this T source, T defaultValue) where T : class => source ?? defaultValue;

        public static bool IsNull(this object source) => source == null;
    }
}