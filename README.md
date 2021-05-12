# Projects
- `AuthServer` - The authentication server
- `Backend` - The main meeting management server
- `MailServer` - The mail server

# Database
- We pick `PostgreSQL` as our relational database management system.

# How to build?
1. Goto https://dotnet.microsoft.com/download/dotnet/5.0 download and install .NET 5.0 SDK.
2. In project folder, enter `dotnet restore` to restore project dependencies.
3. Enter `dotnet build -c Release` to build project.
4. The compiled executables will be under `./bin/Release/net5.0` folder

# Configuration
## How to setup
1. Open `appsettings.json` under output folder.
2. Add config name and value into this file.
    - For example, if you want to set `MailServerAddr` config's value to `https://localhost:8088`, then your `appsettings.json` should look like:
    ```json
    {
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
            }
        },
        "AllowedHosts": "*",
        "MailServerAddr": "https://localhost:8088" // Add this
    }
    ```
---
## Backend
1. `MeetingDatabase_Connection` - The connection to the PostgreSQL database. 
    - Ex. `Host=db.example.com;Database=meetingdb;Username=meetingdb_backend;Password=123;`
2. `AuthServer_Address` - The address of the authentication server.
    - Ex. `http://localhost:8081`
3. `MailServerAddr` - The address of the mail server.
    - Ex. `http://localhost:8082`
## AuthServer
1. `MeetingDatabase_Connection` - The connection to the PostgreSQL database. 
    - Ex. `Host=db.example.com;Database=meetingdb;Username=meetingdb_backend;Password=123;`
## MailServer
1. `SMTP_Server` - The SMTP server to connect to.
    - Ex. `smtp.gmail.com`
2. `SMTP_Account` - Yuor SMTP account.
3. `SMTP_Password` - Your SMTP account password.

# Database Setup
> You should already setup up `MeetingDatabase_Connection` config before doing this.
1. Go into `Backend` folder.
2. Enter `dotnet tool install --global dotnet-ef` to install Entity Framework Core tool.
3. Once done, enter `dotnet ef database update` to make database up to date.
