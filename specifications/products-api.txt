[Back to README](../README.md)

### Products

#### GET /products
- Description: Retrieve a list of all products
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "price desc, title asc")
- Response: 
  ```json
  {
    "data": [
      {
        "id": "integer",
        "title": "string",
        "price": "number",
        "description": "string",
        "category": "string",
        "image": "string",
        "rating": {
          "rate": "number",
          "count": "integer"
        }
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

#### POST /products
- Description: Add a new product
- Request Body:
  ```json
  {
    "title": "string",
    "price": "number",
    "description": "string",
    "category": "string",
    "image": "string",
    "rating": {
      "rate": "number",
      "count": "integer"
    }
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "title": "string",
    "price": "number",
    "description": "string",
    "category": "string",
    "image": "string",
    "rating": {
      "rate": "number",
      "count": "integer"
    }
  }
  ```

#### GET /products/{id}
- Description: Retrieve a specific product by ID
- Path Parameters:
  - `id`: Product ID
- Response: 
  ```json
  {
    "id": "integer",
    "title": "string",
    "price": "number",
    "description": "string",
    "category": "string",
    "image": "string",
    "rating": {
      "rate": "number",
      "count": "integer"
    }
  }
  ```

#### PUT /products/{id}
- Description: Update a specific product
- Path Parameters:
  - `id`: Product ID
- Request Body:
  ```json
  {
    "title": "string",
    "price": "number",
    "description": "string",
    "category": "string",
    "image": "string",
    "rating": {
      "rate": "number",
      "count": "integer"
    }
  }
  ```
- Response: 
  ```json
  {
    "id": "integer",
    "title": "string",
    "price": "number",
    "description": "string",
    "category": "string",
    "image": "string",
    "rating": {
      "rate": "number",
      "count": "integer"
    }
  }
  ```

#### DELETE /products/{id}
- Description: Delete a specific product
- Path Parameters:
  - `id`: Product ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

#### GET /products/categories
- Description: Retrieve all product categories
- Response: 
  ```json
  [
    "string"
  ]
  ```

#### GET /products/category/{category}
- Description: Retrieve products in a specific category
- Path Parameters:
  - `category`: Category name
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "price desc, title asc")
- Response: 
  ```json
  {
    "data": [
      {
        "id": "integer",
        "title": "string",
        "price": "number",
        "description": "string",
        "category": "string",
        "image": "string",
        "rating": {
          "rate": "number",
          "count": "integer"
        }
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./general-api.md">Previous: General API</a>
  <a href="./carts-api.md">Next: Carts API</a>
</div>