# Dojo BE - Backend C# ASP.NET Core Web API

This project is a backend solution for a Courses API, built with Clean Code and Clean Architecture principles in C# using ASP.NET Core. It allows teachers to create courses and students to view and learn from them.

## Table of Contents

- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Technologies](#technologies)
- [Features](#features)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

To get started with this project, you'll need to have the .NET SDK installed on your machine. You can download it from the [official .NET website](https://dotnet.microsoft.com/download).

### Prerequisites

- .NET 7.0 SDK or later
- SQL Server or any other supported database

### Installation

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio or any other IDE that supports .NET Core.
3. Run the solution.

## Project Structure

This project is structured according to the principles of Clean Architecture, which includes:

- **Domain Layer**: Contains all the business logic and domain entities.
- **Application Layer**: Contains interfaces and application logic.
- **Infrastructure Layer**: Contains all the infrastructure-related code, such as data access and external services.
- **Web API Layer**: Contains the ASP.NET Core Web API project, which acts as the entry point for the application.

## Technologies

- **ASP.NET Core 6.0**: For building the Web API.
- **Entity Framework Core**: For data access and database operations.
- **AutoMapper**: For mapping between domain entities and DTOs.
- **MediatR**: For implementing the Mediator pattern to handle requests and responses.
- **FluentValidation**: For validating requests.
- **Swagger**: For API documentation.

## Features

- **Teachers**: Can create, update, delete, and view courses.
- **Students**: Can view and learn from courses.
- **Courses**: Include course details, lessons, and student enrollments.

## Contributing

Contributions are welcome. Please read the [contributing guidelines](CONTRIBUTING.md) before getting started.

## Code Style

We have a code style and conventions defined in [.editorconfig](.editorconfig), based on defaults from [Microsoft](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options).

To comply with the code style defined in [.editorconfig](.editorconfig), use `dotnet format` locally before pushing the code.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
