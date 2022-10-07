# Din91379-csharp

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/flxbe/Din91379/blob/main/LICENSE)
[![ci](https://github.com/flxbe/Din91379/actions/workflows/CI.yml/badge.svg)](https://github.com/flxbe/Din91379/actions/workflows/CI.yml)

A C# implementation of [DIN 91379:2022-08](https://www.beuth.de/de/norm/din-91379/353496133), the replacement of
[DIN SPEC 91379:2019-03](https://www.beuth.de/de/technische-regel/din-spec-91379/301228458)
(also known as
[String.Latin+ 1.2](https://www.xoev.de/sixcms/media.php/13/StringLatin%2012.zip)).
The data types `TypeA`, `TypeB` and `TypeC` have additional support for search form creation as defined in
[Umstellung auf Lateinische Zeichen in Unicode – Vorgaben für Identifikationsverfahren](https://xoev.de/latinchars/1_1/supplement/identverfahren.pdf).

- **Strict**: All data types are Unicode NFC normalized and contain only
  characters and sequences from DIN 91379. Any string containing invalid
  glyphs is rejected.

- **Ergonomic**: When constructing any of the data types, the
  string is correctly normalized automatically. In addition, static methods for checking
  and converting strings are available without the need to actually construct any of
  the data types.

- **Efficient**: The custom validation algorithm is **up to 4x faster** than using the compiled regular expression
  as attached in DIN SPEC 91379:2019-03.

The implementation is fully tested against the complete `latinchars.xml` dataset as attached in DIN SPEC 91379:2019-03,
extended by the newly introduced characters in DIN 91379:2022-08.

## License

MIT License

Copyright (c) 2022 Felix Bernhardt

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
