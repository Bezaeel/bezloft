# Implementation
This solution contains projects - API, Application, Core, Infrastructure


API: I used the CQRS pattern with MediatR to separate the responsibilities of the endpoints into command (POST) and query (GET)
Application, Core and Infrasture projects make up reusable layers.

the Application layer is where the logic resides.


# Technologies used
- C# .NET 6
- MediatR
- MySQL
- Docker
- ORM - Entity Framework

# Assumptions


# How to run
- [ ] configure connection strings in `appsettings.json` in API project
- navigate to `bezloft.API` run `dotnet run`
- afterwards make request on the swagger page on the API

## using docker

- db configuration in `docker-compose.yml` and/or `dockerfile` should correspond with connection string below
- connection string should look like: `"Server=db;User ID=<username>;Password=<password>;Database=bezloft"`
- [ ] Ensure `docker` and `docker-compose` is installed
- run `docker-compose up`


# Challenges
- n/a

# Further Improvement
- global exception handler, currently on a happy path response type
- etc
