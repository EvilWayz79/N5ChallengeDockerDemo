# N5Challenge

N5 company requests a Web API for registering user permissions, to carry out
this task it is necessary to comply with the following steps:
* Create tables to manage employees , permission and permission types.
* Your system must allow that you have employess with "N" count of
permissions type.
* Create a Web API using net core on Visual Studio and persist data on
SQL Server.
* Make use of EntityFramework.
* The Web API must have 3 services “Request Permission”, “Modify
Permission” and “Get Permissions”. Every service should persist a
permission registry in an elasticsearch index, the register inserted in
elasticsearch must contains the same structure of database table
“permission”.
* Create apache kafka in local environment and create new topic where
persist every operation a message with the next dto structure:
* Id: random Guid
* Name operation: “modify”, “request” or “get”.
* (desired)
* Making use of repository pattern and Unit of Work and CQRS
pattern(Desired). Bear in mind that is required to stick to a proper
service architecture so that creating different layers and dependency
injection is a must-have.
* Add information logs in every api endpoint and log the name of
operation using serilog as log library.
* Create Unit Testing and Integration Testing to call the three of the
services.
* Use good practices as much as possible.0
* Prepare the solution to be containerized in a docker image.
* Prepare the solution to be deployed in kubernetes. (desired)
* Upload exercise to some repository (github, gitlab,etc).
