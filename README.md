# Galaxy.Teams

## Description
In this microservice will be responssable for the team actions and interactions. The architecture type is clean architecture so the solution is devided in 3 projects: Presentation, Infrastructure and Core. More about clean architecture you can find here https://medium.com/vinarah/clean-architecture-example-c-5990bd4ac8. 

## Used Technologies
- .Net Core Console App 3.0
- Pomelo EF
- MySql Server
- NLog
- gRPC
- GraphQL

## Entity Framework Migrations
Run the following command from Galaxy.Teams.Presentation folder:
dotnet ef migrations add {{name}} -c TeamDbContext

## Needed in order to run locally
- you need to trust https certificates from .net. Please run this: **dotnet dev-certs https --trust** if you haven't done this before
- install .net core 3.0 https://dotnet.microsoft.com/download/dotnet-core/3.0
- install mysql (https://www.mysql.com/downloads/) or run it from docker. For ease of use you should also get workbanch, or some IDE to visual connect to MySQL server.

## On deploy 
Please make sure you overwrite the following settings:
- ConnectionStrings__TeamDb