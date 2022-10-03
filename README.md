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

To run the application:
```
dotnet run
```
The application should be available at http://localhost:8080 and https://localhost:8081.

## TODO

- [X] Create and configure client-side application
- [X] Create and configure server-side application
- [X] Implement API endpoints with dummy data
- [ ] Update API to be backed by a database
- [X] Add Redux
- [ ] Create skeleton application with planned interactions
- [ ] Style application
- [ ] Add animations
- [ ] Add auth
- [ ] Deployment
