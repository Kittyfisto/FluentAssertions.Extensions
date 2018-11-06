using System.Collections.Generic;

namespace FluentAssertions.Extensions.Test
{
	public sealed class TestClass1
	{
		public bool IsFinished { get; set; }
		public int IntValue { get; set; }
		public string String { get; set; }
		public int? Nullable { get; set; }
		public IEnumerable<string> Strings { get; set; }
	}
}