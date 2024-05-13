# Receipt Overview

A little piece of software, to digitalize Receipts and export them as a .csv to be used in R for data analysis and statistics.

## Shortcuts/Hotkeys:

| Hotkey | Function |
| -------- | ------- |
| Ctrl+N | New position |
| Delete | Remove selected position |
| Enter | Add current entry to collection |
| + | Copy selected entry |
| - | Remove selected entry |
| Ctrl+S | Save current position |


![grafik](https://github.com/al-develop/ReceiptOverview/assets/16868184/3c5a2b7a-e095-4d71-b479-bd364153f831)


## Made with

    Avalonia UI 11.0.10 with the build-in Reactive UI MVVM Framework
    Avalonia Datagrid [NuGet]
    Avalonia Forst Theme (https://theme.xaml.live/)
    C# & dotnet 7.0
    SQLite 3 & Microsoft.Data.Sqlite [NuGet]
    Icons8 (https://icons8.com/)

## Build for

    Fedora Linux
    Windows 10

(not tested on other systems, but should build and run wherever the dotnet 7.0 Runtime/SDK is installed)

## For Code-Readers

The goal was fast development, with no plans to expand further than the base functionalities (which are: inserting Receipts and their entries and export everything to a csv file).
Therefore, maintenance was not a priority, which results in a somewhat messy Codebase.

## License

    MIT
