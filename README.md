
# Desafio Dev

The DesafioDev project was developed for the Bycoders technical test.



## Technologies

**Back-end:** .NET 6

**Database**: SQL Server

## Necessary tools

**Back-end:** Visual Studio, Docker

**Integration tests:** Postman


## Functionalities

- Upload de arquivo
- List with balance totalizer


## API documentation

#### Upload do arquivo

```http
  POST /api/file/upload
```
| Content-Type | multipart/form-data |
| :----------- | :------------------


| Parameter   | Type       | Description                           |
| :---------- | :--------- | :---------------------------------- |
| `file` | `IFormFile` | **Mandatory**. File to be uploaded. |

#### Establishment list with totalizer

```http
  GET /api/establishment
```


## Debug

- To run the app locally, create a database locally and enter the connection string in "appsettings.Development.json".

Note: When running the project, the migration will run automatically, creating the tables in your database.


## Deploy for Docker

- Clone the project to your machine in your preferred location.

- With Docker installed on your machine, navigate to the root of your project using the terminal.

- In the root of the project run the following command "docker-compose up -d".

- When the process completes, access the "http://localhost:8001/health" route to check if the service has started.

Note: through docker compose, the image will be downloaded and the container configured, both .net and SQL Server, will create the database and when running the application it will automatically run the migration, with that our entire application will be ready For use.