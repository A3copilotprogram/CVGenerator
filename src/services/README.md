# Services Directory

This directory contains service modules for business logic and external integrations.

## Purpose

Implement services such as:
- PDF generation service (for exporting CVs)
- Email notification service
- File storage service (local or cloud)
- Authentication and authorization service
- CV data management service
- Template rendering service
- Export/import services (PDF, Word, JSON)

## Blazor Service Pattern

Services in Blazor are typically:
- Registered in `Program.cs` for dependency injection
- Injected into pages and components via `@inject` directive
- Designed as interfaces with concrete implementations
- Scoped, Transient, or Singleton based on requirements

Services encapsulate business logic and external dependencies, keeping components focused on presentation.

