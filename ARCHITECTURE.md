# CVGenerator Architecture Documentation

## Clean Architecture Overview

The CVGenerator application follows **Clean Architecture** principles, ensuring maintainability, testability, and scalability.

## Layer Dependencies

```
┌─────────────────────────────────────────────────┐
│                 Presentation                    │
│            CVGenerator.API                      │
│  ┌─────────────────────────────────────────┐   │
│  │         Controllers                     │   │
│  │  - CVController                         │   │
│  │  - Swagger/OpenAPI                      │   │
│  └─────────────────────────────────────────┘   │
└────────────────────┬────────────────────────────┘
                     │ depends on
                     ▼
┌─────────────────────────────────────────────────┐
│              Application                        │
│         CVGenerator.Application                 │
│  ┌─────────────────────────────────────────┐   │
│  │  DTOs                                   │   │
│  │  - CreateCVRequest                      │   │
│  │  - GenerateCVRequest/Response           │   │
│  ├─────────────────────────────────────────┤   │
│  │  Validators (FluentValidation)          │   │
│  │  - CreateCVRequestValidator             │   │
│  │  - PersonalInfoDtoValidator             │   │
│  ├─────────────────────────────────────────┤   │
│  │  Mapping Profiles (AutoMapper)          │   │
│  │  - CVMappingProfile                     │   │
│  ├─────────────────────────────────────────┤   │
│  │  Services                               │   │
│  │  - ICVService / CVService               │   │
│  └─────────────────────────────────────────┘   │
└────────────────────┬────────────────────────────┘
                     │ depends on
                     ▼
┌─────────────────────────────────────────────────┐
│                  Domain                         │
│           CVGenerator.Domain                    │
│  ┌─────────────────────────────────────────┐   │
│  │  Entities (Core Business Models)        │   │
│  │  - CV                                   │   │
│  │  - PersonalInfo                         │   │
│  │  - Education                            │   │
│  │  - WorkExperience                       │   │
│  │  - Skill                                │   │
│  │  - Certification                        │   │
│  ├─────────────────────────────────────────┤   │
│  │  Interfaces                             │   │
│  │  - ICVGeneratorService                  │   │
│  │  - CVTemplate (enum)                    │   │
│  └─────────────────────────────────────────┘   │
└─────────────────────────────────────────────────┘
                     ▲
                     │ implemented by
                     │
┌─────────────────────────────────────────────────┐
│             Infrastructure                      │
│        CVGenerator.Infrastructure               │
│  ┌─────────────────────────────────────────┐   │
│  │  PDF Generation                         │   │
│  │  - QuestPdfGeneratorService             │   │
│  │    (implements ICVGeneratorService)     │   │
│  ├─────────────────────────────────────────┤   │
│  │  Templates (QuestPDF)                   │   │
│  │  - ModernTemplate                       │   │
│  │  - ClassicTemplate                      │   │
│  │  - CreativeTemplate                     │   │
│  └─────────────────────────────────────────┘   │
└─────────────────────────────────────────────────┘
```

## Dependency Flow

The key principle of Clean Architecture is the **Dependency Rule**: Source code dependencies point only inward, toward higher-level policies.

```
API → Application → Domain
         ↑
Infrastructure (implements Domain interfaces)
```

## SOLID Principles Applied

### 1. Single Responsibility Principle (SRP)
Each class has one reason to change:
- `CVController`: Handles HTTP requests/responses
- `CVService`: Orchestrates CV generation business logic
- `QuestPdfGeneratorService`: Generates PDF documents
- `CVMappingProfile`: Maps between DTOs and Entities
- Each validator validates one specific DTO

### 2. Open/Closed Principle (OCP)
The system is open for extension but closed for modification:
- New templates can be added without modifying existing code
- New validators can be added without changing the validation framework
- New services can be registered without modifying the DI configuration

### 3. Liskov Substitution Principle (LSP)
Derived classes can substitute base classes:
- All template classes implement `IDocument` and can be used interchangeably
- Service implementations can be swapped without affecting consumers

### 4. Interface Segregation Principle (ISP)
Clients don't depend on interfaces they don't use:
- `ICVGeneratorService`: Single focused interface for PDF generation
- `ICVService`: Single focused interface for CV operations
- Each interface has a specific, well-defined purpose

### 5. Dependency Inversion Principle (DIP)
High-level modules don't depend on low-level modules:
- Application layer depends on Domain interfaces, not Infrastructure implementations
- API layer depends on Application services, not Infrastructure details
- Dependencies are injected via constructor injection

## Data Flow

### CV Generation Flow

```
1. Client Request
   ↓
2. CVController receives GenerateCVRequest
   ↓
3. FluentValidation validates the request
   ↓
4. CVService.GenerateCVAsync()
   ↓
5. AutoMapper maps DTO → Domain Entity
   ↓
6. ICVGeneratorService.GeneratePdfAsync()
   ↓
7. QuestPdfGeneratorService selects template
   ↓
8. Template (Modern/Classic/Creative) generates PDF
   ↓
9. PDF bytes returned to CVService
   ↓
10. GenerateCVResponse created
   ↓
11. CVController returns FileResult
   ↓
12. Client receives PDF
```

