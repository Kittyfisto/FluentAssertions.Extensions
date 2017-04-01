using NUnit.Framework;

namespace FluentAssertions.Extensions.Test
{
	[TestFixture]
	public sealed class EventualAssertionsTest
	{
		[Test]
		public void TestShouldEventuallyBeTrue1()
		{
			var obj = new TestClass1 {IsFinished = true};
			obj.Property(x => x.IsFinished).ShouldEventually().BeTrue();
		}

		[Test]
		public void TestShouldEventuallyBeTrue2()
		{
			var obj = new TestClass1 { IsFinished = false };
			obj.Property(x => x.IsFinished).ShouldEventually().BeFalse();
		}
	}
}