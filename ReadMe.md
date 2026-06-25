# .NET 10 + ASP.NET Core Web API + EF Core Setup Guide (Ubuntu 24.04)

## Overview

This document explains how to setup a .NET 10 development environment on Ubuntu 24.04 and create an ASP.NET Core Web API project with EF Core support.

Environment:

* OS: Ubuntu 24.04 LTS
* .NET: 10
* Database tooling: DBeaver / SQL Server compatible
* Shell: bash/zsh

---

# 1. Install .NET 10 SDK

Update packages:

```bash
sudo apt update
```

Install SDK:

```bash
sudo apt install -y dotnet-sdk-10.0
```

Verify:

```bash
dotnet --version
```

Expected:

```
10.0.xxx
```

Check installed SDKs:

```bash
dotnet --list-sdks
```

Check runtimes:

```bash
dotnet --list-runtimes
```

---

# 2. Create ASP.NET Core Web API Project

Create a project folder:

```bash
mkdir demoWithDotnet10AndEfCore
cd demoWithDotnet10AndEfCore
```

Create Web API:

```bash
dotnet new webapi --framework net10.0
```

Restore packages:

```bash
dotnet restore
```

---

# 3. Run the Application

Run:

```bash
dotnet run
```

Example output:

```
Now listening on: http://localhost:5276
Application started.
```

Test API:

Browser:

```
http://localhost:5276/weatherforecast
```

or:

```bash
curl http://localhost:5276/weatherforecast
```

---

# 4. Build Project

Build:

```bash
dotnet build
```

Expected:

```
Build succeeded
```

---

# 5. Enable Hot Reload with dotnet watch

Run:

```bash
dotnet watch
```

This enables:

* automatic rebuild
* hot reload
* file monitoring

---

# 6. Fix dotnet watch Linux inotify Error

## Problem

If you see:

```
The configured user limit (128) on the number of inotify instances has been reached
```

`dotnet watch` is failing because Linux file watcher limits are reached.

Check current limits:

```bash
cat /proc/sys/fs/inotify/max_user_instances

cat /proc/sys/fs/inotify/max_user_watches
```

Example:

```
128
65536
```

---

# Why this happens

Linux uses inotify for file watching.

Used by:

* dotnet watch
* VS Code
* Docker
* Node.js tools
* React/Vite
* JetBrains IDEs
* File sync tools

The default Ubuntu limit is often low for development machines.

---

# Increase inotify limits

Temporary change:

```bash
sudo sysctl fs.inotify.max_user_instances=1024
```

Verify:

```bash
cat /proc/sys/fs/inotify/max_user_instances
```

Expected:

```
1024
```

---

## Make it permanent

Edit:

```bash
sudo nano /etc/sysctl.conf
```

Add:

```
fs.inotify.max_user_instances=1024
fs.inotify.max_user_watches=524288
```

Apply:

```bash
sudo sysctl -p
```

Verify:

```bash
cat /proc/sys/fs/inotify/max_user_instances

cat /proc/sys/fs/inotify/max_user_watches
```

---

# Alternative workaround

Use polling instead of inotify:

```bash
DOTNET_USE_POLLING_FILE_WATCHER=1 dotnet watch
```

This uses more CPU but avoids Linux watcher limits.

---

# 7. HTTPS Development Certificate

.NET creates a development HTTPS certificate automatically.

Trust it:

```bash
dotnet dev-certs https --trust
```

If HTTPS is not needed during development, remove:

```csharp
app.UseHttpsRedirection();
```

from:

```
Program.cs
```

---

# 8. Install EF Core CLI Tools

Install:

```bash
dotnet tool install --global dotnet-ef
```

Verify:

```bash
dotnet ef --version
```

Update tool:

```bash
dotnet tool update --global dotnet-ef
```

---

# 9. EF Core Packages

Example SQL Server setup:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

# 10. Basic EF Core Commands

Create migration:

```bash
dotnet ef migrations add Init
```

Apply database changes:

```bash
dotnet ef database update
```

Remove last migration:

```bash
dotnet ef migrations remove
```

List migrations:

```bash
dotnet ef migrations list
```

Create SQL script:

```bash
dotnet ef migrations script
```

---

# 11. DBeaver Database Client (Ubuntu)

Install:

```bash
sudo snap install dbeaver-ce
```

Run:

```bash
dbeaver
```

Use it to connect with:

* SQL Server
* PostgreSQL
* MySQL
* SQLite

---

# 12. Useful .NET CLI Commands

Create console app:

```bash
dotnet new console
```

Create class library:

```bash
dotnet new classlib
```

Create solution:

```bash
dotnet new sln
```

Add project:

```bash
dotnet sln add Project.csproj
```

Clean:

```bash
dotnet clean
```

Run tests:

```bash
dotnet test
```

Show templates:

```bash
dotnet new list
```

---

# 13. Typical Development Workflow

Create project:

```bash
dotnet new webapi --framework net10.0
```

Restore:

```bash
dotnet restore
```

Run:

```bash
dotnet watch
```

Add EF Core:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Create migration:

```bash
dotnet ef migrations add Init
```

Update database:

```bash
dotnet ef database update
```

---

# Notes

The inotify configuration is a Linux system-level setting.

It applies to all applications for the current machine/user environment.

Increasing:

```
128 -> 1024
```

does not reserve memory immediately. It only increases the maximum allowed file watcher instances.

This configuration is commonly used on developer machines running:

* .NET
* VS Code
* Docker
* Node.js
* databases
* IDE tooling

ref for mssql extension in vs code

```
<https://www.youtube.com/playlist?list=PL3EZ3A8mHh0xv6HXxjln5U6JwqSDrh2wG>
```
