# CVGenerator - Professional CV/Resume Generator API

A .NET Core application for generating professional CVs/resumes from user data with selectable templates. Built with Clean Architecture, SOLID principles, and best practices.

## 🏗️ Architecture

This application follows **Clean Architecture** principles with clear separation of concerns:

```
CVGenerator/
├── src/
│   ├── CVGenerator.Domain/          # Core business entities and interfaces
│   │   ├── Entities/                # Domain models (CV, PersonalInfo, etc.)
│   │   └── Interfaces/              # Core service interfaces
│   │
│   ├── CVGenerator.Application/     # Business logic layer
│   │   ├── DTOs/                    # Data Transfer Objects
│   │   ├── Validators/              # FluentValidation validators
│   │   ├── MappingProfiles/         # AutoMapper profiles
│   │   └── Services/                # Application services
│   │
│   ├── CVGenerator.Infrastructure/  # External concerns
│   │   ├── PDFGeneration/           # QuestPDF generator service
│   │   └── Templates/               # CV template implementations
│   │
│   └── CVGenerator.API/             # Presentation layer
│       ├── Controllers/             # API endpoints
│       └── Program.cs               # Application configuration
│
├── CVGenerator.sln                  # Solution file
└── README.md                        # This file
```

## 🎯 Key Features

- **Clean Architecture**: Separation of concerns with dependency inversion
- **SOLID Principles**: Following industry best practices
- **Dependency Injection**: Built-in .NET Core DI container
- **Data Validation**: FluentValidation for comprehensive validation
- **Object Mapping**: AutoMapper for DTO/Entity mapping
- **PDF Generation**: QuestPDF for high-quality PDF output
- **Multiple Templates**: Modern, Classic, and Creative CV designs
- **RESTful API**: Well-documented API endpoints with Swagger
- **Type Safety**: Full nullable reference types support

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or Rider (optional)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/A3copilotprogram/CVGenerator.git
   cd CVGenerator
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the API**
   ```bash
   cd src/CVGenerator.API
   dotnet run
   ```

The API will start on `https://localhost:5001` or `http://localhost:5000` by default.

### Accessing Swagger UI

Once the API is running, navigate to:
- **Development**: `http://localhost:5000` (Swagger UI is set as the default page)
- **Swagger JSON**: `http://localhost:5000/swagger/v1/swagger.json`

## 📚 API Endpoints

### 1. Generate CV
**POST** `/api/cv/generate`

Generates a CV PDF from provided data using the selected template.

**Request Body:**
```json
{
  "cvData": {
    "personalInfo": {
      "firstName": "John",
      "lastName": "Doe",
      "email": "john.doe@example.com",
      "phoneNumber": "+1-555-0123",
      "address": "123 Main Street",
      "city": "San Francisco",
      "country": "USA",
      "linkedin": "https://linkedin.com/in/johndoe",
      "github": "https://github.com/johndoe",
      "website": "https://johndoe.com"
    },
    "education": [
      {
        "degree": "Bachelor of Science in Computer Science",
        "institution": "Stanford University",
        "location": "Stanford, CA",
        "startDate": "2015-09-01",
        "endDate": "2019-06-01",
        "description": "Focus on Software Engineering",
        "gpa": 3.8
      }
    ],
    "workExperience": [
      {
        "jobTitle": "Senior Software Engineer",
        "company": "Tech Corp",
        "location": "San Francisco, CA",
        "startDate": "2021-01-01",
        "endDate": null,
        "isCurrentPosition": true,
        "responsibilities": [
          "Led development of microservices architecture"
        ],
        "achievements": [
          "Reduced API response time by 40%"
        ]
      }
    ],
    "skills": [
      {
        "name": "C#",
        "level": "Expert",
        "category": "Programming Languages"
      }
    ],
    "languages": ["English (Native)", "Spanish (Intermediate)"],
    "certifications": [
      {
        "name": "Microsoft Certified: Azure Developer",
        "issuer": "Microsoft",
        "issueDate": "2022-05-15",
        "credentialId": "AZ-204-12345"
      }
    ],
    "summary": "Experienced software engineer..."
  },
  "template": "Modern"
}
```

**Response:** PDF file download

**Available Templates:**
- `Modern` - Clean and professional design with blue accents
- `Classic` - Traditional black and white layout
- `Creative` - Two-column layout with purple accents

### 2. Get Available Templates
**GET** `/api/cv/templates`

Returns a list of available CV templates.

**Response:**
```json
{
  "templates": [
    {
      "name": "Modern",
      "description": "Clean and professional design with blue accent colors"
    },
    {
      "name": "Classic",
      "description": "Traditional black and white layout, perfect for conservative industries"
    },
    {
      "name": "Creative",
      "description": "Two-column layout with purple accents, ideal for creative professionals"
    }
  ]
}
```

### 3. Health Check
**GET** `/api/cv/health`

Health check endpoint to verify API status.

**Response:**
```json
{
  "status": "Healthy",
  "timestamp": "2025-12-10T04:35:17.0678281Z"
}
```

