# MNIAAPI

## _API RESTFUL - user registration_

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

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

Ao clonar o repositório siga os passos abaixo: \***\* Você deve ter a versâo .net 5 instalada para testar a aplicação \*\***

First Tab:

```sh
dotnet ef database update
```

Second Tab:

```sh
dotnet run
```
