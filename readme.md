# Arduino MVC Web Project Utilities

Use the publish profiles in each of the .NET projects to build the utilities. .NET runtime version 8 or greater will need to be installed on the target computer where the utilities will be run.

## Install Location

Once built the utilities can be copied to and executed from almost any location. It is recommended to copy the utilities to the user's home directory, and execute from there:

```C:\Users\<user>\Arduino.MVC\Arduino.MVC.CodeGen.exe```

The above is an illustration of where the executables may be located. Substitute ```<user>``` for your Windows username. Linux and MacOS users will need to adapt as required.

## Usage Examples

To create a new Arduino project from a predefined template, the usage is as follows from the command or shell prompt:
```
> dotnet Arduino.MVC.NewProj.dll Web_Project Home About
```
The above will create an Arduino project with views and models for Home and About pages.
```
> dotnet Arduino.MVC.NewProj.dll Web_Project2 Home About Login DataApi
```
The above will create an Ardiuno project as before but with user login authentication and web API views and models.

To create view class files from ASP markup files, the usage is as follows from the command or shell prompt. Be sure to set the current directory to where the utilities are installed.
```
> dotnet Arduino.MVC.CodeGen.dll sourcedir destdir
```
The above will create view class files in the destination directory using the ASP markup files in the source directory.

## Integrating with Visual Studio Code (VS Code)

C++ view class generation from ASP markup files can be automated as part of the build process when using the Visual Studio Code editor. Install the [Arduino extension](https://marketplace.visualstudio.com/items?itemName=vscode-arduino.vscode-arduino-community) (from within VS Code) and configure to use with the Arduino CLI on the extension's settings screen. It will be necessary to download and install PowerShell to be able to invoke the prebuild script that auto generates the view classes. The first successful build of an Arduino project will auto-create the file c_cpp_properties.json, which should resolve intellisense errors displayed in the VS Code editor.

Linux/MacOS users will need to make edits to the file .vscode/BuildViews.ps1 under the Arduino project folder. Open the file to read the comments in it for making such edits.
