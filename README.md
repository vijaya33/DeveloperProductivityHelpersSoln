
# DeveloperProductivityHelpersSoln 

This solution contains two different NuGet packages, where one of the package called DotNet.Productivity helps and improves application development by providing validation checks on Strings, dates format etc.
and the second NuGet package provides Testing on APIs, and other methods, it provides ready to use and customize code coverage , Two NuGet packages are developed in this solution: 

# Testing utilities for .NET:

- **TestData"": deterministic random + fluent builders
- **ApiTestHarness" ": integration testing helpers for ASP.NET Core APIs 

To download and install this Testing package:
 - **Package Manager Console - dotnet add package DotNet.Testing.Extensions.Common

# DotNet.Productivity

A small, production-focused set of .NET extensions:
- **ConfigGuard**: strongly-typed configuration binding + validation + fail-fast startup checks
- **GuardExtensions**: lightweight fluent guard clauses for safer APIs

To download and install this Developer productivity package: 
 - ** Package Manager Console - dotnet add package DotNet.Developer.Extensions.Common 

 # Developer reference note to generate test API security keys:
- ** API keys were generated on this website (generates free API Keys):
- ** https://www.strongdm.com/tools/api-key-generator  

# Important:
***** Microsoft.Extensions.Configuration.Binder namespace nuget package did not have a definition for .Bind and hence throws an error on DotNet.Productivity project and fails the build on this project. DotNet.Testing project builds successfully now. Needs more work on DotNet.Productivity project... *****  
 
# .NET development stack: 
- **Visual Studio 2026 Enterprise edition
- **Target Framework:.NET CORE 9.0 (changed from Target Framework 10.0.0 to 9.0 to use compatibile namespaces) 
- **SDK : .NET 9.0.3 SDK. (changed from SDK 10.0.0 to 9.0.3 to use compatibile namespaces)

# Project Structure:

-|__ Project structure and files

  - |__  DeveloperProductivityHelpersSoln/

       - |__ .github/
       
           - ├── workflows/
          
              - ├── publish.yaml
          
       - |__  src/
          - ├── DeveloperProductivityHelpersSoln.sln
          - ├── README.md
          - |── LICENSE
          - |__ DotNet.Productivity

                     - |__ Folders
                    
                     - |__ {Code Files}
          
          - |__ DotNet.Testing
          
                     - |__ Folders
              
                     - |__ {Code Files} 


