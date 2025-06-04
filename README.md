explanation of the project and architecture.
Implemented library management system using .Net Core 8 (Asp.Net MVC) in a 3-Layer Architecture :
Data access layer , Business logic layer and Presentation layer.

Data access layer contains : Models which store in Sql database (EFCore) , Models Configurations , DbContext , unit of work and repositories that implemented against the interfaces. 
implemented Generic repository which book and author repos implement it.
implemented Book Library Repository using Redis (in-memory database). 

Business logic layer contains : DTOs (used by the Services) , all services that inject the unit of work (also implemented against the interfaces) and mapping profile (for Auto Mapper)

Presentation layer contains : Controllers and Views

System Sequence is Controllers => Services => unit of work => repositories => Databases

 
