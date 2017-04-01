# FluentAssertion Extensions

[![Build status](https://ci.appveyor.com/api/projects/status/oqio2y57p0hnshnu?svg=true)](https://ci.appveyor.com/project/Kittyfisto/fluentassertions-extensions)

Several extensions to the awesome FluentAssertion library.

## Usage

This library features assertions that continously poll a property until the desired
condition is met. Example:

myObject.Property(x => x.IsFinished).ShouldEventually().BeTrue(TimeSpan.FromSeconds(2));

## Credits

Simon Mießler 2017

## License

[MIT](http://opensource.org/licenses/MIT)
