# FluentAssertion Extensions

[![Build status](https://ci.appveyor.com/api/projects/status/oqio2y57p0hnshnu?svg=true)](https://ci.appveyor.com/project/Kittyfisto/fluentassertions-extensions)
![AppVeyor tests](https://img.shields.io/appveyor/tests/Kittyfisto/fluentassertions-extensions.svg?color=%234CC61E)
[![NuGet](https://img.shields.io/nuget/dt/FluentAssertions.Extensions.svg)](http://nuget.org/packages/FluentAssertions.Extensions)
[![NuGet](https://img.shields.io/nuget/v/FluentAssertions.Extensions.svg)](http://nuget.org/packages/FluentAssertions.Extensions)

Several extensions to the awesome FluentAssertion library.

## Usage

This library features assertions that continously poll a property until the desired
condition is met. Example:

```c#
myObject.Property(x => x.IsFinished).ShouldEventually().BeTrue(TimeSpan.FromSeconds(2));
```

## Credits

Simon Mießler 2017

## License

[MIT](http://opensource.org/licenses/MIT)
