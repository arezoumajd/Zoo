# Zoo
Zoo project for MFEX

## Technology Stack
This project is developed using the following technology stack:

- Framework: .NET 7.0
- Application Type: Console Application

## How to run the project
Using cmd or terminal, run `dotnet run` in ZooCore directory, or open the solution and use IDE run feature.

This considers the data files (animals.csv, prices.txt and zoo.xml) to be placed inside `ZooCore/Files` directory.

You can change this default path by passing the relative path as the first cli argument.

`dotnet run ./FilesChanged`

## How to build the project
run `dotnet build` command in root directory.

## How to run the built project
- Go to `ZooCore/bin/Debug/net7.0`
- Place the input files inside `Files` directory if you want to use the default path.
    - The final files will be `ZooCore/bin/Debug/net7.0/Files/animals.csv`, `ZooCore/bin/Debug/net7.0/Files/prices.txt` and `ZooCore/bin/Debug/net7.0/Files/zoo.xml`.
- run the exe file in cmd or terminal using `./ZooCore.exe`, or double click on the exe file on the windows.
    - You can pass the first argument to change the input files path. That should be a relative path.

## How to build the project in Release mode
- run `dotnet build --configuration Release` 
- use `ZooCore/bin/Release/net7.0` to place file.