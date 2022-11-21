# baby-names-webapp

## Development

To build the client (requires Node.js):

```
cd src/BabyNames/Client
yarn install
yarn run build
```

To build the server (requires .NET SDK 6.0 or greater):

```
dotnet build
```

You will also need to create the database by running:

```
./run-database-migrations.sh
```

This will create a `BabyNames.db` file in the root directory.

To run the application:

```
dotnet run
```

The application should be available at http://localhost:8080 and https://localhost:8081.

To run the integration tests you can either run the `run-integration-tests.sh` script or set the `DATABASE__DATABASEFILE` environment variable manually and run them in the IDE of your choice. For Rider, environment variables can be set in File > Settings > Build, Execution, Deployment > Unit Testing > Test Runner.

### TODO

- [x] Create and configure client-side application
- [x] Create and configure server-side application
- [x] Implement API endpoints with dummy data
- [x] Update API to be backed by a database
- [x] Add Redux
- [x] Create skeleton application with planned interactions
- [ ] Style application
- [ ] Add animations
- [x] Add auth
- [x] Deployment

## Deployment

Includes Github Actions that will build the site and archive the result.

The following should be added to your Nginx server configuration to setup the reverse proxy:

```
location /baby-names {
        proxy_pass              http://127.0.0.1:5000;
        proxy_http_version      1.1;
        proxy_set_header        Upgrade $http_upgrade;
        proxy_set_header        Connection keep-alive;
        proxy_set_header        Host $host;
        proxy_cache_bypass      $http_upgrade;
        proxy_set_header        X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header        X-Forwarded-Proto $scheme;
}
```

You should also setup the systemd service using the `baby-names-webapp.service` file. See the comments in that file for instructions.

To deploy the web application, run the following from the server:

```
wget https://github.com/mattherman/baby-names-webapp/releases/latest/download/site.zip
unzip site.zip -d /var/www/baby-names
systemctl restart baby-names-webapp.service
```

To perform a database migration, run the following from the server:

```
wget https://github.com/mattherman/baby-names-webapp/releases/latest/download/database.zip
unzip site.zip -d ./database
./database/BabyNames.Database execute /var/www/baby-names/BabyNames.db
```

