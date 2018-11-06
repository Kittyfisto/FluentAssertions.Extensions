using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FluentAssertions.Extensions.Test
{
	[TestFixture]
	public sealed class EventualAssertionsTest
	{
		[Test]
		public void TestShouldEventuallyBeGreaterOrEqualTo1()
		{
			var obj = new TestClass1 {IntValue = 42};
			new Action(() => obj.Property(x => x.IntValue).ShouldEventually().BeGreaterOrEqualTo(41)).ShouldNotThrow();
			new Action(() => obj.Property(x => x.IntValue).ShouldEventually().BeGreaterOrEqualTo(42)).ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyBeGreaterOrEqualTo2()
		{
			var obj = new TestClass1 {IntValue = 42};
			new Action(() => obj.Property(x => x.IntValue).ShouldAfter(TimeSpan.FromMilliseconds(100)).BeGreaterOrEqualTo(43))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected 42 to be greater or equal to 43 after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldEventuallyBeGreaterThan1()
		{
			var obj = new TestClass1 {IntValue = 42};
			new Action(() => obj.Property(x => x.IntValue).ShouldEventually().BeGreaterThan(41)).ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyBeGreaterThan2()
		{
			var obj = new TestClass1 {IntValue = 42};
			new Action(() => obj.Property(x => x.IntValue).ShouldAfter(TimeSpan.FromMilliseconds(100)).BeGreaterThan(42))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected 42 to be greater than 42 after waiting for 100 ms.");
			new Action(() => obj.Property(x => x.IntValue).ShouldAfter(TimeSpan.FromMilliseconds(100)).BeGreaterThan(43))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected 42 to be greater than 43 after waiting for 100 ms.");
		}

		[Test]
		public void TestNullableShouldEventuallyNotBeNull1()
		{
			var obj = new TestClass1 {Nullable = 42};
			new Action(() => obj.Property(x => x.Nullable).ShouldEventually().NotBeNull()).ShouldNotThrow();
			new Action(() => obj.Property(x => x.Nullable).ShouldEventually().HaveValue()).ShouldNotThrow();
		}

		[Test]
		public void TestNullableShouldEventuallyNotBeNull2()
		{
			var obj = new TestClass1 {Nullable = null};
			new Action(() => obj.Property(x => x.Nullable).ShouldAfter(TimeSpan.FromMilliseconds(100)).NotBeNull())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected <null> to have a value after waiting for 100 ms.");

			new Action(() => obj.Property(x => x.Nullable).ShouldAfter(TimeSpan.FromMilliseconds(100)).HaveValue())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected <null> to have a value after waiting for 100 ms.");
		}

		[Test]
		public void TestNullableShouldEventuallyBeNull1()
		{
			var obj = new TestClass1 {Nullable = null};
			new Action(() => obj.Property(x => x.Nullable).ShouldEventually().BeNull()).ShouldNotThrow();
			new Action(() => obj.Property(x => x.Nullable).ShouldEventually().NotHaveValue()).ShouldNotThrow();
		}

		[Test]
		public void TestNullableShouldEventuallyBeNull2()
		{
			var obj = new TestClass1 {Nullable = 42};
			new Action(() => obj.Property(x => x.Nullable).ShouldAfter(TimeSpan.FromMilliseconds(100)).BeNull())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected 42 to not have a value after waiting for 100 ms.");

			new Action(() => obj.Property(x => x.Nullable).ShouldAfter(TimeSpan.FromMilliseconds(100)).NotHaveValue())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected 42 to not have a value after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldEventuallyNotBe1()
		{
			var obj = new TestClass1 {Strings = new string[0]};
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().NotBe(null))
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyNotBe2()
		{
			var obj = new TestClass1 {Strings = null};
			new Action(() => obj.Property(x => x.Strings).ShouldAfter(TimeSpan.FromMilliseconds(100)).NotBe(null))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected <null> not to be equal to <null> after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldEventuallyNotBe3()
		{
			var obj = new TestClass1 {String = "foo"};
			new Action(() => obj.Property(x => x.String).ShouldAfter(TimeSpan.FromMilliseconds(100)).NotBe("foo"))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected foo not to be equal to foo after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldEventuallyBeNull1()
		{
			var obj = new TestClass1 {Strings = null};
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().BeNull())
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyBeNull2()
		{
			var obj = new TestClass1 {String = "aw"};
			new Action(() => obj.Property(x => x.String).ShouldAfter(TimeSpan.FromMilliseconds(100)).BeNull())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected <null>, but found aw after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldEventuallyNotBeNull1()
		{
			var obj = new TestClass1 {String = "foo"};
			new Action(() => obj.Property(x => x.String).ShouldEventually().NotBeNull())
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyNotBeNull2()
		{
			var obj = new TestClass1 {String = null};
			new Action(() => obj.Property(x => x.String).ShouldAfter(TimeSpan.FromMilliseconds(100)).NotBeNull())
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected <null> not to be equal to <null> after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldEventuallyHaveCount1()
		{
			var obj = new TestClass1 {Strings = new string[0]};
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().HaveCount(0))
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldEventuallyHaveCount2()
		{
			var obj = new TestClass1 {Strings = new string[0]};
			Task.Factory.StartNew(() => obj.Strings = new string[1]);
			new Action(() => obj.Property(x => x.Strings).ShouldEventually().HaveCount(1))
				.ShouldNotThrow();
		}

		[Test]
		public void TestShouldAfterHaveCount1()
		{
			var obj = new TestClass1 {Strings = null};

			new Action(() => obj.Property(x => x.Strings).ShouldAfter(TimeSpan.FromMilliseconds(100)).HaveCount(1))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected collection to contain 1 item(s), but found found <null> after waiting for 100 ms.");
		}

		[Test]
		public void TestShouldAfterHaveCount2()
		{
			var obj = new TestClass1 {Strings = new[] {"foobar"}};

			new Action(() => obj.Property(x => x.Strings).ShouldAfter(TimeSpan.FromMilliseconds(100)).HaveCount(2))
				.ShouldThrow<AssertionException>()
				.WithMessage("Expected collection to contain 2 item(s), but found found 1 after waiting for 100 ms.");
		}

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
