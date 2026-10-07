# 🧑‍💻 User Management API

> A complete, well-structured ASP.NET Core Web API built for demonstrating backend development best practices. It allows clients to manage user profiles efficiently using RESTful principles.

---

## ✨ Features

- **🔄 CRUD Operations**: Create, Read, Update, and Delete users.
- **🛡️ Validation**: Comprehensive data annotations ensure strong input validation.
- **🚫 Duplicate Email Checking**: Prevents registering multiple users with the same email address.
- **🔍 Search**: Search users by first or last name.
- **📄 Pagination**: Navigate through large sets of users efficiently.
- **⚙️ Middleware**: Includes custom Request Logging and Global Exception Handling.
- **📚 Swagger**: Fully integrated for API documentation and testing.
- **🔐 Authentication**: Includes a demonstration of JWT-based authorization.

---

## 🛠️ Technologies Used

- **C#** & **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core**
- **SQLite** (for simple, file-based storage)
- **Swagger/OpenAPI**

---

## 📁 Project Structure

Here is a quick overview of the key directories in the project:

- 📂 `Controllers/` - Contains API endpoints that handle HTTP requests.
- 📂 `Models/` - Represents the database entities.
- 📂 `DTOs/` - Data Transfer Objects for controlled data exposure and input validation.
- 📂 `Data/` - Contains the Entity Framework Core DbContext and database initialization.
- 📂 `Services/` - Contains the business logic, abstracting data operations from controllers.
- 📂 `Middleware/` - Contains custom middleware for logging and error handling.

---

## 🌐 API Endpoints

| Method | Endpoint | Description |
|:---:|---|---|
| `GET` | `/api/users` | Get all users (supports pagination `?page=1&pageSize=10`) |
| `GET` | `/api/users/{id}` | Get user by ID |
| `GET` | `/api/users/search?name=value` | Search users by name |
| `POST` | `/api/users` | Create user |
| `PUT` | `/api/users/{id}` | Update user |
| `DELETE` | `/api/users/{id}` | Delete user (Requires Auth) |

💡 **Pagination**: The `GET /api/users` endpoint accepts optional `page` and `pageSize` query parameters for paginated results.

---

## ✅ Validation Rules

The API enforces the following rules for incoming user data:
- **First and Last Name**: Required, maximum of 50 characters.
- **Email**: Required, must be a valid email format, and must be unique.
- **Phone Number**: Required, must match a standard phone number regex format.
- **Age**: Must be between 1 and 120.
- **City**: Required, cannot be empty.

---

## 🔌 Custom Middleware

- 📝 **Request Logging Middleware**: Intercepts every request and logs the HTTP method, path, status code, and processing time in milliseconds.
- ⚠️ **Global Exception Handling Middleware**: Catches unhandled exceptions gracefully, logging the error and returning a clean `500 Internal Server Error` JSON response without leaking stack traces to the client.
- 🔑 **Authentication/Authorization**: A basic JWT Bearer configuration is set up in `Program.cs`. The `DELETE` endpoint is protected by `[Authorize]` as a demonstration.

---

## 🤖 Microsoft Copilot Integration

### Debugging with Microsoft Copilot
During the development of this project, Microsoft Copilot was utilized to identify and resolve several key issues:
- **Middleware Registration Order**: Copilot assisted in correctly placing `UseExceptionHandler` and `UseAuthentication` in the pipeline to ensure errors during auth were caught.
- **Entity Framework Null References**: Copilot helped debug an issue where `AsNoTracking()` was improperly used with updates, correcting the flow to properly track entities during `UPDATE` operations.
- **Routing Ambiguities**: Helped resolve route conflicts between `GET {id}` and `GET search`.
- **Validation Issues**: Assisted in refining the Regular Expression used for phone number validation.

### How Microsoft Copilot Assisted
1. **API Code Generation**: Scaffolded basic controller structures and model definitions.
2. **CRUD Implementation**: Generated boilerplate EF Core repository methods.
3. **Debugging**: Identified missing `await` keywords and middleware pipeline ordering issues.
4. **Validation**: Provided standard Regex patterns for emails and phone numbers.
5. **Middleware**: Created the skeleton for the stopwatch-based request logger.
6. **Documentation**: Helped format the Markdown tables and organize this README.

---

## 🚀 How to Run

1. Open a terminal in the project directory.
2. Run the following commands:

```bash
dotnet restore
dotnet build
dotnet run
```

3. Open a browser and navigate to the Swagger URL (usually `http://localhost:5000/swagger` or `https://localhost:5001/swagger` based on your terminal output).

---

## 🧪 Testing the API

Here are some sample JSON bodies you can use to test the endpoints via Swagger or Postman:

**Example `POST` Request Body:**
```json
{
  "firstName": "Alice",
  "lastName": "Wonderland",
  "email": "alice@example.com",
  "phoneNumber": "1234567890",
  "age": 25,
  "city": "London"
}
```

**Example `PUT` Request Body:**
```json
{
  "firstName": "Alice",
  "lastName": "Liddell",
  "phoneNumber": "0987654321",
  "age": 26,
  "city": "Oxford"
}
```

---

## 📤 GitHub Submission Guide

Use these commands to push your project to GitHub:

```bash
git init
git add .
git commit -m "Initial commit of User Management API"
git branch -M main
git remote add origin <your-repository-url>
git push -u origin main
```

---

## 📋 Assignment Checklist (Rubric)

- [x] **5 points** – GitHub repository created *(Configured via `.gitignore` and Git instructions in README)*
- [x] **5 points** – GET, POST, PUT, DELETE CRUD endpoints implemented *(Implemented in `UsersController.cs` and `UserService.cs`)*
- [x] **5 points** – Microsoft Copilot used for debugging *(Documented in the README)*
- [x] **5 points** – User input validation implemented *(Data Annotations in Models and DTOs)*
- [x] **5 points** – Middleware implemented for logging/error handling/authentication *(Included `RequestLoggingMiddleware.cs` and `ExceptionHandlingMiddleware.cs`)*
