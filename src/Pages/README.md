# Pages Directory

This directory contains routable Blazor pages for the CV Generator application.

## Purpose

Store Blazor pages that users can navigate to via URLs. Each page uses the `@page` directive to define its route:
- `Index.razor` - Home page / Dashboard
- `CreateCV.razor` - CV creation page
- `EditCV.razor` - CV editing page
- `Templates.razor` - Template selection page
- `Preview.razor` - CV preview page
- `Export.razor` - Export options page

## Page Structure

Each page typically includes:
- `@page` directive with the route
- HTML markup and Blazor components
- C# code for page logic (inline or code-behind)
- Dependency injection for services
- State management and navigation logic

Pages should coordinate between components and services to provide complete user workflows.
