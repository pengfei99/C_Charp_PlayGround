# C# Project 

## General structure

A typical C# project consists of several components that work together:

- Solution File(.sln): Define a `container` that organizes one or more `related projects`. A solution can contain multiple projects that work together
- Project File(.csproj): Defines a single project's configuration. It contains project settings, references, and build instructions
- Program.cs: Contains the entry point (Main method) for console applications
- Properties/: Contains assembly information 
- bin/: Contains compiled binaries (created during build)
- obj/: Contains intermediate build files 
- References/: Contains references to external assemblies


### Project File(.csproj)

The project file is an `XML file` that defines:
 - Target framework
Package references
Project references
Build settings





Directory Structure
A basic C# project typically includes:

Program.cs: Contains the entry point (Main method) for console applications
Properties/: Contains assembly information
bin/: Contains compiled binaries (created during build)
obj/: Contains intermediate build files
References/: Contains references to external assemblies