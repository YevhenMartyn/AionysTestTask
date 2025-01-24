# AionysTestTask

Test task for the Aionys

## Prerequisites

- .NET 7.0 SDK or higher
- SQL Server
- Node.js

## Setup Instructions

### Backend

1.Navigate to the back-end folder: To get to the FilmManagementSystem folder:

cd FilmManagementSystem

2.Apply migrations to create the database: Navigate to the Infrastructure folder where migrations are stored and apply them to set up the database:

cd Infrastructure
dotnet ef database update

3.After migrations are applied, go back to the Web folder to start the API project:

cd ../Web
dotnet run

This will run the back-end API on http://localhost:7147

### Frontend

1.Navigate to the front-end project (FilmManagementSystemUI) directory:

cd FilmManagementSystemUI

2.Install the required npm packages:

npm install

3.Rename the .env.example to .env

4.Run the application:

npm run dev

This will start the development server and you can view the React application at http://localhost:5173

### Notes

If you have the backend or frontend running on different ports, you need to make adjustments
Backend: Web/Program.cs - appsettings.json
Frontend: .env
