#!/bin/bash

export DATABASE__DATABASEFILE=$(pwd)/BabyNames.db

pushd src/BabyNames > /dev/null
dotnet run
popd > /dev/null
