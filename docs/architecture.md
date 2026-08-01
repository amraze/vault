vault/
├── src/
│   ├── ApiGateway/
│   │   └── Vault.Gateway/                     # YARP reverse proxy + GraphQL entry point
│   │
│   ├── Services/
│   │   ├── Identity/                           # ── the pattern every service follows ──
│   │   │   ├── Vault.Identity.Domain/            # entities, domain events, business rules
│   │   │   ├── Vault.Identity.Application/       # CQRS handlers, use cases (MediatR)
│   │   │   ├── Vault.Identity.Infrastructure/    # data access, external adapters
│   │   │   └── Vault.Identity.Api/               # Minimal API host, the entry point
│   │   │
│   │   ├── Customer/  ...  
│   │   └── ...
│   │
│   ├── BuildingBlocks/                         # shared *technical* plumbing — NOT business logic
│   │   ├── Vault.SharedKernel/                   # base Entity, AggregateRoot, DomainEvent, Result
│   │   ├── Vault.Application.Common/             # MediatR pipeline behaviors, validation, CQRS base
│   │   ├── Vault.Infrastructure.Common/          # EF Core setup, outbox, logging conventions
│   │   ├── Vault.Messaging/                       # RabbitMQ/bus setup and publishing
│   │   ├── Vault.Caching/                         # Redis helpers, hold/lock primitives
│   │   ├── Vault.Auth/                            # JWT validation + shared auth policies
│   │   └── Vault.Web/                             # common Minimal API extensions, error handling, health
│   │
│   └── Contracts/                              # the ONLY things crossing service boundaries
│       ├── Vault.Contracts.Grpc/                 # shared .proto definitions
│       └── Vault.Contracts.IntegrationEvents/    # event message shapes published on the bus
│
├── tests/                                      # mirrors src/Services one-to-one
│   ├── Identity/
│   │   ├── Vault.Identity.UnitTests/
│   │   └── Vault.Identity.IntegrationTests/
│   ├── Customer/  ...  (same per service)
│   └── ...
│
├── deploy/
│   ├── docker/
│   │   └── docker-compose.yml                  # local dev: databases, Redis, RabbitMQ
│   ├── helm/
│   │   ├── vault/                             # umbrella chart for the whole platform
│   │   └── charts/                             # one subchart per service
│   └── k8s/                                    # (alternative to Helm) kustomize base + overlays
│       ├── base/
│       └── overlays/{dev,prod}/
│
├── pipelines/
│   ├── azure-pipelines.yml                     # CI/CD entry point
│   └── templates/                              # reusable build/test/deploy step templates
│
├── docs/
│   ├── architecture.md
│   ├── functional-spec.md
│   ├── db-diagram.md
│   └── contribution.md
│
├── .gitignore
├── .editorconfig                               # shared code style across all projects
├── Directory.Build.props                       # MSBuild settings inherited by every project
├── Directory.Packages.props                    # central package versions (one place to bump)
├── global.json                                 # pins the .NET SDK version
├── Vault.sln                                  # aggregate solution