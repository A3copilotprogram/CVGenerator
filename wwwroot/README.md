# wwwroot - Static Web Assets

This directory contains static assets that are served directly to the client in Blazor applications.

## Structure

- **`css/`** - Stylesheets for the application UI
- **`js/`** - JavaScript files for interop and client-side functionality
- **`images/`** - Image assets (logos, icons, sample avatars, etc.)

## Blazor Static Assets

In Blazor Web Server:
- Files in `wwwroot` are served as static content
- CSS files can be referenced in components or layouts
- JavaScript files are loaded via `<script>` tags or JS interop
- Images are referenced using paths like `/images/logo.png`
- Global styles are typically in `wwwroot/css/app.css` or `site.css`

These assets are optimized and served directly by the web server without processing.
