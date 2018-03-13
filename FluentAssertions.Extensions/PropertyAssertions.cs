using System;

namespace FluentAssertions
{
	public sealed class PropertyAssertions<T, TProperty>
	{
		private readonly T _subject;
		private readonly Func<T, TProperty> _getter;

		public PropertyAssertions(T subject, Func<T, TProperty> getter)
		{
			_subject = subject;
			_getter = getter;
		}

		/// <summary>
		///     Starts a blocking observation of an object's property which performs busy
		///     waiting until a certain assertion is fullfilled or a given amount of time elapses.
		/// </summary>
		/// <returns></returns>
		public EventualAssertions<TProperty> ShouldEventually()
		{
			return new EventualAssertions<TProperty>(() => _getter(_subject));
		}

		/// <summary>
		///     Starts a blocking observation of an object's property which performs busy
		///     waiting until a certain assertion is fullfilled or the given amount of time elapses.
		/// </summary>
		/// <param name="maximumWaitTime"></param>
		/// <returns></returns>
		public EventualAssertions<TProperty> ShouldAfter(TimeSpan maximumWaitTime)
		{
			return new EventualAssertions<TProperty>(() => _getter(_subject), maximumWaitTime);
		}
	}
}