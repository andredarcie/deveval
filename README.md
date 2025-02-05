# DevEval Project

![Build Status](https://github.com/andredarcie/deveval/actions/workflows/dotnet.yml/badge.svg)

DevEval is a robust application for system evaluation and management, designed using a clean, modular, and scalable architecture. This project organizes its logic into well-defined layers, making it easier to maintain, test, and extend.

---

## ▶️ How to Run

Run the database in any way you prefer, then configure appsettings.Development.json with the credentials. For example, using a Docker container:

docker run -d --name dev_eval_db --restart always -e POSTGRES_DB=DevEvalDb -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=yourpassword -p 5432:5432 -v postgres_data:/var/lib/postgresql/data postgres:15

Run the project normally with:

dotnet run

It will automatically open the Swagger homepage, displaying the API documentation. Call the endpoint api/auth/login. When running the project, migrations are executed, the database structure is created, and an admin user is automatically set up. The API already has default values for nickname and password. Just make a POST request and retrieve the JWT.

Create a new product, get its ID, and create a new shopping cart using the newly created product ID. After creating the cart, perform the checkout:

/api/carts/{id}/checkout

This will finalize the cart and turn it into a newly created purchase.

## 📂 Project Structure

The project follows a modular approach inspired by **Domain-Driven Design (DDD)** principles, divided into several layers:

- **src/DevEval.Application**  
  Contains the application layer, responsible for orchestrating domain logic and interacting with external interfaces. Includes:
  - **Commands**: Represent intent and encapsulate all data required to execute specific operations (e.g., `LoginUserCommand`, `CreateCartCommand`, `ConvertCartToSaleCommand`).
  - **Handlers**: Execute business logic by interacting with the domain layer (e.g., `LoginUserHandler`, `CancelSaleItemHandler`).
  - **DTOs (Data Transfer Objects)**: Used to transfer data between layers and external clients (e.g., `CartDto`, `ProductDto`, `SaleDto`, `UserDto`).
  - **Profiles**: AutoMapper profiles to map domain models to DTOs, maintaining separation of concerns.

- **src/DevEval.Domain**  
  Represents the core of the application, focusing on business rules and domain logic. Implements core DDD patterns:
  - **Entities**: Unique objects with an identity that persists over time (e.g., `Cart`, `Product`, `User`, `Sale`, `SaleItem`).
  - **Value Objects**: Immutable types representing concepts without a unique identity (e.g., `Address`, `Rating`).
  - **Repositories**: Abstract interfaces for data access, enabling the persistence and retrieval of domain entities while maintaining a separation from infrastructure concerns.
  - **Aggregates**: Clusters of domain objects (entities and value objects) that are treated as a single unit. For example, `Cart` may act as an aggregate root, encapsulating `CartProduct`.

- **src/DevEval.Common**  
  Contains shared utilities and abstractions to support the application and domain layers:
  - **Pagination**: Helpers for paginating large datasets (e.g., `PaginationHelper`, `PaginatedResult`).
  - **Sorting**: Logic for sorting collections dynamically (e.g., `SortingHelper`).
  - **Base Classes**: Abstract implementations for common patterns (e.g., `ValueObject`, `Repository`).

- **src/DevEval.WebApi**  
  Acts as the interface layer, exposing the domain and application logic via RESTful APIs. Adheres to **CQRS** principles by separating query and command responsibilities:
  - **Controllers**:
    - `AuthController`: Handles authentication and token generation.
    - `CartsController`: Manages cart-related operations (CRUD, checkout).
    - `ProductsController`: Manages products, including category-based filtering.
    - `SalesController`: Handles the sales lifecycle.
    - `UsersController`: Manages user operations such as registration and updates.
  - **Middleware**: Implements cross-cutting concerns, such as:
    - `ErrorHandlingMiddleware`: Centralized handling of exceptions with consistent responses.
  - **Configuration Files**:
    - `appsettings.json` and `appsettings.Development.json` for managing environment-specific settings.
  - **Deployment**:
    - `Dockerfile` and `docker-compose.yml` to facilitate containerized deployments.

- **test/DevEval.Test**  
  Implements automated tests to ensure application quality and robustness:
  - **Handlers**: Tests for application handlers to validate business logic (e.g., `LoginUserHandlerTests`, `CancelSaleItemHandlerTests`).
  - **Profiles**: Tests for AutoMapper mappings (e.g., `CartProfileTests`, `ProductProfileTests`).
  - **Entities**: Tests for domain entities to verify business rules (e.g., `CartTests`, `ProductTests`, `SaleTests`).
  - **Value Objects**: Tests for value objects to ensure immutability and validity (e.g., `AddressTests`).
  - **Project Configuration**: `DevEval.Test.csproj` consolidates all test files and dependencies.

---

### 💡 Domain-Driven Design (DDD) Principles Applied

1. **Layered Architecture**:  
   - Separation of concerns between application logic (`Application`), domain rules (`Domain`), and infrastructure (`WebApi`, `Common`).

2. **Entities and Value Objects**:  
   - Core domain objects (e.g., `Cart`, `Sale`, `User`) ensure domain integrity.
   - Value objects (e.g., `Address`) enforce immutability and equality comparison.

3. **Repositories**:  
   - Abstract interfaces isolate domain logic from persistence details.

4. **Aggregates**:  
   - Aggregate roots like `Cart` and `Sale` control access to related entities, ensuring consistency.

5. **CQRS**:  
   - Commands (`CreateCartCommand`, `ConvertCartToSaleCommand`) handle state changes.
   - Queries (`GetProductsQuery`, `GetCartsQuery`) retrieve data without side effects.

6. **Ubiquitous Language**:  
   - Aligns code structure and terminology (e.g., `Cart`, `Checkout`, `Sale`) with domain experts to improve understanding.

7. **Exception Handling**:  
   - Middleware centralizes exception management, adhering to **fail-fast** and **single responsibility** principles.

---

### 🛠️ Usage of Technologies

### Backend
- **.NET 8.0**: Core framework for building RESTful APIs and executing business logic across all layers.
- **C#**: Primary programming language for implementing application logic.

### Frameworks
- **Mediator (MediatR)**:  
  - Centralizes request handling by decoupling controllers and domain logic.  
  - Example: `LoginUserCommand` is sent by `AuthController` and processed by `LoginUserHandler`.

- **Automapper**:  
  - Maps `Cart` domain entities to `CartDto` in handlers and controllers to ensure API responses are formatted for external clients.

- **Rebus**:  
  A lean service bus implementation for .NET. Used to implement event-driven design for publishing events like `SaleCreated`, `SaleModified`, `SaleCancelled`, and `ItemCancelled`.  

### Testing
- **xUnit**:  
  - Tests application handlers to validate the flow of business logic, such as checking the successful creation of a cart in `CreateCartCommandTests`.
  - Ensures domain rules, such as validating `Sale` aggregates, behave correctly.

- **NSubstitute**:  
  - Mocks repositories and services to isolate specific functionalities, like testing the `LoginUserHandler` without relying on a real database.

- **Faker**:  
  - Generates fake data for entities like users and products during unit tests, ensuring diverse test cases (e.g., `Faker` creates test users for `UserTests`).

### Database
- **PostgreSQL**:  
  - Stores relational data, such as users and product inventories, accessed using EF Core (e.g., `UserRepository` retrieves user data).
  - Pagination and filtering are applied directly through queries in `GetUsersQuery`.

### Error Handling
- **ErrorHandlingMiddleware**:  
  - Catches exceptions in the API pipeline, such as `UnauthorizedAccessException` in `LoginUserCommand`, and returns appropriate HTTP status codes (e.g., `401 Unauthorized`).

---