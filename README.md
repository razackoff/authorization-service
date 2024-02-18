# C# Authorization Microservice

This project is a simple authorization microservice that allows users to register, login, and logout. The microservice is written in C# and uses a database to store user accounts.

## Key features

* User registration
* User login
* User logout
* Authorization by roles

## Technologies

* C#
* .NET Core
* Entity Framework Core
* Docker

## Instructions for use

1. Clone the repository.
2. Create a database named "auth".
3. Run the command `docker-compose up`.
4. The microservice's web interface will be available at http://localhost:5000.

## Architecture

The microservice is divided into two main parts:

* The API layer provides endpoints for registering, logging in, and logging out users.
* The data access layer stores user accounts in a database.

The API layer is implemented using ASP.NET Core. The data access layer is implemented using Entity Framework Core.

## Security

The microservice implements the following security features:

* Password hashing
* Role-based authorization

## Limitations

* The microservice does not support multi-factor authentication.
* The microservice does not support password reset.

## Author

Firuz Razakov

## Additional information

* The repository contains documentation that describes how to use the microservice.
* The microservice can also be used as a library for other projects.

## Future plans

* Add support for multi-factor authentication.
* Add support for password reset.
* Add support for other authentication methods, such as OAuth 2.0.
