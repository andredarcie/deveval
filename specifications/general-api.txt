[Back to README](../README.md)

## General API Definitions

### Pagination

Pagination is supported for list endpoints using the following query parameters:

- `_page`: Page number (default: 1)
- `_size`: Number of items per page (default: 10)

Example:
```
GET /products?_page=2&_size=20
```

### Ordering

When requesting a collection of a resource, you can also specify the order of the elements in the collection using the query parameter `_order`. Simply indicate the desired order: ascending (`asc`) or descending (`desc`). If not specified, the default order will be ascending.

**Note**

In the GET request, you must use the field names in the same format as the JSON response.

For example, consider the following Product resource:

```json
{
  "id": 1,
  "title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
  "price": 109.95,
  "description": "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday",
  "category": "men's clothing",
  "image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
  "rating": {
    "rate": 3.9,
    "count": 120
  }
}
```

In this case, to retrieve a list of products ordered by price in descending order and then by title in ascending order, the request would look like this:

```
GET /products?_order="price desc, title asc"
```

or 

```
GET /products?_order="price desc, title"
```

### Filtering

Filters can be applied to list endpoints using the following query parameters:

- `field=value`: Filter by specific field value.

Example:

```
GET /products?category=men's clothing&price=109.95
```

**String Fields**

To filter partial matches for string fields, use an asterisk (`*`) before or after the value.

Example:

```
GET /products?title=Fjallraven*
GET /products?category=*clothing
```

**Numeric and Date Fields**

To filter numeric or date fields by range, use `_min` and `_max` prefixes before the field name.

Example:

```
GET /products?_minPrice=50
GET /products?_minPrice=50&_maxPrice=200
GET /carts?_minDate=2023-01-01
```

Logical Operators
When combining filters, use `&` (AND) between them.

Example:

```
GET /products?category=men's clothing&_minPrice=50
GET /products?title=Fjallraven*&category=men's clothing&_minPrice=100
```

*Note*
Even when filtering with "or" for different values in the same field, use `&` in the query.

## Error Handling

The API uses conventional HTTP response codes to indicate the success or failure of an API request. In general:

- 2xx range indicate success
- 4xx range indicate an error that failed given the information provided (e.g., a required parameter was omitted, etc.)
- 5xx range indicate an error with our servers

### Error Response Format

```json
{
  "type": "string",
  "error": "string",
  "detail": "string"
}
```

- `type`: A machine-readable error type identifier
- `error`: A short, human-readable summary of the problem
- `detail`: A human-readable explanation specific to this occurrence of the problem

Example error responses:

1. Resource Not Found
```json
{
  "type": "ResourceNotFound",
  "error": "Product not found",
  "detail": "The product with ID 12345 does not exist in our database"
}
```

2. Authentication Error
```json
{
  "type": "AuthenticationError",
  "error": "Invalid authentication token",
  "detail": "The provided authentication token has expired or is invalid"
}
```

3. Validation Error
```json
{
  "type": "ValidationError",
  "error": "Invalid input data",
  "detail": "The 'price' field must be a positive number"
}
```

For detailed error information, refer to the specific endpoint documentation.

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./frameworks.md">Previous: Frameworks</a>
  <a href="./products-api.md">Next: Products API</a>
</div>