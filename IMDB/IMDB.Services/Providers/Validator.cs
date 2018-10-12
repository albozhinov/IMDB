using System;

namespace IMDB.Services.Providers
{
    public static class Validator
    {
        public static void IfNull<T>(object o, string msg) where T : Exception, new()
        {
            if (o is null)
                throw (T)Activator.CreateInstance(typeof(T), msg);

		}
		public static void IfNull<T>(object o) where T : Exception, new()
		{
			if (o is null)
				throw new T();
		}
		public static void IfIsInRangeInclusive<T>(T item, T min, T max, string msg) where T : IComparable
		{
			if (item.CompareTo(min) < 0 || item.CompareTo(max) > 0)
				throw new ArgumentException(msg);
		}
		public static void IsNonNegative(int item, string msg)
		{
			if (item < 0)
				throw new ArgumentException(msg);
		}
		public static void IsPositive(int item, string msg)
		{
			if (item <= 0)
				throw new ArgumentOutOfRangeException(msg);
		}
		public static void IsLetter(char symbol, string msg)
		{
			if (!Char.IsLetter(symbol))
				throw new ArgumentException(msg);
		}

		public static void DoesNotContainWhiteSpaces(string source, string msg)
		{
			if(source.Contains(' '))
				throw new ArgumentException(msg);
			if (source.Contains('	'))
				throw new ArgumentException(msg);
		}
    }
}
