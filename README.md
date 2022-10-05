# Din91379-csharp

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/flxbe/Din91379/blob/main/LICENSE)
[![ci](https://github.com/flxbe/Din91379/actions/workflows/CI.yml/badge.svg)](https://github.com/flxbe/Din91379/actions/workflows/CI.yml)

A C# implementation of [DIN 91379:2022-08](https://www.beuth.de/de/norm/din-91379/353496133),
including search form generation for the data types `TypeA`, `TypeB` and `TypeC` as defined in
[Umstellung auf Lateinische Zeichen in Unicode – Vorgaben für Identifikationsverfahren](https://xoev.de/latinchars/1_1/supplement/identverfahren.pdf).

- **Strict**: All data types are Unicode NFC normalized and contain only
  non-deprecated glyphs as defined in DIN 91379. Any string containing invalid
  glyphs is rejected when trying to construct any of the data types.

- **Ergonomic**: When constructing any of the data types, the
  string is correctly normalized and deprecated glyphs are converted to their
  corresponding replacement automatically. In addition, static methods for checking
  and converting strings are available without the need to actually construct any of
  the data types.
