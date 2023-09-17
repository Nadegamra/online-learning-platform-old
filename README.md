# online-learning-platform (WIP)
An example platform for creating and taking online courses
## Tech stack
- .NET7
- React
- MySql
- RabbitMQ
- Docker
## Current platform status
| Functionality                  | API           | [Frontend]     |
| ------------------------------ |:-------------:| :-------------:|
| [Authentication]               | ⏳            | ✅             |
| [Course Management]            | ✅            | ⏳             |
| Course Content Management      | ❌            | ❌             |
| Course Participation           | ❌            | ❌             |
| Test Taking                    | ❌            | ❌             |

[Authentication]: https://github.com/Nadegamra/microservices-authentication
[Course Management]: https://github.com/Nadegamra/microservices-course_management
[Frontend]: https://github.com/Nadegamra/olp-frontend-spa

## Launch instructions
### General requirements
- Having docker installed on the device
### Backend
#### Requirements
- Having an email (for sending Authentication service emails)
- Having a Google Drive API Service Account (for storing course images)
#### Instructions
- Create auth_dev.env file inside `./Backend` directory. It should contain these environment variables:
  - Smtp__EmailAddress (Email address from which to send account confirmation emails, etc.)
  - Smtp__EmailPass (If you are using gmail, you should generate an App Password, but this could differ according to email provider)
  - Smtp__TestEmail (Used for testing. Email to which all mail is redirected, leave empty to turn off this functionality)
- Get a Google Drive API Service Account json key, rename it to `key.json` and place inside the `./Backend/Services/CourseManagement/` directory
- Execute `dotnet ef database update` in `./Backend/Services/Authentication` and `./Backend/Services/CourseManagement`
- Execute `docker-compose up` from `./Backend` directory
### Frontend
#### Instructions
- Execute `docker-compose up` from `./Frontend` directory
