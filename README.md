UNIT CONVERTER v.1.0

A simple unit converter. The current version supports convertion of the following types of units: Length, Data.

Solution created in the Visual Studio 2015 Community Edition, and based on .NET v.4.5. It includes three projects:
* UnitConverter -- a core of the convertor.
* UnitConverter.Console -- console application that allow convert units using command line.
* UnitConverter.Tests -- unit tests of the UnitConverter class.

Some points for improvement:

* Use interfaces for Prefixes, UnitRatio, Parser, and Processor classes; initialize them via DI or via factory method. It's allow to have different implementations, for example, for prefixes - decimal, and binary for Data conversion.

* The data for Prefixes and UnitRatio can be loaded from some data storage.

* Use short version of the prefixes, like 'kg', 'Gb'.

* Use plural form for units, by using some stemming algorithm.
