Repository for an fun project, which is agar.io type game. The main goal of this is to learn some technologies, especially backend.

### Finished project is going to use:

 - ASP.NET Core 2.2 (or 3.0) 
 - React + Redux (TypeScript) for simple SPA (mainly managing user & stats)
 - Simple game written in TS using HTML5 Canvas
 - JWT authentication/authorization
 - SignalR for real-time communication
 - Microservices architecture
 - Docker containers (docker-compose as simple orchestrator)
 - RabbitMQ for message queuing
 - Simple MSSQL database (managing users/stats)
 - API healtchecks
 - API docs (swagger)

### Road Map:
- [x] Create MSQL DB
- [x] Create JWT provider
- [x] Create users API (login, register)
- [x] Create React+Redux+TypeScript SPA template
- [ ] **[In progress]** Create simple game (more like PoC then actuall game)
- [ ] Create SignalR hub for managing multiplayer mode
- [ ] Authorize SignalR
- [ ] Create RabbitMQ messaging for notyfying Identity service about user stats
- [ ] Move into docker containers
