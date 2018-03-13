using System;

namespace FluentAssertions
{
	public static class ObjectExtensions
	{
		/// <summary>
		///     Captures read-access to a property.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="that"></param>
		/// <param name="getter"></param>
		/// <returns></returns>
		public static PropertyAssertions<T, TProperty> Property<T, TProperty>(this T that, Func<T, TProperty> getter)
		{
			return new PropertyAssertions<T, TProperty>(that, getter);
		}
	}
}