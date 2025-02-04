[Back to README](../README.md)

### Users

#### GET /users
- Description: Retrieve a list of all users
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "username asc, email desc")
- Response: 
  ```json
  {
    "data": [
      {
        "id": "integer",
        "email": "string",
        "username": "string",
        "password": "string",
        "name": {
          "firstname": "string",
          "lastname": "string"
        },
        "address": {
          "city": "string",
          "street": "string",
          "number": "integer",
          "zipcode": "string",
          "geolocation": {
            "lat": "string",
            "long": "string"
          }
        },
        "phone": "string",
        "status": "string (enum: Active, Inactive, Suspended)",
        "role": "string (enum: Customer, Manager, Admin)"
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

#### POST /users
- Description: Add a new user
- Request Body:
  ```json
  {
    "email": "string",
    "username": "string",
    "password": "string",
    "name": {
      "firstname": "string",
      "lastname": "string"
    },
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "string",
        "long": "string"
      }
    },
    "phone": "string",
    "status": "string (enum: Active, Inactive, Suspended)",
    "role": "string (enum: Customer, Manager, Admin)"
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "email": "string",
    "username": "string",
    "password": "string",
    "name": {
      "firstname": "string",
      "lastname": "string"
    },
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "string",
        "long": "string"
      }
    },
    "phone": "string",
    "status": "string (enum: Active, Inactive, Suspended)",
    "role": "string (enum: Customer, Manager, Admin)"
  }
  ```

#### GET /users/{id}
- Description: Retrieve a specific user by ID
- Path Parameters:
  - `id`: User ID
- Response: 
  ```json
  {
    "id": "integer",
    "email": "string",
    "username": "string",
    "password": "string",
    "name": {
      "firstname": "string",
      "lastname": "string"
    },
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "string",
        "long": "string"
      }
    },
    "phone": "string",
    "status": "string (enum: Active, Inactive, Suspended)",
    "role": "string (enum: Customer, Manager, Admin)"
  }
  ```

#### PUT /users/{id}
- Description: Update a specific user
- Path Parameters:
  - `id`: User ID
- Request Body:
  ```json
  {
    "email": "string",
    "username": "string",
    "password": "string",
    "name": {
      "firstname": "string",
      "lastname": "string"
    },
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "string",
        "long": "string"
      }
    },
    "phone": "string",
    "status": "string (enum: Active, Inactive, Suspended)",
    "role": "string (enum: Customer, Manager, Admin)"
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "email": "string",
    "username": "string",
    "password": "string",
    "name": {
      "firstname": "string",
      "lastname": "string"
    },
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "string",
        "long": "string"
      }
    },
    "phone": "string",
    "status": "string (enum: Active, Inactive, Suspended)",
    "role": "string (enum: Customer, Manager, Admin)"
  }
  ```

#### DELETE /users/{id}
- Description: Delete a specific user
- Path Parameters:
  - `id`: User ID
- Response: 
  ```json
  {
    "id": "integer",
    "email": "string",
    "username": "string",
    "password": "string",
    "name": {
      "firstname": "string",
      "lastname": "string"
    },
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "string",
        "long": "string"
      }
    },
    "phone": "string",
    "status": "string (enum: Active, Inactive, Suspended)",
    "role": "string (enum: Customer, Manager, Admin)"
  }
  ```
<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./carts-api.md">Previous: Carts API</a>
  <a href="./auth-api.md">Next: Auth API</a>
</div>
