# Truck Management System

## About the Project

The Truck Management System is a demo web application to save truck's information through a simple and friendly interface.

## Business Requirements

As user you can:

- See all trucks saved.
- Create or update a truck with following properties:
    - **Model**: Can be *FH* or *FM* only
    - **Manufacture year**: Should be current year
    - **Model year**: Should be current or next year
    - **Color**: Color with you want.
- Delete existing trucks.


## Technological Stack

|   **Program**         |   **Version**     |
|-----------------------|-------------------|
| .NET Core             |   3.1             |
| SQL Server - Local DB |   -               |
| Angular CLI           |   13.1.0          |
| NodeJS                |   16.3+           |

## Prerequisites

This application requires .NET Core 3.1, Docker 19+, NodeJS and Angular CLI installed on your OS.

## Running the application

### Running from source

#### Backend

Run the project in the Visual Studio 2019+ for debugging, execute the unit tests and understand the application logic applyed for this project.

The backend source code has the following layers:

|               **Layer**           |               **Main objective** |
|-----------------------------------|--------------------|
| **TruckManagement**               | Main project, here are located the API controllers and middlewares of the application. |
| **TruckManagement.Business**      | This project provides the application's logic, validations and communication between the Controllers, and the repository (database) layers. |
| **TruckManagement.Business.Interfaces**      | This project provides the definition of application's operations. |
| **TruckManagement.Repository**          | This project provides an abstraction of the database connection and CRUD (Create, Retrieve, Update and Delete) operations. |
| **TruckManagement.Repository.Interfaces**          | This project provides the definition of database operations. |
| **TruckManagement.Models**              | This class library contains the Entity objects. |
| **TruckManagement.ViewModels**          | This class library contains the POCO objects returned by business and API layers to clients. |
| **TruckManagement.Infra.Core**          | This class library contains mainly the helpers, constants, and common classes used by entire projects. |
| **TruckManagementTests**                  | Unit tests for *TruckManagement* project   |
| **TruckManagement.BusinessTests**         | Unit tests for *TruckManagement.Business* project   |
| **TruckManagement.RepositoryTests**       | Unit tests for *TruckManagement.Repository* project   |


Before start the application for the first time, check the database connection string located at ***appsettings.development.json*** file, and change it if necessary. Database and all pending migrations are applied automatically during application start up.

After start the debugging, a new browser window will be open with the Swagger documentation of the API. You can test all endpoints.

#### Frontend

The frontend application is located at **frontend** directory and can be opened with Visual Studio Code or any text editor of your preference.

This application is a single Angular project with consumes the backend API.

Install the dependencies with the command:

```bash
npm install
```

To run this project, execute the following command:

```bash
npm start
```
After the compile step, the frontend is available at [http://localhost:4200](http://localhost:4200).
