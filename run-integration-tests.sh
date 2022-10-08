#!/bin/bash

export DATABASE__DATABASEFILE=$(pwd)/BabyNames.db

pushd src/BabyNames.Tests > /dev/null
dotnet test --filter Category="Integration"
popd > /dev/null
