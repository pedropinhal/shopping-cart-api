# Shopping Cart Api

Shopping Cart Api is a sample application built using ASP.NET Core and Entity Framework Core. 

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Within the `Api` directory, launch the api by running:
     ```
	 dotnet run
	 ```  
  5. Launch [https://localhost:5001/api](https://localhost:5001/api) in your browser to view the API

## Technologies
* .NET Core 2.2
* ASP.NET Core 2.2
* Entity Framework Core 2.2

## Assumptions

Authentication - Authorization
Throughout this example we consider authentication - authorization functionality to exist and to be provided by a separate microservice. Therefore, a JWT token (or some other token) is supposedly provided by this microservice and included in the Authorization header in all HTTP requests.