## Component Responsibilities

### Domain Layer
**Purpose**: Contains enterprise business rules and entities

**Responsibilities**:
- Define core business entities (CV, PersonalInfo, etc.)
- Define interfaces for external services
- No dependencies on other layers
- Pure business logic

**Files**:
- `Entities/CV.cs`: Core CV entity with all components
- `Interfaces/ICVGeneratorService.cs`: PDF generation interface

### Application Layer
**Purpose**: Contains application-specific business rules

**Responsibilities**:
- Define DTOs for data transfer
- Validate incoming data using FluentValidation
- Map between DTOs and Domain entities using AutoMapper
- Orchestrate use cases via services
- Depends only on Domain layer

**Files**:
- `DTOs/CVDtos.cs`: Data Transfer Objects
- `Validators/CVValidators.cs`: Validation rules
- `MappingProfiles/CVMappingProfile.cs`: AutoMapper configuration
- `Services/CVService.cs`: Application service implementation
- `DependencyInjection.cs`: Service registration

### Infrastructure Layer
**Purpose**: Implements external concerns and I/O

**Responsibilities**:
- Implement Domain interfaces
- Generate PDFs using QuestPDF
- Define CV templates
- Handle external dependencies
- Depends on Application layer

**Files**:
- `PDFGeneration/QuestPdfGeneratorService.cs`: PDF generation implementation
- `Templates/ModernTemplate.cs`: Modern CV design
- `Templates/ClassicTemplate.cs`: Classic CV design
- `Templates/CreativeTemplate.cs`: Creative CV design
- `DependencyInjection.cs`: Infrastructure service registration

### Presentation Layer (API)
**Purpose**: Exposes the application via HTTP endpoints

**Responsibilities**:
- Define API controllers and endpoints
- Handle HTTP requests/responses
- Configure middleware (CORS, Swagger, etc.)
- Manage dependency injection
- Depends on Application and Infrastructure layers

**Files**:
- `Controllers/CVController.cs`: CV API endpoints
- `Program.cs`: Application startup and configuration

## Dependency Injection Configuration

### Service Lifetimes

**Scoped Services**:
- `ICVService` → `CVService`: Created once per request
- Validators: Created once per request

**Singleton Services**:
- `ICVGeneratorService` → `QuestPdfGeneratorService`: Single instance
- AutoMapper: Single instance

**Transient Services**:
- None in current implementation

### Registration Order

```csharp
// Program.cs
builder.Services.AddControllers();
builder.Services.AddApplicationServices();    // Application layer
builder.Services.AddInfrastructureServices(); // Infrastructure layer
```

## Extension Points

The architecture makes it easy to extend:

### Adding New Templates
1. Create new template class implementing `IDocument`
2. Add case to `QuestPdfGeneratorService` switch
3. Update controller to list new template

### Adding New Fields
1. Add properties to Domain entities
2. Add corresponding DTOs
3. Update validators
4. Update AutoMapper profiles
5. Update templates to render new fields

### Adding Authentication
1. Add authentication middleware in API layer
2. Add user context to Application services
3. Store user ID with generated CVs

### Adding Database Storage
1. Create repository interfaces in Domain
2. Implement repositories in Infrastructure
3. Update Application services to use repositories
4. Register repositories in DI

## Testing Strategy

### Unit Tests (Future)
- Domain entities: Business logic validation
- Application services: Use case orchestration
- Validators: Validation rules
- Mappers: DTO/Entity mapping

### Integration Tests (Future)
- API endpoints: End-to-end request/response
- PDF generation: Template rendering
- Database operations: CRUD operations

### Architecture Tests (Future)
- Verify layer dependencies using ArchUnit or similar
- Ensure no circular dependencies
- Validate naming conventions

## Best Practices

1. **Keep Domain Pure**: No external dependencies in Domain layer
2. **Use Interfaces**: Program to interfaces, not implementations
3. **Dependency Injection**: All dependencies injected via constructor
4. **Validation**: Always validate input at the boundary (API layer)
5. **Separation of Concerns**: Each layer has a single, well-defined purpose
6. **Async/Await**: Use async programming for I/O operations
7. **Immutability**: Prefer immutable objects where possible
8. **Error Handling**: Handle errors at appropriate layers
9. **Logging**: Log at service boundaries
10. **Documentation**: Document public APIs with XML comments

## Technology Choices Rationale

### AutoMapper
- Eliminates boilerplate mapping code
- Centralizes mapping configuration
- Type-safe mapping with compile-time checking

### FluentValidation
- Fluent, readable validation rules
- Separation of validation from business logic
- Easy to test validation rules independently

### QuestPDF
- Modern, code-based PDF generation
- Fluent API for document composition
- High-quality output
- Free community license

### .NET Dependency Injection
- Built-in, performant container
- Lifetime management
- Supports all dependency patterns
- No external dependencies needed

---

This architecture provides a solid foundation for building a maintainable, testable, and scalable CV generation application.
