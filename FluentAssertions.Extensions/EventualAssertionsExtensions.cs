﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace FluentAssertions
{
	public static class EventualAssertionsExtensions
	{
		public static void BeTrue(this EventualAssertions<bool> that, string message = null)
		{
			that.Be(expectedValue: true, message: message);
		}

		public static void BeFalse(this EventualAssertions<bool> that, string message = null)
		{
			that.Be(expectedValue: false, message: message);
		}

		public static void BeGreaterOrEqual(this EventualAssertions<int> that,
		                                    int threshold,
		                                    string message = null)
		{
			int finalValue;
			if (IsTrue(that, value => value >= threshold, that.MaximumWaitTime, out finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.AppendFormat("Expected {0} to be greater or equal to {1}",
			                             finalValue,
			                             threshold);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		public static void Be<T>(this EventualAssertions<T> that,
		                         T expectedValue,
		                         string message = null)
		{
			T finalValue;
			if (IsTrue(that, value => Equals(value, expectedValue), that.MaximumWaitTime, out finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.AppendFormat("Expected {0}, but found found {1}",
			                             expectedValue,
			                             finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		public static void Equal<T>(this EventualAssertions<IEnumerable<T>> that,
		                            IEnumerable<T> expectedEnumeration,
		                            string message = null)
		{
			var expectedCopy = expectedEnumeration.ToList();

			IEnumerable<T> finalValue;
			if (IsTrue(that, values =>
			{
				var copy = values?.ToList();
				if (copy?.Count != expectedCopy.Count)
					return false;

				for (int i = 0; i < copy.Count; ++i)
				{
					if (!Equals(copy[i], expectedCopy[i]))
						return false;
				}

				return true;
			}, that.MaximumWaitTime, out finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected collection equal to ");
			completeMessage.AppendFormat(expectedCopy);
			completeMessage.Append(", but found found ");
			completeMessage.AppendFormat(finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		public static void HaveCount<T>(this EventualAssertions<IEnumerable<T>> that, int expectedCount, string message = null)
		{
			IEnumerable<T> finalValue;
			if (IsTrue(that, values => values != null && values.Count() == expectedCount, that.MaximumWaitTime, out finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.AppendFormat("Expected collection to contain {0} item(s), but found found ", expectedCount);
			completeMessage.AppendFormatCount(finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		public static void BeEmpty<T>(this EventualAssertions<IEnumerable<T>> that, string message = null)
		{
			IEnumerable<T> finalValue;
			if (IsTrue(that, values => values != null && !values.Any(), that.MaximumWaitTime, out finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected empty collection, but found found ");
			completeMessage.AppendFormat(finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		private static bool IsTrue<T>(this EventualAssertions<T> that,
		                              Predicate<T> predicate,
		                              TimeSpan maximumWaitTime,
		                              out T finalValue)
		{
			var started = DateTime.UtcNow;
			do
			{
				finalValue = that.GetValue();
				if (predicate(finalValue))
					return true;

				Thread.Sleep(EventualAssertions<int>.DefaultSleepTime);
			} while (DateTime.UtcNow - started < maximumWaitTime);

			return false;
		}

		private static void AppendMessage(this StringBuilder builder, string message)
		{
			if (!string.IsNullOrEmpty(message))
				builder.AppendFormat(
				                     message.StartsWith("because", StringComparison.InvariantCultureIgnoreCase)
					                     ? ", {0}"
					                     : ", because {0}"
				                     , message);
		}

		private static void AppendWaitTime(this StringBuilder builder, TimeSpan timespan)
		{
			builder.Append(" after waiting for ");
			if (timespan > TimeSpan.FromSeconds(value: 1))
				builder.AppendFormat("{0} second(s)", timespan.TotalSeconds);
			else
				builder.AppendFormat("{0} ms", timespan.TotalMilliseconds);
		}

		private static void AppendFormatCount<T>(this StringBuilder builder, IEnumerable<T> values)
		{
			if (values == null)
			{
				builder.AppendNull();
			}
			else
			{
				builder.Append(values.Count());
			}
		}

		private static void AppendFormat<T>(this StringBuilder builder, IEnumerable<T> values)
		{
			if (values == null)
			{
				builder.AppendNull();
			}
			else
			{
				builder.Append("{");
				const int maximumNumberOfDisplayedValues = 10;
				var copy = values.ToList();

				int displayCount = Math.Min(copy.Count, maximumNumberOfDisplayedValues);
				for (int i = 0; i < displayCount; ++i)
				{
					if (i > 0)
						builder.Append(", ");
					builder.Append(copy[i]);
				}

				if (copy.Count > maximumNumberOfDisplayedValues)
					builder.Append(", ...");
				builder.Append("}");
			}
		}

		private static void AppendNull(this StringBuilder builder)
		{
			builder.Append("<null>");
		}
	}
}