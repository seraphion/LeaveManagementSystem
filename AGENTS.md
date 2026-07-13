# AGENTS.md - Leave Management System

## Purpose

This repository is an ASP.NET Core MVC Leave Management System using EF Core, SQL Server, Identity and Razer views.

Agents must preserve the layered architecture and modernize toward .NET 10.

## Setup commands

Use these commands from the repository root:

```bash
dotnet --info
dotnet restore LeaveManagementSystem.slnx
dotnet build LeaveManagementSystem.slnx --configuration Release --no-restore
dotnet test LeaveManagementSystem.slnx --configuration Release --no-build
```
