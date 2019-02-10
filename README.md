Repository for an fun project, which is agar.io type game. The main goal of this is to learn some technologies, especially backend.

### Finished project is going to use:

 - ASP.NET Core 2.2 (or 3.0) 
 - React + Redux (TypeScript) for simple SPA (mainly managing user & stats)
 - Simple real-time game written in TS using HTML5 Canvas
 - JWT authentication/authorization
 - MSSQL database (for statistics and identity)
 - Dapper as ORM for Statistics service
 - ASP.NET Core Identity as production-proof user manager
 - AutoMapper for mapping eneitites -> DTOs for Statistics API
 - SignalR for real-time communication w/ MessagePack
 - RabbitMQ for message queuing
 - Microservices architecture
 - Docker containers (docker-compose as simple orchestrator)
 - API healtchecks
 - API docs (swashbuckle/swagger)

### Road Map:
- [x] Create MSSQL DB
- [x] Create JWT provider
- [x] Create users API (login, register) using MSSQL + Dapper - consider moving it to Identity + Entity Framework [Obsolete]
- [x] Create React+Redux+TypeScript SPA template
- [ ] **[In progress]** Create SPA for basic operations like login/register/play game/statistisc
- [ ] **[In progress]** Create simple game (more like PoC then actuall game)
- [x] Create SignalR hub for managing multiplayer mode
- [x] Authorize SignalR
- [ ] **[In progress]** Crete Statistics service with persistence layer w/ Dapper and API w/ AutoMapper
- [x] Create RabbitMQ messaging for notyfying Identity service about user stats - bug with NACK messages
- [ ] Move API identity to use .NET Core Identity w/ Entity Framework
- [ ] Apply event Sourcing pattern for data consistency
- [ ] Move into docker containers
