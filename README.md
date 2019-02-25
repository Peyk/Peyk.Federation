# Peyk - Federation API

[![Heroku Badge]](https://peyk-federation.herokuapp.com)
[![Travis Badge]](https://travis-ci.org/Peyk/Peyk.Federation)
[![AppVeyor Badge]](https://ci.appveyor.com/project/poulad/peyk-federation)
[![Docker Hub Badge]](https://cloud.docker.com/repository/docker/peyk/federation)

A [Peyk] micro-service for the [Federation API](aka. Server to Server).

## Getting Started

Here are the quick instructions to run this micro-service.
This guide uses Docker to run all the components and Node.js to automate the process.

> Check the [CONTRIBUTING] guide to learn about setting up the development environment.

### Requirements

- Node.js
- Docker

### Instructions

```sh
# go to the automation scripts directory
cd scripts/

# resolve Node.js dependencies
npm install

# build the Docker images
npm run build

# spin up the Docker containers for a local deployment
# and seed the storage with some test data
npm run serve-dev

# get a list of the public rooms
curl -X GET "http://localhost:8008/_matrix/client/r0/publicRooms"
```

## Repository Contents

- `Peyk.Federation.sln`: .NET Solution
- `src/`
  - `Peyk.Matrix.Models`: Contains the DTOs and the Entities in Matrix Protocol
  - `Peyk.Data.Entities`: Peyk data entities persisted in the storage
  - `Peyk.Data.Abstractions`: Peyk entity repository interfaces (Repository Pattern)
  - `Peyk.Data.Mongo`: Peyk entity repository implementation for MongoDB
  - `Peyk.Federation.Ops.Query`: Federation API operations for querying(read-only) data
  - `Peyk.Federation.Web`: Web API implementation for the Federation HTTP protocol
- `test/`
  - `Framework`: Contains a set of common utilities for the xUnit framework shared between the test projects
  - `MongoTests`: Systems integration tests for the MongoDB DAL(`Peyk.Data.Mongo` project)
- [`scripts/`]: CI/CD automation scripts(Node.js) for this service

[Heroku Badge]: https://img.shields.io/badge/-demo-yellowgreen.svg?style=popout-square&logo=heroku&colorA=cccccc
[AppVeyor Badge]: https://img.shields.io/appveyor/ci/poulad/peyk-federation/master.svg?style=popout-square&logo=appveyor
[Travis Badge]: https://img.shields.io/travis/Peyk/Peyk.Federation/master.svg?style=popout-square&logo=travis
[Docker Hub Badge]: https://img.shields.io/docker/pulls/peyk/federation.svg?style=popout-square&logo=docker
[Peyk]: https://peyk.github.io/
[Federation API]: https://matrix.org/docs/spec/server_server/latest.html
[CONTRIBUTING]: ./CONTRIBUTING.md
[`scripts/`]: ./scripts/
