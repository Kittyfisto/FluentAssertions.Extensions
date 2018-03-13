using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FluentAssertions.Extensions.Test
{
	[TestFixture]
	public sealed class EventualAssertionsTest
	{
		[Test]
		public void TestShouldEventuallyBeEmpty1()
		{
			var obj = new TestClass1 {Strings = new string[0]};
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().BeEmpty())
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyBeEmpty2()
		{
			var obj = new TestClass1 {Strings = new string[1]};
			Task.Factory.StartNew(() => obj.Strings = new string[0]);
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().BeEmpty())
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyEqual1()
		{
			var obj = new TestClass1 {Strings = new string[0]};
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().Equal(new string[0]))
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyEqual2()
		{
			var obj = new TestClass1 {Strings = new [] {"bar", "foo"}};
			Task.Factory.StartNew(() => obj.Strings = new [] {"foo", "bar"});
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().Equal(new [] {"foo", "bar"}))
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldAfterEqual1()
		{
			var obj = new TestClass1 {Strings = new [] {"bar", "foo"}};
			new Action(() => obj.Property(x => x.Strings).ShouldAfter(TimeSpan.FromMilliseconds(100))
			                    .Equal(new[] {"foo", "bar"}))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected collection equal to {foo, bar}, but found found {bar, foo} after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldAfterBeEmpty1()
		{
			var obj = new TestClass1 {Strings = null};
			new Action(() => obj.Property(x => x.Strings).ShouldAfter(TimeSpan.FromMilliseconds(100)).BeEmpty())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected empty collection, but found found <null> after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldAfterBeEmpty2()
		{
			var obj = new TestClass1 {Strings = new[] {"foobar"}};

			new Action(() => obj.Property(x => x.Strings).ShouldAfter(TimeSpan.FromMilliseconds(100)).BeEmpty())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected empty collection, but found found {foobar} after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldEventuallyBeTrue1()
		{
			var obj = new TestClass1 {IsFinished = true};
			obj.Property(x => x.IsFinished).ShouldEventually().BeTrue();
		}

		[Test]
		public void TestShouldEventuallyBeTrue2()
		{
			var obj = new TestClass1 {IsFinished = false};
			obj.Property(x => x.IsFinished).ShouldEventually().BeFalse();
		}
	}
}