#!/bin/bash

export DATABASE__DATABASEFILE=$(pwd)/BabyNames.db

pushd src/BabyNames.Database > /dev/null
dotnet run -- execute
popd > /dev/null
