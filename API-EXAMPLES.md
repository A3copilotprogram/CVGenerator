# CVGenerator API Examples

This document provides comprehensive examples for using the CVGenerator API.

## Table of Contents
- [Basic Usage](#basic-usage)
- [Complete Examples](#complete-examples)
- [Error Handling](#error-handling)
- [Advanced Scenarios](#advanced-scenarios)

## Basic Usage

### Starting the API

```bash
cd src/CVGenerator.API
dotnet run
```

The API will be available at `http://localhost:5000` (or the port specified in `launchSettings.json`).

### Accessing Swagger UI

Navigate to `http://localhost:5000` in your browser to access the interactive Swagger UI documentation.

## Complete Examples

### Example 1: Software Engineer CV (Modern Template)

```bash
curl -X POST http://localhost:5000/api/cv/generate \
  -H "Content-Type: application/json" \
  -d '{
    "cvData": {
      "personalInfo": {
        "firstName": "Jane",
        "lastName": "Smith",
        "email": "jane.smith@email.com",
        "phoneNumber": "+1-555-0199",
        "city": "Seattle",
        "country": "USA",
        "linkedin": "https://linkedin.com/in/janesmith",
        "github": "https://github.com/janesmith"
      },
      "education": [
        {
          "degree": "Master of Science in Computer Science",
          "institution": "University of Washington",
          "location": "Seattle, WA",
          "startDate": "2018-09-01",
          "endDate": "2020-06-01",
          "gpa": 3.9
        },
        {
          "degree": "Bachelor of Science in Software Engineering",
          "institution": "University of Washington",
          "location": "Seattle, WA",
          "startDate": "2014-09-01",
          "endDate": "2018-06-01",
          "gpa": 3.7
        }
      ],
      "workExperience": [
        {
          "jobTitle": "Senior Software Engineer",
          "company": "Microsoft",
          "location": "Redmond, WA",
          "startDate": "2022-01-01",
          "isCurrentPosition": true,
          "responsibilities": [
            "Lead development of Azure cloud services",
            "Architect scalable microservices solutions",
            "Mentor junior engineers and conduct code reviews"
          ],
          "achievements": [
            "Improved system performance by 60%",
            "Led team of 8 engineers on critical project"
          ]
        },
        {
          "jobTitle": "Software Engineer II",
          "company": "Amazon",
          "location": "Seattle, WA",
          "startDate": "2020-07-01",
          "endDate": "2021-12-31",
          "isCurrentPosition": false,
          "responsibilities": [
            "Developed e-commerce platform features",
            "Implemented RESTful APIs using .NET Core",
            "Optimized database queries for better performance"
          ],
          "achievements": [
            "Reduced page load time by 40%"
          ]
        }
      ],
      "skills": [
        {
          "name": "C#",
          "level": "Expert",
          "category": "Programming Languages"
        },
        {
          "name": "Python",
          "level": "Advanced",
          "category": "Programming Languages"
        },
        {
          "name": ".NET Core",
          "level": "Expert",
          "category": "Frameworks"
        },
        {
          "name": "Azure",
          "level": "Advanced",
          "category": "Cloud"
        },
        {
          "name": "SQL Server",
          "level": "Advanced",
          "category": "Databases"
        },
        {
          "name": "Docker",
          "level": "Advanced",
          "category": "DevOps"
        },
        {
          "name": "Kubernetes",
          "level": "Intermediate",
          "category": "DevOps"
        }
      ],
      "languages": [
        "English (Native)",
        "Mandarin (Professional)"
      ],
      "certifications": [
        {
          "name": "Microsoft Certified: Azure Solutions Architect Expert",
          "issuer": "Microsoft",
          "issueDate": "2023-03-15"
        },
        {
          "name": "AWS Certified Developer - Associate",
          "issuer": "Amazon Web Services",
          "issueDate": "2021-11-20"
        }
      ],
      "summary": "Results-driven Senior Software Engineer with 5+ years of experience designing and implementing scalable cloud solutions. Specialized in .NET Core, Azure, and microservices architecture. Proven track record of leading teams and delivering high-impact projects."
    },
    "template": "Modern"
  }' \
  -o jane-smith-cv.pdf
```

### Example 2: Marketing Professional CV (Creative Template)

```bash
curl -X POST http://localhost:5000/api/cv/generate \
  -H "Content-Type: application/json" \
  -d '{
    "cvData": {
      "personalInfo": {
        "firstName": "Michael",
        "lastName": "Chen",
        "email": "michael.chen@email.com",
        "phoneNumber": "+1-555-0234",
        "city": "Los Angeles",
        "country": "USA",
        "linkedin": "https://linkedin.com/in/michaelchen",
        "website": "https://michaelchen.com"
      },
      "education": [
        {
          "degree": "MBA - Marketing",
          "institution": "UCLA Anderson School of Management",
          "location": "Los Angeles, CA",
          "startDate": "2016-09-01",
          "endDate": "2018-06-01",
          "gpa": 3.8
        }
      ],
      "workExperience": [
        {
          "jobTitle": "Senior Marketing Manager",
          "company": "Nike",
          "location": "Los Angeles, CA",
          "startDate": "2021-03-01",
          "isCurrentPosition": true,
          "responsibilities": [
            "Develop and execute integrated marketing campaigns",
            "Manage $5M annual marketing budget",
            "Lead team of 6 marketing specialists"
          ],
          "achievements": [
            "Increased brand awareness by 45%",
            "Generated $10M in additional revenue"
          ]
        },
        {
          "jobTitle": "Marketing Manager",
          "company": "Adidas",
          "location": "Portland, OR",
          "startDate": "2018-07-01",
          "endDate": "2021-02-28",
          "isCurrentPosition": false,
          "responsibilities": [
            "Created digital marketing strategies",
            "Managed social media campaigns",
            "Analyzed market trends and consumer behavior"
          ],
          "achievements": [
            "Grew social media following by 200%"
          ]
        }
      ],
      "skills": [
        {
          "name": "Digital Marketing",
          "level": "Expert",
          "category": "Marketing"
        },
        {
          "name": "SEO/SEM",
          "level": "Advanced",
          "category": "Marketing"
        },
        {
          "name": "Google Analytics",
          "level": "Advanced",
          "category": "Tools"
        },
        {
          "name": "Adobe Creative Suite",
          "level": "Intermediate",
          "category": "Design"
        }
      ],
      "languages": [
        "English (Native)",
        "Spanish (Intermediate)"
      ],
      "certifications": [
        {
          "name": "Google Analytics Certified",
          "issuer": "Google",
          "issueDate": "2022-01-15"
        }
      ],
      "summary": "Creative and data-driven Marketing Manager with 7+ years of experience developing successful multi-channel campaigns. Expertise in digital marketing, brand strategy, and team leadership."
    },
    "template": "Creative"
  }' \
  -o michael-chen-cv.pdf
```

### Example 3: Academic CV (Classic Template)

```bash
curl -X POST http://localhost:5000/api/cv/generate \
  -H "Content-Type: application/json" \
  -d '{
    "cvData": {
      "personalInfo": {
        "firstName": "Dr. Emily",
        "lastName": "Johnson",
        "email": "emily.johnson@university.edu",
        "phoneNumber": "+1-555-0345",
        "city": "Boston",
        "country": "USA"
      },
      "education": [
        {
          "degree": "Ph.D. in Computer Science",
          "institution": "MIT",
          "location": "Cambridge, MA",
          "startDate": "2015-09-01",
          "endDate": "2019-06-01",
          "description": "Dissertation: Machine Learning Applications in Healthcare",
          "gpa": 4.0
        },
        {
          "degree": "M.S. in Computer Science",
          "institution": "Stanford University",
          "location": "Stanford, CA",
          "startDate": "2013-09-01",
          "endDate": "2015-06-01",
          "gpa": 3.95
        }
      ],
      "workExperience": [
        {
          "jobTitle": "Associate Professor",
          "company": "Harvard University",
          "location": "Cambridge, MA",
          "startDate": "2022-09-01",
          "isCurrentPosition": true,
          "responsibilities": [
            "Teach graduate and undergraduate courses in AI and ML",
            "Conduct research in machine learning applications",
            "Supervise Ph.D. and Master's students",
            "Serve on university committees"
          ],
          "achievements": [
            "Published 15 papers in top-tier conferences",
            "Secured $2M in research funding"
          ]
        }
      ],
      "skills": [
        {
          "name": "Machine Learning",
          "level": "Expert",
          "category": "Research"
        },
        {
          "name": "Python",
          "level": "Expert",
          "category": "Programming"
        },
        {
          "name": "Research Methodology",
          "level": "Expert",
          "category": "Academic"
        }
      ],
      "languages": [
        "English (Native)",
        "French (Fluent)"
      ],
      "certifications": [],
      "summary": "Accomplished researcher and educator specializing in machine learning and artificial intelligence. Dedicated to advancing the field through innovative research and mentoring the next generation of computer scientists."
    },
    "template": "Classic"
  }' \
  -o emily-johnson-cv.pdf
```

## Error Handling

### Example: Missing Required Fields

```bash
curl -X POST http://localhost:5000/api/cv/generate \
  -H "Content-Type: application/json" \
  -d '{
    "cvData": {
      "personalInfo": {
        "firstName": "",
        "lastName": "Test",
        "email": "invalid-email"
      }
    },
    "template": "Modern"
  }'
```

**Response (400 Bad Request):**
```json
{
  "title": "Validation failed",
  "status": 400,
  "errors": {
    "CVData.PersonalInfo.FirstName": [
      "First name is required"
    ],
    "CVData.PersonalInfo.Email": [
      "Invalid email format"
    ],
    "CVData.Education": [
      "At least one education entry is required"
    ]
  }
}
```

### Example: Invalid Template

```bash
curl -X POST http://localhost:5000/api/cv/generate \
  -H "Content-Type: application/json" \
  -d '{
    "cvData": { ... },
    "template": "InvalidTemplate"
  }'
```

**Response (400 Bad Request):**
```json
{
  "title": "Validation failed",
  "status": 400,
  "errors": {
    "Template": [
      "Template must be: Modern, Classic, or Creative"
    ]
  }
}
```

## Advanced Scenarios

### Using PowerShell (Windows)

```powershell
# Create CV request
$cvData = @{
    cvData = @{
        personalInfo = @{
            firstName = "John"
            lastName = "Doe"
            email = "john.doe@example.com"
        }
        education = @(
            @{
                degree = "BS Computer Science"
                institution = "Tech University"
                startDate = "2015-09-01"
                endDate = "2019-06-01"
            }
        )
        workExperience = @()
        skills = @(
            @{
                name = "C#"
                level = "Expert"
                category = "Programming"
            }
        )
        languages = @("English")
        certifications = @()
        summary = "Software developer"
    }
    template = "Modern"
} | ConvertTo-Json -Depth 10

# Generate CV
Invoke-RestMethod `
    -Uri "http://localhost:5000/api/cv/generate" `
    -Method Post `
    -ContentType "application/json" `
    -Body $cvData `
    -OutFile "john-doe-cv.pdf"
```

### Using JavaScript/Node.js

```javascript
const fetch = require('node-fetch');
const fs = require('fs');

async function generateCV() {
  const cvData = {
    cvData: {
      personalInfo: {
        firstName: "Sarah",
        lastName: "Williams",
        email: "sarah.williams@example.com",
        phoneNumber: "+1-555-0456",
        city: "New York",
        country: "USA"
      },
      education: [
        {
          degree: "Bachelor of Arts in Design",
          institution: "Parsons School of Design",
          startDate: "2016-09-01",
          endDate: "2020-06-01",
          gpa: 3.6
        }
      ],
      workExperience: [
        {
          jobTitle: "UX Designer",
          company: "Apple",
          location: "Cupertino, CA",
          startDate: "2020-08-01",
          isCurrentPosition: true,
          responsibilities: [
            "Design user interfaces for iOS applications",
            "Conduct user research and usability testing",
            "Collaborate with developers and product managers"
          ],
          achievements: [
            "Improved app usability score by 35%"
          ]
        }
      ],
      skills: [
        {
          name: "Figma",
          level: "Expert",
          category: "Design Tools"
        },
        {
          name: "User Research",
          level: "Advanced",
          category: "UX"
        }
      ],
      languages: ["English (Native)"],
      certifications: [],
      summary: "Creative UX Designer with a passion for creating intuitive user experiences."
    },
    template: "Creative"
  };

  try {
    const response = await fetch('http://localhost:5000/api/cv/generate', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(cvData)
    });

    if (response.ok) {
      const buffer = await response.buffer();
      fs.writeFileSync('sarah-williams-cv.pdf', buffer);
      console.log('CV generated successfully!');
    } else {
      const error = await response.json();
      console.error('Error:', error);
    }
  } catch (error) {
    console.error('Request failed:', error);
  }
}

generateCV();
```

### Using Python

```python
import requests
import json

def generate_cv():
    cv_data = {
        "cvData": {
            "personalInfo": {
                "firstName": "Robert",
                "lastName": "Martinez",
                "email": "robert.martinez@example.com",
                "phoneNumber": "+1-555-0567",
                "city": "Austin",
                "country": "USA"
            },
            "education": [
                {
                    "degree": "Bachelor of Science in Data Science",
                    "institution": "University of Texas",
                    "startDate": "2017-09-01",
                    "endDate": "2021-06-01",
                    "gpa": 3.75
                }
            ],
            "workExperience": [
                {
                    "jobTitle": "Data Scientist",
                    "company": "Tesla",
                    "location": "Austin, TX",
                    "startDate": "2021-07-01",
                    "isCurrentPosition": True,
                    "responsibilities": [
                        "Build machine learning models for autonomous driving",
                        "Analyze large datasets for insights",
                        "Develop data pipelines"
                    ],
                    "achievements": [
                        "Improved model accuracy by 25%"
                    ]
                }
            ],
            "skills": [
                {
                    "name": "Python",
                    "level": "Expert",
                    "category": "Programming"
                },
                {
                    "name": "Machine Learning",
                    "level": "Advanced",
                    "category": "Data Science"
                }
            ],
            "languages": ["English (Native)", "Spanish (Native)"],
            "certifications": [],
            "summary": "Data Scientist specializing in machine learning and AI."
        },
        "template": "Modern"
    }

    try:
        response = requests.post(
            'http://localhost:5000/api/cv/generate',
            json=cv_data,
            headers={'Content-Type': 'application/json'}
        )
        
        if response.status_code == 200:
            with open('robert-martinez-cv.pdf', 'wb') as f:
                f.write(response.content)
            print('CV generated successfully!')
        else:
            print(f'Error: {response.status_code}')
            print(response.json())
    except Exception as e:
        print(f'Request failed: {e}')

if __name__ == '__main__':
    generate_cv()
```

## Tips and Best Practices

1. **Always validate locally first**: Use the Swagger UI to test your JSON before scripting
2. **Save request templates**: Keep sample JSON files for different CV types
3. **Use proper date formats**: ISO 8601 format (YYYY-MM-DD)
4. **Handle errors gracefully**: Check response status codes and parse error messages
5. **Test all templates**: Generate samples with each template to see which fits best
6. **Keep it concise**: Be specific but brief in descriptions
7. **Use categories**: Group skills by category for better organization
8. **Current positions**: Set `isCurrentPosition: true` and `endDate: null` for current jobs

## Getting Help

- **Swagger UI**: Interactive documentation at `http://localhost:5000`
- **Health Check**: Verify API is running at `http://localhost:5000/api/cv/health`
- **Templates List**: Get available templates at `http://localhost:5000/api/cv/templates`

---

For more information, see the [README.md](README.md) and [ARCHITECTURE.md](ARCHITECTURE.md) files.
