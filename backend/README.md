# Backend with C#

To install and run the project, make sure you have **Docker** installed on your machine.

Run this command in the terminal:
```bash
docker compose up -d
```

## Configuration
- 1.1.
Make sure to configure your .env file according to the ```.env.example``` for the database credentials, and to enable CORS access for the frontend.

- 1.2.
In the ```appsettings.json``` file (line 8), you will find the configuration used to connect the project to the database.
It follows the same values as defined in the ```.env.example```
If you haven't made any changes, the project should run normally.
However, if you modify the configuration or are no longer using localhost, make sure to update this file accordingly to avoid connection issues.

```
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=testetecnico;Username=postgres;Password=postgres"
  },
```

## To access the project
To access the project, open your browser and go to:

```bash
localhost:5000/swagger/index.html
```

The /login and POST /user routes are public (do not require authentication).

## 
