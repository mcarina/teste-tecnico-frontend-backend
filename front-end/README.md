# Front-end with Angular

To install and run the project, make sure you have **Docker** installed on your machine.

```bash
docker compose up -d
```

To access the project frontend, open your browser and go to:

```bash
localhost:8080/
```

## Configuration
Inside the folder ```src/app/environments```, you will find the backend API path configuration.
If you are not using ```localhost```, update the apiUrl with the ```IP address``` and ```port``` of the server you are using.
```
export const environment = {
    production: false,
    apiUrl: 'http://localhost:5000'
  };
  
```