## 🎨 CV Templates

### Modern Template
- Clean, professional design
- Blue accent colors
- Single-column layout
- Perfect for tech and modern industries

### Classic Template
- Traditional black and white design
- Centered header
- Formal typography
- Ideal for conservative industries (finance, law, academia)

### Creative Template
- Two-column layout
- Purple accent colors
- Sidebar for skills and certifications
- Great for creative fields (design, marketing, arts)

## 🔧 Technology Stack

### Core Technologies
- **.NET 8.0**: Latest .NET framework
- **ASP.NET Core**: Web API framework
- **C# 12**: Latest C# language features

### Libraries
- **AutoMapper 12.0.1**: Object-to-object mapping
- **FluentValidation 12.1.1**: Validation library
- **QuestPDF 2025.7.4**: PDF generation engine
- **Swashbuckle (Swagger)**: API documentation

### Design Patterns & Principles
- Clean Architecture
- SOLID Principles
- Dependency Injection
- Repository Pattern (interfaces)
- DTO Pattern

## 🧪 Testing the API

### Using cURL

**Generate CV with Modern template:**
```bash
curl -X POST http://localhost:5000/api/cv/generate \
  -H "Content-Type: application/json" \
  -d @sample-cv-request.json \
  -o my-cv.pdf
```

**Get available templates:**
```bash
curl http://localhost:5000/api/cv/templates
```

### Using PowerShell

```powershell
$body = Get-Content sample-cv-request.json -Raw
Invoke-RestMethod -Uri "http://localhost:5000/api/cv/generate" `
  -Method Post `
  -ContentType "application/json" `
  -Body $body `
  -OutFile "my-cv.pdf"
```

## 📋 Validation Rules

The API validates all input data using FluentValidation:

### Personal Information
- First name and last name are required (max 50 chars)
- Valid email address required
- URLs (LinkedIn, GitHub, website) must be valid HTTP/HTTPS URLs

### Education
- At least one education entry required
- Degree and institution are required
- Start date cannot be in the future
- End date must be after start date
- GPA must be between 0 and 4.0

### Work Experience
- Job title and company are required
- Start date cannot be in the future
- End date must be after start date (if not current position)
- Current positions should not have an end date

### Skills
- Skill name is required
- Level must be: Beginner, Intermediate, Advanced, or Expert

### Certifications
- Name and issuer are required
- Issue date cannot be in the future
- Expiry date must be after issue date

## 🔒 Best Practices Implemented

1. **Clean Architecture**: Clear separation between layers
2. **SOLID Principles**: Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion
3. **Dependency Injection**: All services registered in DI container
4. **DTO Pattern**: Separate models for API and domain
5. **Validation**: Comprehensive input validation
6. **Error Handling**: Proper exception handling and error responses
7. **Logging**: Structured logging throughout the application
8. **API Documentation**: Swagger/OpenAPI documentation
9. **Nullable Reference Types**: Enabled for type safety
10. **Async/Await**: Async programming throughout

## 🛠️ Development

### Project Structure

Each layer has a specific responsibility:

- **Domain**: Contains core business entities and interfaces. No dependencies on other layers.
- **Application**: Contains business logic, DTOs, validators, and service implementations. Depends only on Domain.
- **Infrastructure**: Contains implementations of external concerns (PDF generation). Depends on Application.
- **API**: Contains controllers and configuration. Depends on Application and Infrastructure.

### Adding a New Template

1. Create a new class in `CVGenerator.Infrastructure/Templates/` implementing `IDocument`
2. Add the template to the `QuestPdfGeneratorService` switch statement
3. Update the `CVController` templates list
4. Update the `CVTemplate` enum in the Domain layer

### Extending Functionality

To add new features:
1. Add domain models in the Domain layer
2. Create DTOs and validators in the Application layer
3. Update AutoMapper profiles
4. Implement services in the Application layer
5. Add API endpoints in the API layer

## 📦 NuGet Packages

```xml
<!-- Application Layer -->
<PackageReference Include="AutoMapper" Version="12.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
<PackageReference Include="FluentValidation" Version="12.1.1" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="12.1.1" />

<!-- Infrastructure Layer -->
<PackageReference Include="QuestPDF" Version="2025.7.4" />

<!-- API Layer -->
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.22" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
```

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:
1. Fork the repository
2. Create a feature branch
3. Follow the existing code style and architecture
4. Add tests for new features
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For issues, questions, or contributions:
- Open an issue on GitHub
- Contact: support@cvgenerator.com

## 🎓 Learning Resources

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [QuestPDF Documentation](https://www.questpdf.com/)

## 🗺️ Roadmap

Future enhancements:
- [ ] Add authentication and authorization
- [ ] Implement CV storage (database)
- [ ] Add more template options
- [ ] Support for multiple languages
- [ ] Add CV preview endpoint
- [ ] Implement rate limiting
- [ ] Add unit and integration tests
- [ ] Deploy to Azure/AWS
- [ ] Add Docker support

---

**Built with ❤️ using .NET Core and Clean Architecture principles**
