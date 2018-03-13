using System;

namespace FluentAssertions
{
	public sealed class EventualAssertions<TProperty>
	{
		/// <summary>
		///     The amount of time any of the BeXYZ methods block before failing, if no custom
		///     maximumWaitTime has been specified.
		/// </summary>
		public static readonly TimeSpan DefaultMaximumWaitTime = TimeSpan.FromSeconds(value: 10);

		/// <summary>
		///     The amount of time which is elapsed between two passes to verify if a particular
		///     assertions is true now.
		/// </summary>
		public static readonly TimeSpan DefaultSleepTime = TimeSpan.FromMilliseconds(value: 10);

		private readonly Func<TProperty> _getter;
		private readonly TimeSpan _maximumWaitTime;

		public EventualAssertions(Func<TProperty> getter)
			: this(getter, DefaultMaximumWaitTime)
		{
			_getter = getter;
		}

		public EventualAssertions(Func<TProperty> getter, TimeSpan maximumWaitTime)
		{
			_maximumWaitTime = maximumWaitTime;
			_getter = getter;
		}

		public TimeSpan MaximumWaitTime => _maximumWaitTime;

		public TProperty GetValue()
		{
			return _getter();
		}
	}
}