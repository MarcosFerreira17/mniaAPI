## _API RESTFUL - user registration_

This is a RESTFUL API, containing the basic HTTP methods (GET, POST, PUT, DELETE) for a course/student system, after registering the student cannot change their data.

## About the API

- Automatic email sending when registering and logging into the system
- Checks whether the CPF is valid.
- The commum user can only access the system lists
- The admin is the only one who has access to change information
- User can register a profile picture
- The MySQL database is populated: dotnet ef database update
- API documented with Swagger
- HATEOAS was implemented 

## Development

When cloning you must follow the steps below: \***\* You must have the .net 5 version installed to test the application \*\***

First Tab:

```sh
dotnet ef database update
```

Second Tab:

```sh
dotnet run
```
