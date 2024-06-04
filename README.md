# Receipt Overview

A little piece of software, to digitalize Receipts and export them as a .csv to be used in R for data analysis and statistics.

# Initial Setup

The application needs a database in the following path: <ApplicationDirectory>/Data/data.db
After starting the application for the first time, an empty data.db is created. This one can be deleted.
Instead, use the DB Browser for SQLite (https://sqlitebrowser.org/) to run the "create_db.sql" script which is located in the same directory (<ApplicationDirectory>/Data/create_db.sql)
The resulting database needs to be saved as "data.db" in the same directory (<ApplicationDirectory>/Data/data.db).
Afterwards, the application is ready for usage.

## Shortcuts/Hotkeys:

| Hotkey | Function |
| -------- | ------- |
| Ctrl+N | New position |
| Delete | Remove selected position |
| Enter | Add current entry to collection |
| + | Copy selected entry |
| - | Remove selected entry |
| Ctrl+S | Save current position |

Take notice, that the "Delete" Button is a Hotkey and therefore should not be used to remove charactes. Use "Backspace" instead.

![grafik](https://github.com/al-develop/ReceiptOverview/assets/16868184/7512ff2a-de5d-4fbd-bad0-44ea824afbb3)


## Made with

    Avalonia UI 11.0.10 with the build-in Reactive UI MVVM Framework
    Avalonia Datagrid [NuGet]
    Avalonia Forst Theme (https://theme.xaml.live/)
    C# & dotnet 7.0
    SQLite 3 & Microsoft.Data.Sqlite [NuGet]
    Icons8 (https://icons8.com/)

## Build for

    Fedora Linux 39 & 40 (KDE, x11)
    Windows 10

(not tested on other systems, but should build and run wherever the dotnet 7.0 Runtime/SDK is installed)

## For Code-Readers

The goal was fast development, with no plans to expand further than the base functionalities (which are: inserting Receipts and their entries and export everything to a csv file).
Therefore, maintenance was not a priority, which results in a somewhat messy Codebase.

## License

    MIT
