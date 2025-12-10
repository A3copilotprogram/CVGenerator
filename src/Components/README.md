# Components Directory

This directory contains reusable Blazor components for the CV Generator application.

## Purpose

Store reusable UI components that are used across multiple pages:
- Form components (input fields, dropdowns, date pickers)
- CV section components (work experience, education, skills)
- Display components (CV preview, template selector)
- Dialog and modal components
- Custom input components

## Blazor Component Structure

Each component typically consists of:
- `.razor` file containing the HTML markup and C# code
- Optional `.razor.cs` code-behind file for complex logic
- Optional `.razor.css` for component-specific styling (CSS isolation)

Components should be designed to be reusable, testable, and follow the single responsibility principle.
