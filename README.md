# DotnetFrameworkReferenceLibraries

**Repository:** [sreee2001/DotnetFrameworkReferenceLibraries](https://github.com/sreee2001/DotnetFrameworkReferenceLibraries)  
**Description:** My Personal collection of base libraries for any Dotnet Framework Projects v4.7.2 and above.

**Language:** C#

---

## Table of Contents

1. [Introduction](#introduction)
2. [Projects Overview](#projects-overview)
3. [Project Details](#project-details)
    - [Infrastructure](#infrastructure)
    - [Infrastructure.UI](#infrastructureui)
    - [Repository](#repository)
4. [Interface Summary](#interface-summary)
    - [Infrastructure.Interfaces](#infrastructureinterfaces)
    - [Repository.Interfaces](#repositoryinterfaces)
5. [How to Use](#how-to-use)
    - [As a NuGet Package](#as-a-nuget-package)
    - [As a Git Submodule](#as-a-git-submodule)
6. [References](#references)

---

## Introduction

DotnetFrameworkReferenceLibraries is a curated set of foundational libraries designed to accelerate and standardize .NET Framework projects. The repository includes reusable base components, interfaces, entity models, and utility classes, making it easier to build robust, maintainable applications. The libraries are organized to support various infrastructure needs (data access, UI, validation, and more) and promote good architectural practices.

---

## Projects Overview

This repository is organized into several projects:

- **Infrastructure**: Core base classes and interfaces for data entities and operations.
- **Infrastructure.UI**: UI-related infrastructure, such as converters for data binding.
- **Repository**: Data access abstractions and repository patterns.

---

## Project Details

### Infrastructure

Contains:
- Entity base classes : Used for Binding to UI, provides INotifyPropertyChanged and other interfaces
- POCO entity classes : Plain old clr classes for Database operations
- Interfaces for common behaviors (e.g., identification, editability)
- Base classes for property change notification and error info

### Infrastructure.UI

Contains:
- UI helpers, such as converters for data binding

### Repository

Contains:
- Data access and repository pattern implementations

---

## Interface Summary

### Infrastructure.Interfaces

- `IHaveId`
- `IAmEditable`
- `IHaveName`
- Other utility interfaces

### Repository.Interfaces

- `IDbService`
- `IRepository<T>`
- Additional repository interfaces

---

## How to Use

### As a NuGet Package

If published on NuGet, add the package via NuGet Package Manager:

```powershell
Install-Package DotnetFrameworkReferenceLibraries
```

Or use the Visual Studio NuGet UI.


### As a Git Submodule

Add the repository as a submodule:

bash
git submodule add https://github.com/sreee2001/DotnetFrameworkReferenceLibraries.git
git submodule update --init --recursive

Then reference the relevant projects (e.g., `Infrastructure`, `Repository`) in your solution.

### References
* [Repository Home](https://github.com/sreee2001/DotnetFrameworkReferenceLibraries)
* [Interface Code Search](https://github.com/sreee2001/DotnetFrameworkReferenceLibraries/search?q=interface+)
* [Project Structure](https://github.com/sreee2001/DotnetFrameworkReferenceLibraries/tree/master)

