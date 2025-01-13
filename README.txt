Summary of Project
This project involves the development of an ASP.NET Core Razor Pages web application that allows the management of events, guests, staff, venues, and food services. 
It connects to a series of web APIs, including the ThAmCo.Venues API for managing venue reservations and the ThAmCo.Catering API for managing food bookings. 
The primary objectives are to allow users to create, manage, and view events, guests, food bookings, staff, and venues, while ensuring proper access control for various user roles.

The project utilizes ASP.NET Core and Entity Framework Core, following best practices in MVC design patterns, and making extensive use of asynchronous programming for better performance.

Tools and Technologies
ASP.NET Core 6.0: The project uses the ASP.NET Core framework, version 6.0.
Entity Framework Core: This is used to interact with the database and handle the data models for events, guests, staff, and venues.
Razor Pages: Used for building the UI of the web application, which is highly interactive and user-friendly.
SQLite Database: SQLite has been used as the database solution for storing event, guest, staff, and venue information.
ThAmCo.Catering API: For managing food menus, items, and bookings for events.
ThAmCo.Venues API: For managing venue bookings and availability.
Visual Studio 2022: Used for development and debugging of the application.


Test and Deployment Environments
Development Environment:

Visual Studio 2022 (or later) on Windows or MacOS.
SQLite Database is used for development and testing.
ThAmCo.Venues and ThAmCo.Catering APIs are external services required for testing certain functionalities (these APIs are provided and should not be modified).

Deployment Environment:

The application is intended to be deployed to a cloud service (Azure or similar) for production use.
A production-grade relational database (SQL Server or PostgreSQL) will be used in place of SQLite.
The application will be accessible via web browsers with standard security measures like SSL/TLS.

Academic Details
Module: Web Development and ASP.NET Core
Coursework: Development of an intranet event management system
Instructor: [Barry Hebron]


Assumptions
The ThAmCo.Venues and ThAmCo.Catering APIs are available and functional.
The development will be done in line with industry-standard best practices for ASP.NET Core applications.
The application will be used internally within a business context and will only be accessible to authenticated users.

Code/Design Decision Justifications
Entity Framework Core:
Chosen for data persistence as it allows for efficient interaction with a relational database and automatic migrations. 
Entity Framework Core is ideal for handling complex relationships like events, guests, and staff.

Razor Pages:
Razor Pages was chosen for building the UI as it provides a simple, clean architecture for the project. 
Razor Pages makes it easier to manage each feature on separate pages with minimal boilerplate code.


Asynchronous Programming: 
The application uses asynchronous programming to ensure that database queries, API calls, and other time-consuming operations do not block the main thread. 
This improves the performance and responsiveness of the application.


Role-Based Access Control (RBAC): 
User access control is implemented using role-based permissions to ensure that only users with the appropriate roles (Managers, Team Leaders) can perform sensitive actions,
such as deleting events or adjusting staff assignments.


Event-Driven Design: 
The system is built around events and interactions between events, staff, guests, and venues, making it easier to scale and modify in the future.


Key Functionalities Implemented
Web API Services (ThAmCo.Catering):
Create, edit, delete, and list food items and menus.
Add or remove food items from menus.
Book, edit, and cancel food bookings for events.

Razor Pages Web Application:
Create, edit, and delete guests.
Create events, specifying the title, date, and type.
Edit event details (except for date and type).
Book guests onto events and manage guest attendance.
Display guest details, including associated events and attendance status.

Staff Management:
Create, edit, and delete staff members.
Adjust staffing of events by adding or removing staff.
Display staff details, including upcoming events they are assigned to.

Venue Management:
Reserve available venues for events via ThAmCo.Venues API.
Free previously associated venues when canceling or modifying events.
Display available venues, filtered by event type and date range.

Event Management:
Display detailed event information, including guests, venue, and staff.
Cancel or delete events, freeing associated venues and staff.

Security Features
Login Authentication: 
A login page allows only authenticated users to access the application. This will ensure that only authorised users can perform actions like booking guests and modifying event details.

Role-Based Access Control (RBAC): 
Different user roles (Managers, Team Leaders) will have different permissions. For example, only Managers will have the ability to delete staff or events permanently, while Team Leaders can adjust staffing.

Data Protection: 
Sensitive personal data (such as guest information) is handled securely, and features like data anonymisation will be included to comply with data protection regulations.

Secure API Calls: 
Communication with the ThAmCo.Venues and ThAmCo.Catering APIs will be secured via HTTPS to ensure that sensitive data is protected during transmission.


Known Issues
Staffing Management: 
The functionality for adding and removing staff from events is mostly functional but requires additional testing and refinement to handle edge cases and ensure seamless functionality.

Error Handling: There are some areas where error handling needs to be improved, especially when dealing with database constraints or API failures.

Future Improvements
Login and User Management: 
Although basic user authentication is planned, implementing a full-fledged user management system with registration, password recovery, and role assignment is a future priority.

Enhanced Error Handling: More robust error handling and validation will be added, especially for actions that involve multiple entities like events and staff

GitHub Repository Link: https://github.com/harrismahmoud/ThAmCo