# machine.specifications.runner.dnx

A runner for specifications written for the machine.specifacations framework with the DNX project system.

## for what?

If you create an ASP.NET 5 Application or a ASP.NET 5 based Console Application you may also want to 
write your tests with machine.specifications. With this runner you can run the specifications from 
the console and within Visual Studio.

## how?


Download this project and reference it to the project.json of your test project, or if possible

> Install-Package Machine.Specifications.Runner.dnx

Add a test command to that project.json

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

* Running all tests
* running specific tests

## what won't work yet?


.NET Core is not supported, because of machine.specifications also wont run on .NET Core.


## how to work on the runner?

Clone the repository, open the solution within Visual Studio 2015 and work on it. It uses itself as a runner. 
So you dogfood it while developing.

## roadmap

 * running specific tests
 * support RC 2 and RTM of ASP.NET 5
 * support the upcomming dotnet-cli
 * mor output options TeamCity, Appveyour etc.


