Open cmd or bash at project root
Run command (project must be published)
```dotnet publish -c Release```

Then run command (to build docker image)
``` docker build -t blazor-ui .```

Now you may run the docker compose in the sample kogito project to start everything