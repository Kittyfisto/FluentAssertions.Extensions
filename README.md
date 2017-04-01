# FluentAssertion Extensions

[![Build status]()]()

Several extensions to the awesome FluentAssertion library.

## Usage

This library features assertions that continously poll a property until the desired
condition is met. Example:

myObject.Property(x => x.IsFinished).ShouldEventually().BeTrue(TimeSpan.FromSeconds(2));

## Credits

Simon Mieﬂler 2017

## License

[MIT](http://opensource.org/licenses/MIT)
