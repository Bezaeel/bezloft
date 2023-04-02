# Task
Event mgt 
- users can create events
- invite participant
- edit event details
- get all events created by users

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

# How to run
- [ ] configure connection strings in `appsettings.json` in API project
- navigate to `bezloft.API` run `dotnet run`

- browse `http://localhost:3000/swagger/index.html` to access the swagger page on the API
- user table has been seeded, you can use the available user ids for testing:
	```
	297e3e8e-3164-4343-8b64-44d1c0ce127d
	2a5a05ad-82f9-48a3-869c-2818b1ac1d33
	c3b0ce62-75a4-483f-b965-f625059803f0
	```

## using docker

- db configuration in `docker-compose.yml` and/or `dockerfile` should correspond with connection string below
- connection string should look like: `"Server=db;User ID=<username>;Password=<password>;Database=bezloft"`
- **the server value is 'db' in docker, because services connect by name on the same network within the same container**
- [ ] Ensure `docker` and `docker-compose` is installed
- run `docker-compose up`

# Testing
To run the unit tests
  - open terminal at root of solution, then run `dotnet test`

# Challenges
- n/a

# Further Improvement
- more test coverage
- global exception handler, currently on a happy path response type
- etc
