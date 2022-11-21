#!/bin/sh

openssl rand -base64 32 | awk '{print "{ \"Jwt\": { \"SecretKey\": \"" $1 "\" } }"}' > src/BabyNames/appsettings.secrets.json
