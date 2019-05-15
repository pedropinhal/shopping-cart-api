# Shopping Cart Api

Shopping Cart Api is a sample application built using ASP.NET Core and Entity Framework Core. 

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Within the `Api.Tests` directory, launch the tests by running:
     ```
	 dotnet test
	 ```  
  4. Within the `Api` directory, launch the api by running:
     ```
	 dotnet run
	 ```
  5. When the api is running, within the `Api.Client` directory, launch the api client console app by running:
     ```
	 dotnet run
	 ```  
  6. Launch [https://localhost:5001/api](https://localhost:5001/api) in your browser to view the API

## Technologies
* .NET Core 2.2
* ASP.NET Core 2.2
* Entity Framework Core 2.2

## Assumptions

Authentication - Authorization
Throughout this example we consider authentication - authorization functionality to exist and to be provided by a separate microservice. Therefore, a JWT token (or some other token) is supposedly provided by this microservice and included in the Authorization header in all HTTP requests.

Validation
Due to time constraints, there is no validation behaviour. It is fairly simple to introduce a validation behaviour into the mediator pipeline to validate requests.

## HTTP Methods

### Create Cart
A `POST` HTTP request is sent to create a new shopping cart. The `Location` header is used to link to the newly created resource (the cart) in order for the client to be able to access it without querying anew.

### Request
```
POST /api/cart
```

### Response
```
201 Created
Content-Type: application/json; charset=utf-8
Location: api/cart/{id}
{
  "id": 1,
  "products": []
}
```

### Get Cart
A `GET` HTTP request is sent to retrieve shopping cart contents.

### Request
```
GET /api/cart/{id}
```

### Response
```
200 OK
Content-Type: application/json; charset=utf-8
{
  "id": 1,
  "products": [
    {
      "productId": 1,
      "quantity": 1
    }
  ]
}
```

### Add Product
A `POST` HTTP request with a `product` json body is sent to `cart` to add products to a shopping cart.

### Request
```
POST /api/cart/{id}
Content-Type: application/json
{
    "productId": 1,
    "quantity": 1
}
```

### Response
```
201 Created
Content-Type: application/json; charset=utf-8
Location: api/cart/{id}
{
  "id": 1,
  "products": [
    {
      "productId": 1,
      "quantity": 1
    }
  ]
}
```

### Remove Product
A `DELETE` HTTP request with a is sent to `cart` to remove products from a shopping cart.

### Request
```
DELETE /api/cart/{id}/products/{productId}/{quantity}
```

### Response
```
200 OK
Content-Type: application/json; charset=utf-8
```

### Clear Cart
A `POST` HTTP request with a sent clear a shopping cart.

### Request
```
POST /api/cart/{id}/clear
```

### Response
```
200 OK
Content-Type: application/json; charset=utf-8
```

## Api Client

Within the `Api.Client` directory, there is a console app to demonstrate the use of the `CartClient` library. Simply update the `host` parameter to api url.

### Note

If you are running the client app in a development environment (ie localhost), run `dotnet dev-certs https --trust` to create a self-signed local certificate in order to use the SSL endpoint.