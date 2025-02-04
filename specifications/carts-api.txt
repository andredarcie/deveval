[Back to README](../README.md)

### Carts

#### GET /carts
- Description: Retrieve a list of all carts
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "id desc, userId asc")
- Response: 
  ```json
  {
    "data": [
      {
        "id": "integer",
        "userId": "integer",
        "date": "string (date)",
        "products": [
          {
            "productId": "integer",
            "quantity": "integer"
          }
        ]
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

#### POST /carts
- Description: Add a new cart
- Request Body:
  ```json
  {
    "userId": "integer",
    "date": "string (date)",
    "products": [
      {
        "productId": "integer",
        "quantity": "integer"
      }
    ]
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "userId": "integer",
    "date": "string (date)",
    "products": [
      {
        "productId": "integer",
        "quantity": "integer"
      }
    ]
  }
  ```

#### GET /carts/{id}
- Description: Retrieve a specific cart by ID
- Path Parameters:
  - `id`: Cart ID
- Response: 
  ```json
  {
    "id": "integer",
    "userId": "integer",
    "date": "string (date)",
    "products": [
      {
        "productId": "integer",
        "quantity": "integer"
      }
    ]
  }
  ```

#### PUT /carts/{id}
- Description: Update a specific cart
- Path Parameters:
  - `id`: Cart ID
- Request Body:
  ```json
  {
    "userId": "integer",
    "date": "string (date)",
    "products": [
      {
        "productId": "integer",
        "quantity": "integer"
      }
    ]
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "userId": "integer",
    "date": "string (date)",
    "products": [
      {
        "productId": "integer",
        "quantity": "integer"
      }
    ]
  }
  ```

#### DELETE /carts/{id}
- Description: Delete a specific cart
- Path Parameters:
  - `id`: Cart ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```


<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./products-api.md">Previous: Products API</a>
  <a href="./users-api.md">Next: Users API</a>
</div>