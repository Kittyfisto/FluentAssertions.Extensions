using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace FluentAssertions
{
	/// <summary>
	///    Contains a number of methods to assert that an System.Object is in an expected state.
	/// </summary>
	public static class EventualAssertionsExtensions
	{
		/// <summary>
		/// Asserts that the current object has not been initialized yet.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void BeNull<T>(this EventualAssertions<T> that, string message = null) where T : class
		{
			that.Be(null, message);
		}

		/// <summary>
		/// Asserts that the current object has been initialized already.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void NotBeNull<T>(this EventualAssertions<T> that, string message = null) where T : class
		{
			that.NotBe(null, message);
		}

		/// <summary>
		/// Asserts that the current object has not been initialized yet.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void NotHaveValue<T>(this EventualAssertions<T?> that, string message = null) where T : struct
		{
			that.BeNull(message);
		}

		/// <summary>
		/// Asserts that the current object has been initialized already.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void HaveValue<T>(this EventualAssertions<T?> that, string message = null) where T : struct
		{
			that.NotBeNull(message);
		}

		/// <summary>
		/// Asserts that the current object has been initialized already.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void NotBeNull<T>(this EventualAssertions<T?> that, string message = null) where T : struct
		{
			if (IsTrue(that, value => value != null, that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected ");
			completeMessage.AppendValue(finalValue);
			completeMessage.Append(" to have a value");
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		/// <summary>
		/// Asserts that the current object has not been initialized yet.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void BeNull<T>(this EventualAssertions<T?> that, string message = null) where T : struct
		{
			if (IsTrue(that, value => value == null, that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected ");
			completeMessage.AppendValue(finalValue);
			completeMessage.Append(" to not have a value");
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		/// <summary>
		/// Asserts that the value is true.
		/// </summary>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void BeTrue(this EventualAssertions<bool> that, string message = null)
		{
			that.Be(expectedValue: true, message: message);
		}

		/// <summary>
		/// Asserts that the value is false.
		/// </summary>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void BeFalse(this EventualAssertions<bool> that, string message = null)
		{
			that.Be(expectedValue: false, message: message);
		}

		/// <summary>
		///     Asserts that the numeric value is greater than the specified expected value.
		/// </summary>
		/// <param name="that"></param>
		/// <param name="threshold"></param>
		/// <param name="message"></param>
		public static void BeGreaterThan(this EventualAssertions<int> that,
		                                 int threshold,
		                                 string message = null)
		{
			if (IsTrue(that, value => value > threshold, that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.AppendFormat("Expected {0} to be greater than {1}",
			                             finalValue,
			                             threshold);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		/// <summary>
		///     Asserts that the numeric value is greater than or equal to the specified expected
		///     value.
		/// </summary>
		/// <param name="that"></param>
		/// <param name="threshold"></param>
		/// <param name="message"></param>
		public static void BeGreaterOrEqualTo(this EventualAssertions<int> that,
		                                      int threshold,
		                                      string message = null)
		{
			if (IsTrue(that, value => value >= threshold, that.MaximumWaitTime, out var finalValue))
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

		/// <summary>
		///     Asserts that an object equals another object using its System.Object.Equals(System.Object)
		///     implementation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="expectedValue"></param>
		/// <param name="message"></param>
		public static void Be<T>(this EventualAssertions<T> that,
		                         T expectedValue,
		                         string message = null)
		{
			if (IsTrue(that, value => Equals(value, expectedValue), that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected ");
			completeMessage.AppendValue(expectedValue);
			completeMessage.Append(", but found ");
			completeMessage.AppendValue(finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		/// <summary>
		///     Asserts that an object does not equal another object using its System.Object.Equals(System.Object)
		///     method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="expectedValue"></param>
		/// <param name="message"></param>
		public static void NotBe<T>(this EventualAssertions<T> that,
		                            T expectedValue,
		                            string message = null)
		{
			if (IsTrue(that, value => !Equals(value, expectedValue), that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected ");
			AppendValue(completeMessage, expectedValue);
			completeMessage.Append(" not to be equal to ");
			AppendValue(completeMessage, finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		/// <summary>
		///     Expects the current collection to contain all the same elements in the same order
		///     as the collection identified by elements. Elements are compared using their T.Equals(T)
		///     method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="expectedEnumeration"></param>
		/// <param name="message"></param>
		public static void Equal<T>(this EventualAssertions<IEnumerable<T>> that,
		                            IEnumerable<T> expectedEnumeration,
		                            string message = null)
		{
			var expectedCopy = expectedEnumeration.ToList();

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
			}, that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected collection equal to ");
			AppendValue(completeMessage, expectedCopy);
			completeMessage.Append(", but found found ");
			completeMessage.AppendFormatEnumeration(finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		/// <summary>
		///     Asserts that the number of items in the collection matches the supplied expected
		///     amount.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="expectedCount"></param>
		/// <param name="message"></param>
		public static void HaveCount<T>(this EventualAssertions<IEnumerable<T>> that, int expectedCount, string message = null)
		{
			if (IsTrue(that, values => values != null && values.Count() == expectedCount, that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.AppendFormat("Expected collection to contain {0} item(s), but found found ", expectedCount);
			completeMessage.AppendFormatCount(finalValue);
			completeMessage.AppendWaitTime(that.MaximumWaitTime);
			completeMessage.AppendMessage(message);
			completeMessage.Append(".");

			throw new AssertionException(completeMessage.ToString());
		}

		/// <summary>
		///     Asserts that the collection does not contain any items.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="message"></param>
		public static void BeEmpty<T>(this EventualAssertions<IEnumerable<T>> that, string message = null)
		{
			if (IsTrue(that, values => values != null && !values.Any(), that.MaximumWaitTime, out var finalValue))
				return;

			var completeMessage = new StringBuilder();
			completeMessage.Append("Expected empty collection, but found found ");
			completeMessage.AppendFormatEnumeration(finalValue);
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

		private static void AppendValue<T>(this StringBuilder builder, T value)
		{
			if (value == null)
			{
				builder.AppendNull();
			}
			else
			{
				if (!(value is string) && value is IEnumerable)
				{
					dynamic tmp = value;
					AppendFormatEnumeration(builder, tmp);
				}
				else
				{
					builder.Append(value);
				}
			}
		}

		private static void AppendFormatEnumeration<T>(this StringBuilder builder, IEnumerable<T> values)
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