# ArkDiffDisplayer

ArkDiffDisplayer is a C# web application that allows users to compare two CSV files and create a diff of them to see how positions have changed and if there were any new positions. The purpose of this application is to provide market analysis.

## Features

- Allows users to upload and download CSV files for comparison.
- Parses the data from CSV files.
- Creates a difference between the two CSV files.
- Outputs the difference to the console as a string or to a simple website.

## Structure of the project

The project is divided into 3 main classes:

### Clasess

1. `ArkDiffDisplayer`: Responsible for managing all the funcionality that's divided into 4 interfaces.
2. `ArkDiffDisplayerTests`: Responsible for testing the whole project and it's every class.
3. `ArkDiffDisplayerUI`: Responsible for creating some basic UI for the application.

### Interfaces
1. `IDataManagement`: Responsible for downloading and fetching the CSV files.
2. `IParser`: Responsible for parsing the data from the CSV files.
3. `IDiffCreater`: Responsible for creating the difference between the two CSV files.
4. `IOutputCreator`: Responsible for creating the output either to the console as a string or to a simple website.

## Authors

- Bc. Marek Jankola, [485425]
- Bc. Marek Macho, [485281]
- Bc. Dávid Maslo, [485351]
- Bc. Valentína Monková, [485066]
- Bc. Katarína Platková, [493144]
- Bc. Matúš Valko, [484962]
- Bc. Adam Zálešák, [493071]
- Bc. Tomáš Žilínek, [485115]

[485115]:https://is.muni.cz/auth/osoba/485115
[493071]:https://is.muni.cz/auth/osoba/adamzalesak
[484962]:https://is.muni.cz/auth/osoba/484962
[493144]:https://is.muni.cz/auth/osoba/493144
[485351]:https://is.muni.cz/auth/osoba/485351
[485066]:https://media.giphy.com/media/uELtzAhhqpRKg/giphy.gif
[485281]:https://is.muni.cz/auth/osoba/m.macho
[485425]:https://is.muni.cz/auth/osoba/mjankola

## Testing

The application is fully tested using the NuGet package FakeItEasy and Unit Tests.

## Requirements

- .NET Framework 4.7.2 or later.
- Visual Studio 2017 or later / JetBrains Rider 2018.3 or later.

## Installation

1. Clone the repository: `git clone https://gitlab.fi.muni.cz/xjankola/software-quality-yellow-team.git`.
2. Open the project in Visual Studio or JetBrains Rider.
3. Build the project.
4. Run the project.
