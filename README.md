# C# — Generic Repository Pattern with Service Layer

<p align="center">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET%20Framework-4.7.2-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Visual%20Studio-2022-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white" />
  <img src="https://img.shields.io/badge/Pattern-Repository-blue?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Architecture-3--Layer-orange?style=for-the-badge" />
</p>

<p align="center">
  A clean, fully layered <strong>C# console application</strong> demonstrating the <strong>Generic Repository Pattern</strong> with <strong>Dependency Injection</strong> and a dedicated <strong>Service Layer</strong> — built around a Student Enrollment domain.
</p>

---

## 📋 Table of Contents

- [Project Overview](#-project-overview)
- [Architecture](#-architecture)
- [Layer Breakdown](#-layer-breakdown)
  - [Model / Entity Layer](#-layer-1--model--entity)
  - [Repository Layer](#-layer-2--repository)
  - [Service Layer](#-layer-3--service-layer)
  - [Entry Point](#-entry-point--programcs)
- [Design Patterns & Concepts](#-design-patterns--concepts)
- [Data Flow Diagram](#-data-flow-diagram)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Expected Output](#-expected-output)
- [Author](#-author)

---

## 🌟 Project Overview

This project implements the **Repository Design Pattern** in C# using a generic, reusable repository that works with any entity that extends `BaseModel`. The architecture cleanly separates data access, business logic, and presentation into three independent layers — mirroring the structure used in real-world ASP.NET MVC and Web API applications.

The domain model represents a **Student Enrollment System** where students can be registered, updated, deleted, and listed with their enrolled courses — all managed through a fully abstracted data pipeline.

| Attribute | Detail |
|---|---|
| **Language** | C# |
| **Framework** | .NET Framework 4.7.2 |
| **IDE** | Visual Studio 2022 |
| **Pattern** | Generic Repository + Service Layer |
| **Storage** | In-memory `List<T>` (no database required) |
| **Output** | Console Application |

---

## 🏗️ Architecture

The solution is organized into **three distinct layers**, each with a single responsibility:

```
┌─────────────────────────────────────────────────────────────┐
│                    ENTRY POINT (Program.cs)                  │
│         Wires dependencies, seeds data, runs CRUD ops        │
└──────────────────────────┬──────────────────────────────────┘
                           │ uses
┌──────────────────────────▼──────────────────────────────────┐
│                  SERVICE LAYER                               │
│              StudentService                                  │
│   Register · UpdateInfo · DeleteStudent · ShowAll            │
│   depends on IRepository<Student> (injected via constructor) │
└──────────────────────────┬──────────────────────────────────┘
                           │ depends on (abstraction)
┌──────────────────────────▼──────────────────────────────────┐
│                  REPOSITORY LAYER                            │
│  ┌─────────────────────────────────────┐                    │
│  │  «interface»  IRepository<T>        │                    │
│  │  GetAll · GetById · Insert          │                    │
│  │  Update · Delete                    │                    │
│  │  where T : BaseModel                │                    │
│  └────────────────┬────────────────────┘                    │
│                   │ implements                               │
│  ┌────────────────▼────────────────────┐                    │
│  │  GenericRepository<T>               │                    │
│  │  private List<T> _data              │                    │
│  │  CRUD operations on in-memory store │                    │
│  └─────────────────────────────────────┘                    │
└──────────────────────────┬──────────────────────────────────┘
                           │ operates on
┌──────────────────────────▼──────────────────────────────────┐
│                  MODEL / ENTITY LAYER                        │
│  ┌──────────────┐   ┌──────────────┐   ┌────────────────┐   │
│  │  BaseModel   │   │   Student    │   │     Course     │   │
│  │  (abstract)  │◄──│  Name        │   │  Title         │   │
│  │  Id: int     │   │  Enrolled    │   │                │   │
│  └──────────────┘   │  Course      │   └────────────────┘   │
│                     └──────────────┘                        │
└─────────────────────────────────────────────────────────────┘
```

---

## 📂 Layer Breakdown

### 🟡 Layer 1 — Model / Entity

> **Namespace:** `Repository_project_by_atikur_rahman.model_or_entity`

This layer defines the data contracts. All entities inherit from `BaseModel` to guarantee the presence of an `Id` property — which is the key used by the repository for lookups, updates, and deletes.

---

#### `BaseModel` — Abstract Root Entity
> `model or entity/Basemodel.cs`

```csharp
public abstract class BaseModel
{
    public int Id { get; set; }
}
```

The shared base for all entities. Declaring it `abstract` ensures it can never be instantiated directly — it exists only as a contract. The generic constraint `where T : BaseModel` in the repository layer enforces that only proper entities can be managed.

---

#### `Student` — Concrete Entity
> `model or entity/Student.cs`

```csharp
public class Student : BaseModel
{
    public string Name { get; set; }
    public Course EnrolledCourse { get; set; }
}
```

Represents a student in the system. Holds a **navigation property** (`EnrolledCourse`) linking each student to their enrolled course — mirroring how Entity Framework models a one-to-one relationship.

---

#### `Course` — Concrete Entity
> `model or entity/Course.cs`

```csharp
public class Course : BaseModel
{
    public string Title { get; set; }
}
```

Represents an enrolled course. Inherits `Id` from `BaseModel` and adds a `Title` property.

---

#### Entity Relationship

```
BaseModel (abstract)
    │
    ├── Student
    │     ├── Id          (from BaseModel)
    │     ├── Name
    │     └── EnrolledCourse ──────────► Course
    │                                        ├── Id   (from BaseModel)
    │                                        └── Title
    │
    └── Course
          ├── Id          (from BaseModel)
          └── Title
```

---

### 🔵 Layer 2 — Repository

> **Namespace:** `Repository_project_by_atikur_rahman.repository`

This layer is the heart of the pattern. It completely abstracts how data is stored and retrieved. The rest of the application never needs to know whether data comes from a `List<T>`, a SQL database, or a remote API — it only ever talks to `IRepository<T>`.

---

#### `IRepository<T>` — Generic Interface
> `repository/Irepository.cs`

```csharp
public interface IRepository<T> where T : BaseModel
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
}
```

Defines the **full CRUD contract** for any entity `T` that extends `BaseModel`. The generic constraint `where T : BaseModel` enforces that only domain entities with a guaranteed `Id` field can be managed through this interface.

| Method | Return | Description |
|---|---|---|
| `GetAll()` | `IEnumerable<T>` | Returns all stored entities |
| `GetById(int id)` | `T` | Returns a single entity by its ID, or `null` |
| `Insert(T entity)` | `void` | Adds a new entity to the store |
| `Update(T entity)` | `void` | Replaces an existing entity matched by `Id` |
| `Delete(int id)` | `void` | Removes an entity by its ID |

---

#### `GenericRepository<T>` — Concrete Implementation
> `repository/Genericrepository.cs`

```csharp
public class GenericRepository<T> : IRepository<T> where T : BaseModel
{
    private readonly List<T> _data = new List<T>();

    public IEnumerable<T> GetAll()  => _data;
    public T GetById(int id)        => _data.FirstOrDefault(x => x.Id == id);
    public void Insert(T entity)    => _data.Add(entity);

    public void Update(T entity)
    {
        var existing = GetById(entity.Id);
        if (existing != null)
        {
            var index = _data.IndexOf(existing);
            _data[index] = entity;
        }
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing != null) _data.Remove(existing);
    }
}
```

A single class that implements **all five CRUD operations** for any entity type. Key design decisions:

- `_data` is `private readonly` — the list is fully encapsulated and cannot be replaced from outside
- `GetById` uses LINQ `FirstOrDefault` — returns `null` safely if the ID does not exist
- `Update` finds the existing record by index and replaces it in-place — preserving list order
- `Delete` checks for existence before removal — prevents exceptions on invalid IDs
- Because this is generic, `new GenericRepository<Course>()` would work just as well without writing any new code

---

### 🟢 Layer 3 — Service Layer

> **Namespace:** `Repository_project_by_atikur_rahman.service_layer`

> **File:** `implement/Studentservice.cs`

The service layer sits between the entry point and the repository. It holds the **business logic** — what to do before or after data operations — and wraps each repository call with meaningful, domain-specific behavior.

```csharp
public class StudentService
{
    private readonly IRepository<Student> _repo;

    // Constructor Injection — depends on the abstraction, not the implementation
    public StudentService(IRepository<Student> repo) { _repo = repo; }

    public void Register(Student s)
    {
        _repo.Insert(s);
        Console.WriteLine($"{s.Name.ToLower()} enrolled in {s.EnrolledCourse.Title.ToLower()}");
    }

    public void UpdateInfo(Student s)
    {
        _repo.Update(s);
        Console.WriteLine($"{s.Name.ToLower()} updated successfully");
    }

    public void DeleteStudent(int id)
    {
        var s = _repo.GetById(id);
        if (s != null)
        {
            string name = s.Name;
            _repo.Delete(id);
            Console.WriteLine($"{name.ToLower()} removed successfully");
        }
    }

    public void ShowAll()
    {
        foreach (var s in _repo.GetAll())
            Console.WriteLine($"id {s.Id} name {s.Name.ToLower()} coursename {s.EnrolledCourse.Title.ToLower()}");
    }
}
```

#### Service Method Reference

| Method | Repository Call | Business Logic Added |
|---|---|---|
| `Register(Student)` | `Insert()` | Logs enrollment confirmation with course name |
| `UpdateInfo(Student)` | `Update()` | Logs update success message |
| `DeleteStudent(int id)` | `GetById()` + `Delete()` | Fetches name before deletion for the log message; guards against missing IDs |
| `ShowAll()` | `GetAll()` | Iterates and formats each student record for display |

> **Key principle:** `StudentService` depends on `IRepository<Student>` — the **interface**, not `GenericRepository<Student>`. This means the storage implementation can be swapped (e.g., to a database-backed repository) without touching a single line of service code.

---

### ▶ Entry Point — `Program.cs`

> **Namespace:** `Repository_project_by_atikur_rahman`

The entry point wires all three layers together and runs a complete CRUD lifecycle:

```csharp
// 1. Compose the dependency graph manually (manual DI)
var studentRepo    = new GenericRepository<Student>();
var studentService = new StudentService(studentRepo);

// 2. INSERT — Register 10 students with courses
studentService.Register(new Student {
    Id = 1, Name = "Atikur Rahman",
    EnrolledCourse = new Course { Title = "C#" }
});
// ... 9 more students

// 3. UPDATE — Change course for 3 students
studentService.UpdateInfo(new Student {
    Id = 1, Name = "Atikur Rahman",
    EnrolledCourse = new Course { Title = "MVC.CORE" }
});
// ... 2 more updates

// 4. DELETE — Remove 3 students by ID
studentService.DeleteStudent(3);
studentService.DeleteStudent(4);
studentService.DeleteStudent(5);

// 5. READ ALL — Display remaining students
studentService.ShowAll();
```

**Students registered (IDs 1–10):**

| ID | Name | Initial Course |
|---|---|---|
| 1 | Atikur Rahman | C# |
| 2 | Sabbir Ahmed | Java |
| 3 | Nusrat Jahan | Python |
| 4 | Tanvir Hossain | SQL |
| 5 | Fahmida Akter | ASP.NET |
| 6 | Ariful Islam | React |
| 7 | Sadia Afrin | Angular |
| 8 | Rakibul Hasan | Cloud |
| 9 | Mitu Sarkar | Security |
| 10 | Kamrul Islam | Networking |

**Updates applied:**

| ID | Name Updated | New Course |
|---|---|---|
| 1 | Atikur Rahman | MVC.CORE |
| 2 | Saikat Khan | LLM |
| 3 | Hasibur Rahman | AI Learning |

**Deletions applied:** IDs 3, 4, 5

**Remaining after all operations:** IDs 1, 2, 6, 7, 8, 9, 10

---

## 🧠 Design Patterns & Concepts

| Pattern / Concept | Where Applied | Benefit |
|---|---|---|
| **Repository Pattern** | `IRepository<T>` + `GenericRepository<T>` | Decouples data access from business logic; swappable storage backend |
| **Generic Repository** | `GenericRepository<T> where T : BaseModel` | One class handles all entity types — no duplicate CRUD code |
| **Generic Constraint** | `where T : BaseModel` | Compile-time guarantee that only valid entities enter the repository |
| **Dependency Injection** | `StudentService(IRepository<Student> repo)` | Service depends on abstraction; implementation injected from outside |
| **Interface Segregation** | `IRepository<T>` defines only what's needed | Clean contract; easy to mock for unit testing |
| **Separation of Concerns** | 3-layer folder structure | Each layer has one job; changes in one layer don't ripple through others |
| **Abstraction** | `BaseModel` is `abstract` | Enforces `Id` on all entities without allowing direct instantiation |
| **Encapsulation** | `private readonly List<T> _data` | Internal storage is fully hidden from all consumers |
| **LINQ** | `FirstOrDefault`, `IndexOf` in repository | Concise, readable data queries over the in-memory collection |
| **Expression-bodied Members** | `GetAll()`, `GetById()`, `Insert()` | Single-line methods for simple operations — clean and readable |
| **Null Guard** | `if (existing != null)` in Update/Delete | Prevents runtime exceptions on stale or invalid IDs |

---

## 🔄 Data Flow Diagram

```
Program.cs
    │
    │  new GenericRepository<Student>()
    │  new StudentService(studentRepo)
    │
    ▼
StudentService
    │
    │  Register(student)      ──► IRepository.Insert(entity)
    │  UpdateInfo(student)    ──► IRepository.Update(entity)
    │  DeleteStudent(id)      ──► IRepository.GetById(id)
    │                              IRepository.Delete(id)
    │  ShowAll()              ──► IRepository.GetAll()
    │
    ▼
GenericRepository<Student>
    │
    │  private List<Student> _data
    │
    ├── Insert  → _data.Add(entity)
    ├── GetAll  → return _data
    ├── GetById → _data.FirstOrDefault(x => x.Id == id)
    ├── Update  → _data[index] = entity
    └── Delete  → _data.Remove(existing)
```

---

## 📁 Project Structure

```
c# repository project/
│
├── 1294236.slnx                                    # Visual Studio solution file
│
└── Repository project by atikur rahman/
    │
    ├── Program.cs                                  # Entry point — wires DI, runs CRUD
    ├── Repository project by atikur rahman.csproj
    │
    ├── model or entity/                            # Layer 1 — Domain Models
    │   ├── Basemodel.cs                            # Abstract base with Id
    │   ├── Student.cs                              # Student entity
    │   └── Course.cs                               # Course entity
    │
    ├── repository/                                 # Layer 2 — Data Access
    │   ├── Irepository.cs                          # Generic CRUD interface
    │   └── Genericrepository.cs                    # In-memory List<T> implementation
    │
    ├── implement/                                  # Layer 3 — Business Logic
    │   └── Studentservice.cs                       # Student-specific service
    │
    └── Properties/
        └── AssemblyInfo.cs
```

---

## 🚀 Getting Started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community edition or higher)
- .NET Framework 4.7.2 workload installed

### Run in Visual Studio

```
1. Open Visual Studio 2022
2. File → Open → Project/Solution
3. Select  1294236.slnx
4. Press F5  or click ▶ Start
```

### Run via .NET CLI

```bash
# Navigate to the project folder
cd "Repository project by atikur rahman"

# Build and run
dotnet run
```

---

## 🖥️ Expected Output

```
==========insert student===============
atikur rahman enrolled in c#
sabbir ahmed enrolled in java
nusrat jahan enrolled in python
tanvir hossain enrolled in sql
fahmida akter enrolled in asp.net
ariful islam enrolled in react
sadia afrin enrolled in angular
rakibul hasan enrolled in cloud
mitu sarkar enrolled in security
kamrul islam enrolled in networking

========Update Student course============
atikur rahman updated successfully
saikat khan updated successfully
hasibur rahman updated successfully

=========Delete student===============
hasibur rahman removed successfully
tanvir hossain removed successfully
fahmida akter removed successfully

=======SHow Student with course=========
id 1 name atikur rahman coursename  mvc.core
id 2 name saikat khan coursename llm
id 6 name ariful islam coursename react
id 7 name sadia afrin coursename angular
id 8 name rakibul hasan coursename cloud
id 9 name mitu sarkar coursename security
id 10 name kamrul islam coursename networking
```

---

## 👤 Author

| Field | Details |
|---|---|
| **Name** | Atikur Rahman |
| **Student ID** | 1294236 |
| **Batch ID** | WADA/PNTL-M/69/01 |
| **Programme** | ISDB-BISEW IT Scholarship Programme — Round 69 |
| **Email** | dotnetdeveloperatikur@gmail.com |
| **Specialization** | ASP.NET · Web API · C#.NET · MS SQL Server |

---

<p align="center">
  Built with ❤️ using <strong>C#</strong> and <strong>.NET Framework 4.7.2</strong>
</p>
