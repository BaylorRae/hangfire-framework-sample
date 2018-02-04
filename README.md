# Hangfire Framework - Sample

This is a framework for wrapping Hangfire with an opinionated syntax for
queueing jobs. It essentially draws inspiration from .NET MVC middleware
configuration with the following goals.

1. Encourage scalars when defining job options.
2. Use "convention over configuration" when queueing jobs.
3. Create abstractions to make testing easier.

![Sample Project](./screenshots/project.png)

## Running the Demo

```bash
$ git clone https://github.com/BaylorRae/hangfire-framework-sample.git
$ cd hangfire-framework-sample

# build the frontend and worker projects
$ docker-compose build

# start everything as a daemon
$ docker-compose up -d

# follow the logs for frontend and worker1
$ docker-compose logs -f frontend worker1
```

## Project Structure

This project is designed to keep all 3rd party dependencies in the top most
layers (e.g. `frontend`, `worker` and `common`). This is accomplished by
wrapping all third party apis with internal interfaces in our `core` project.

![Dependency Graph](./src/frontend/wwwroot/images/dependency-graph.svg)
