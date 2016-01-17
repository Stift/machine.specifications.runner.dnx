# machine.specifications.runner.dnx

A runner for specifications written for the machine.specifacations framework and the DNX project system.

## for what?

If you create an ASP.NET 5 Application or a DNX based Console Application you may want to write your tests
with machine.specifications. With this runner you can run the specifications and from the console.

## how?

Download this project and reference it your project.json of the test project, or if possible

> Install-Package Machine.Specifications.Runner.dnx
(not yet, this is an early alpha)

Add the command test command to that project.json

```
  "commands": {
    "test": "machine.specifications.runner.dnx"
  },
```

with that you get the console runner and the Visual Studio 2015 integration.

## console

> dnx -p pathToProject test

## Visual Studio 2015

> Menu -> Test -> Windows -> Test Explorer

## what works?

Running all tests

## what won't work yet?

running specific tests

.NET Core is not supported, this is because of the machine.specifications also wont run on .NET Core.

## roadmap

 * running specific tests
 * support RC 2 and RTM of ASP.NET 5
 * support the upcomming dotnet system
 * mor output options TeamCity, Appveyour etc.


