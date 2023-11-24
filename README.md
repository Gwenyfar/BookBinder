# BookBinder
This is a bookstore API that allows for the catalog of books, authors and publishers.
It also allows a store manager to keep track of users and the books they have purchased.
The API is hosted [here](https://bookbinder.azurewebsites.net/)
> **Note**
> This API is still a work in progress, the functionalities may change in the near future.

## Endpoints
The endpoints to in use currently are described in this section

## 1. AuthorController 
This controller contains actions performed on the authors

### AddAuthor
You can add an author to the bookstore by inputing the details in the request body
- HTTP Method `POST`
- Route `/api/authors`
- Request Body
  ```
  {
    "firstName": "Freddie",
    "lastName": "Jones",
    "email": "freddie@yahoo.com"
  }
  ```
- Response
  - Returns an `Ok` response with unique id of new author if the request body is correct and author email doesn't exist in the database already.
  -  If request body is incorrect or author exists, it returns a `BadRequest` response with error messages.
  -  If there's a failure commiting to the database, it returns an `InternalServerError` response.
    
### FetchAuthors
This endpoint fetches all authors in the bookstore database
- HTTP Method `GET`
- Route `/api/authors`
- Response
  - Returns an `Ok` response with a list of all authors. Each author is exposed with this `Json` format;
   ```
  {
    "id": "a9852712-124b-4c3b-8ae2-d43b3d126b2b",
    "firstName": "Freddie",
    "lastName": "Jones",
    "email": "freddie@yahoo.com"
  }
  ```
  -  If there's a failure in fetching from the database, it returns an `InternalServerError` response.
    
### UpdateAuthor
You can update the information of an author belonging to the bookstore by inputing the details in the request body
- HTTP Method `PUT`
- Route `/api/authors`
- Request Body
  ```
  {
    "id": "a9852712-124b-4c3b-8ae2-d43b3d126b2b",
    "firstName": "Freddie",
    "lastName": "Jones",
    "email": "freddie@yahoo.com"
  }
  ```
- Response
  - Returns an `Ok` response  if the request body is correct and author exists in the database already.
  -  If request body is incorrect, it returns a `BadRequest` response with an error message.
  -  If author doesn't exist, it returns a `NotFound` response with an error message.
  -  If there's a failure commiting to the database, it returns an `InternalServerError` response.

### RemoveAuthor
This endpoint deletes an existing author from the database.
- HTTP Method `DELETE`
- Route `/api/authors/a9852712-124b-4c3b-8ae2-d43b3d126b2b`
- Response
  - Returns an `Ok` response if the author id is correct and author exists in the database already.
  -  If author id is incorrect, it returns a `BadRequest` response with error message.
  -  If the author doesn't exis it returns a `NotFound` response with error message.
  -  If there's a failure commiting to the database, it returns an `InternalServerError` response.
---------------------------------------------------------------

