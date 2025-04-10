# Backend with C#

If you want to run only the frontend project, make sure you are inside the backend project directory: `teste-tecnico-frontend-backend/backend`.
use the command below.

To install and run the project, make sure you have **Docker** installed on your machine.

```bash
docker compose up -d
```

## Configuration
Make sure to configure your .env file according to the .env.example for the database credentials.

## To access the project
To access the project, open your browser and go to:

```bash
localhost:5000/swagger/index.html
```
The /login and POST /user routes are public (do not require authentication).
