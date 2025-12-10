# CV Generator System Architecture

This document describes the folder structure and architecture of the CV Generator application built with **Blazor Web Server**.

## Overview

The CV Generator is a Blazor Web Server application that allows users to create, customize, and export professional curriculum vitae (CVs) and resumes using various templates. Blazor enables rich interactive web UI using C# instead of JavaScript.

## Technology Stack

- **Blazor Web Server** - Server-side Blazor with SignalR for real-time UI updates
- **ASP.NET Core** - Hosting framework
- **C#** - Primary programming language
- **Razor Components** - Component-based UI architecture

## Directory Structure

```
CVGenerator/
├── src/                    # Application source code
│   ├── Components/        # Reusable Blazor components
│   ├── Pages/             # Routable Blazor pages (with @page directive)
│   ├── Shared/            # Shared layouts and navigation components
│   ├── Models/            # Data models and entities
│   ├── Services/          # Business logic and external integrations
│   ├── Utils/             # Utility functions and helpers
│   └── Config/            # Application configuration
├── wwwroot/               # Static web assets
│   ├── css/              # Stylesheets
│   ├── js/               # JavaScript for interop
│   └── images/           # Image assets
├── tests/                 # Test files
│   ├── unit/             # Unit tests
│   └── integration/      # Integration tests
├── templates/             # CV template designs
├── data/                  # Sample data and schemas
└── docs/                  # Documentation
```

## Core Components

### Source Code (`src/`)

The main application logic is organized following Blazor Web Server patterns:

- **Components**: Reusable Blazor components (.razor files) for forms, cards, dialogs, etc.
- **Pages**: Routable pages with `@page` directives for different app sections
- **Shared**: Shared components like layouts, navigation menus, headers, and footers
- **Models**: C# classes defining data structures (User, CV, WorkExperience, Education, Skills)
- **Services**: Business logic and integrations (injected via dependency injection)
- **Utils**: Helper functions and extension methods
- **Config**: Configuration classes and settings

### Static Assets (`wwwroot/`)

Static files served directly to the browser:
- CSS stylesheets (including component-specific isolated CSS)
- JavaScript files for Blazor JS interop
- Images, icons, and other media

### Tests (`tests/`)

Comprehensive testing using bUnit for Blazor components:
- **Unit tests**: Test individual components, services, and utility functions
- **Integration tests**: Test component interactions and service integrations

### Templates (`templates/`)

Pre-designed CV layouts as Blazor components or data templates:
- Professional templates
- Creative templates
- Academic templates
- Each template is a customizable Blazor component or layout

### Data (`data/`)

Supporting data files:
- JSON schemas for validation
- Sample CV data
- Seed data for development

## Blazor Web Server Architecture Patterns

### Component-Based UI

Blazor uses a component-based architecture where:
- Components are reusable UI elements defined in `.razor` files
- Components can have parameters for data binding
- Components support two-way binding with `@bind` directive
- Event handling is done with C# methods, not JavaScript

### Dependency Injection

Services are registered in `Program.cs` and injected into components:
```csharp
@inject ICVService CVService
@inject NavigationManager Navigation
```

### State Management

State can be managed through:
- Component parameters and cascading parameters
- Scoped services for user session state
- Application state services for global state
- Browser storage (via JS interop) for persistence

### SignalR Connection

Blazor Web Server maintains a real-time SignalR connection:
- UI updates are sent from server to client
- User interactions are sent from client to server
- Efficient delta updates minimize bandwidth

## Architecture Principles

1. **Component-Based Design**: UI built from composable, reusable Blazor components
2. **Separation of Concerns**: Components handle UI, services handle business logic
3. **Dependency Injection**: Services injected where needed for testability
4. **Type Safety**: C# throughout the stack provides compile-time safety
5. **Server-Side Rendering**: UI logic runs on server, reducing client-side complexity

## Getting Started

Each major directory contains its own README.md file with specific details about its contents and usage. Please refer to these files for more information:

- [src/README.md](src/README.md) - Source code organization
- [src/Components/README.md](src/Components/README.md) - Blazor components
- [src/Pages/README.md](src/Pages/README.md) - Routable pages
- [src/Shared/README.md](src/Shared/README.md) - Shared layouts
- [wwwroot/README.md](wwwroot/README.md) - Static assets
- [tests/README.md](tests/README.md) - Testing guidelines
- [templates/README.md](templates/README.md) - CV templates
- [data/README.md](data/README.md) - Data resources

## Blazor Web Server Benefits

- **C# everywhere**: Write both frontend and backend in C#
- **Rich interactive UI**: Real-time updates without page refreshes
- **Code sharing**: Share models and logic between client and server
- **Secure**: Business logic stays on server
- **SEO friendly**: Server-side rendering for initial load
- **Easy debugging**: Debug C# code in Visual Studio or VS Code

## Future Enhancements

This architecture is designed to accommodate future features such as:
- User authentication with ASP.NET Core Identity
- Real-time collaboration via SignalR
- Cloud storage integration (Azure Blob Storage, AWS S3)
- Template marketplace with component library
- Export to multiple formats (PDF, Word, LaTeX)
- AI-powered content suggestions using Azure OpenAI
- Progressive Web App (PWA) capabilities
- Offline support with local storage
