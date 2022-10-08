# baby-names-webapp

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

## TODO

- [X] Create and configure client-side application
- [X] Create and configure server-side application
- [X] Implement API endpoints with dummy data
- [X] Update API to be backed by a database
- [X] Add Redux
- [X] Create skeleton application with planned interactions
- [ ] Style application
- [ ] Add animations
- [ ] Add auth
- [ ] Deployment
