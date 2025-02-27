# Personal Portfolio Web Application

This project is a personal portfolio web application developed using **Clean Architecture**, **CQRS**, **FluentValidation**, **Repository Pattern**, **MSSQL**, and **AutoMapper**. The main goal of this project is to showcase personal skills, projects, and achievements in a professional manner, while adhering to best practices in software architecture and design.

## Technologies Used

- **.NET**: The primary framework used for developing the backend API and web MVC projects.
- **Clean Architecture**: Organized the project into distinct layers to promote separation of concerns and scalability.
- **CQRS (Command Query Responsibility Segregation)**: Applied to handle data reading and writing operations separately for better performance and maintainability.
- **MSSQL**: Used as the database management system for storing and retrieving application data.
- **FluentValidation**: Used for input validation across the application to ensure data integrity.
- **AutoMapper**: Simplifies the mapping between DTOs (Data Transfer Objects) and entities.
- **Repository Pattern**: Encapsulates the logic of data access and abstracts it from the rest of the application.
- **ASP.NET Core MVC**: The web framework used for building the frontend views and handling HTTP requests.
- **JWT Authentication**: Used to secure API endpoints by validating JSON Web Tokens (JWT) for user authentication and authorization. This ensures that only authenticated users can access protected resources.


## Project Structure

The project follows the **Clean Architecture** structure, which includes the following layers:

1. **Core**: Contains the application entities, DTOs, and interfaces.
2. **Application**: Holds the business logic, including CQRS-related handlers, commands, and queries.
3. **Infrastructure**: Implements data persistence and external services, such as email API, file API, and data API.
4. **WebMVC**: The main frontend part of the application, where users interact with the system.
5. **AdminMVC**: A separate frontend for administrators to manage portfolio content (projects, skills, etc.).
6. **API**: Handles API endpoints for communication with the frontend or other services, including:
   - **EmailAPI**: Manages email sending functionality.
   - **FileAPI**: Handles file management operations.
   - **DataAPI**: Provides data retrieval services.
   - **AuthAPI**: Manages authentication and authorization via JWT tokens.


## Features

- User Authentication & Authorization (JWT Token)
- Portfolio Display with Projects and Achievements
- Contact Form for user inquiries
- Admin Panel for managing portfolio content (projects, skills, etc.)
- Responsiveness and mobile-friendly design
