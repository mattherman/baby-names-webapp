#!/bin/bash

DatabaseFile=$(pwd)/BabyNames.db

pushd src/BabyNames.Database > /dev/null
dotnet run -- execute $DatabaseFile
popd > /dev/null
