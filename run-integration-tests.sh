#!/bin/bash

export DATABASE__DATABASEFILE=$(pwd)/BabyNames.db
dotnet test --filter Category="Integration"
