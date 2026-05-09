# C# — Fundamentals & Object-Oriented Programming with Generics

<p align="center">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET%20Framework-4.7.2-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Visual%20Studio-2022-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white" />
  <img src="https://img.shields.io/badge/OOP-Generics-blue?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Projects-2-orange?style=for-the-badge" />
</p>

<p align="center">
  A two-part Visual Studio solution covering <strong>C# language fundamentals</strong> and a fully structured <strong>Object-Oriented Programming system with Generics</strong> — built around a real-world vehicle hierarchy domain.
</p>

---

## 📋 Table of Contents

- [Solution Overview](#-solution-overview)
- [Project 1 — Basic C#](#-project-1--basic-c)
- [Project 2 — OOP with Generics](#-project-2--oop-with-generics)
  - [Class Hierarchy Diagram](#-class-hierarchy-diagram)
  - [Class & Interface Reference](#-class--interface-reference)
- [OOP Concepts Demonstrated](#-oop-concepts-demonstrated)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Author](#-author)

---

## 🌟 Solution Overview

This solution (`solutionbyatikur.slnx`) contains two independent C# console projects that together form a complete learning arc — starting from the ground-level syntax of C# and building up to a multi-layered, interface-driven, generic OOP architecture.

| Project | Namespace | Focus |
|---|---|---|
| `basic c#` | `basic_c_` | C# syntax, data types, control flow, exceptions |
| `oopwithgenric` | `oopwithgenric.oopandgenric` | OOP pillars, interfaces, generics, sealed classes |

**Platform:** .NET Framework 4.7.2 · **IDE:** Visual Studio 2022 · **Language:** C# · **Output:** Console Application

---

## 📘 Project 1 — Basic C\#

> **Path:** `basic c#/Program.cs`

This project is a focused walkthrough of the core C# language features, all demonstrated inside `Main()` with clearly commented sections.

---

### A · Control Flow

Covers every branching and looping construct available in C#.

```csharp
// if / else — age-based voting eligibility
int age = 18;
if (age >= 18) Console.WriteLine("you can vote");
else           Console.WriteLine("you cannot vote");

// switch — boolean mode selection
bool mode = true;
switch (mode) {
    case true:  Console.WriteLine("darkmode on");  break;
    case false: Console.WriteLine("lightmode on"); break;
    default:    Console.WriteLine("system mode on"); break;
}

// while, do-while, for — loop variants
while (x <= 10)  { Console.WriteLine(x); x++; }
do { Console.WriteLine(y); y++; } while (y <= 10);
for (int i = 0; i < 10; i++) Console.WriteLine(i);
```

**Concepts:** `if/else` · `switch/case/default` · `while` · `do-while` · `for`

---

### B · Logical Operators

Demonstrates combining conditions with `&&` (AND) and `||` (OR).

```csharp
// AND — both conditions must be true
if (userage >= 18 && member) Console.WriteLine("you can enter");

// OR — at least one condition must be true
if (userage < 18 || memberrelative) Console.WriteLine("you also can enter");
```

**Concepts:** `&&` (logical AND) · `||` (logical OR) · compound boolean expressions

---

### C · Data Types & Operators

Demonstrates every primitive C# value type alongside arithmetic and increment operators.

```csharp
string  a = "atikur";           // Unicode text
char    b = 'a';                // Single character
int     c = 155 + 544;          // 32-bit integer
short   d = 4 * 5;              // 16-bit integer
long    e = 545454L - 454545L;  // 64-bit integer
bool    f = true;               // Boolean
double  g = 455.5 / 55.5;       // 64-bit floating point
float   h = 5454.555F % 5454.55F; // 32-bit floating point
decimal i = 545454.455M;        // 128-bit high-precision decimal

int count = 1;
count++;   // post-increment
++count;   // pre-increment
```

**Concepts:** `string` · `char` · `int` · `short` · `long` · `bool` · `double` · `float` · `decimal` · arithmetic operators (`+`, `-`, `*`, `/`, `%`) · increment (`++`)

---

### D · Methods — Named & Default Parameters

Defines and calls a static method using both named arguments and optional default parameter values.

```csharp
// Calling with named arguments
amerinfo(name: "atikur", address: "pabna");

// Method definition with required and optional (default) parameters
public static void amerinfo(
    string name,
    string address,
    string fname = "mojaharul",   // optional
    string mname = "sufiya")      // optional
{
    int age = 25;
    Console.WriteLine($"{name} {age} {address} {fname} {mname}");
}
```

**Concepts:** `static` methods · named arguments · default parameter values · string interpolation (`$"..."`)

---

### E · Checked & Unchecked Integer Overflow

Shows how C# handles arithmetic overflow in both safe and unsafe contexts.

```csharp
// checked — throws OverflowException when integer wraps
checked {
    try {
        int ry = int.MaxValue;
        ry++;  // throws System.OverflowException
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}

// unchecked — silently wraps around (int.MaxValue + 1 = int.MinValue)
unchecked {
    int ty = int.MaxValue;
    ty++;
    Console.WriteLine(ty);  // -2147483648
}
```

**Concepts:** `checked` context · `unchecked` context · `int.MaxValue` · overflow behavior

---

### F · Exception Handling

Demonstrates structured exception handling with specific exception types and a `finally` cleanup block.

```csharp
try {
    int m = 45, n = 0;
    int o = m / n;              // throws DivideByZeroException
}
catch (DivideByZeroException de) {
    Console.WriteLine(de.Message);
}
finally {
    Console.WriteLine("yes i catch the exception"); // always runs
}
```

**Concepts:** `try` · `catch` · `finally` · `DivideByZeroException` · typed exception catching

---

## 📗 Project 2 — OOP with Generics

> **Path:** `oopwithgenric/oopandgenric/`

This project implements a **Vehicle Management System** using a rich OOP architecture. It demonstrates all four pillars of OOP — encapsulation, abstraction, inheritance, and polymorphism — combined with generic interfaces and type-constrained generics.

The system models two families of vehicles:
- **Two-Wheeler** → `Motorcycle` (e.g., Pulsar 150)
- **Four-Wheeler** → `Car` (e.g., Toyota Corolla)

---

### 🗂 Class Hierarchy Diagram

```
                    ┌─────────────────────┐
                    │   Vehicle (abstract) │  ← Base abstract class
                    │─────────────────────│
                    │ + modelno            │
                    │ + yearmake           │
                    │ + numofgear          │
                    │ + enginecapcc        │
                    │ + vehicletype        │
                    │ + detail() abstract  │
                    └─────────┬───────────┘
                              │
              ┌───────────────┴────────────────┐
              │                                │
   ┌──────────▼──────────┐        ┌────────────▼────────────┐
   │   Twowheeler        │        │      Fourwheeler         │
   │─────────────────────│        │─────────────────────────│
   │ + stratm            │        │ + numofseat              │
   │ + mbph              │        │ + numofdoor              │
   │ + mnm               │        │ implements Iinteriordesign│
   │ + mileage           │        │ + addinteriordesign()    │
   │ + cooling           │        │ + getinteriordesign()    │
   │ + fbrake / rbrake   │        │ + detail() override      │
   │ implements          │        └────────────┬─────────────┘
   │   Iexteriordesign   │                     │
   │ + addexteriordesign │          ┌───────────▼──────────┐
   │ + getexteriordesign │          │   Car (sealed)        │
   │ + detail() override │          │──────────────────────│
   └──────────┬──────────┘          │  inherits Fourwheeler │
              │                     └──────────────────────┘
   ┌──────────▼──────────┐
   │  Motorcycle (sealed) │
   │─────────────────────│
   │  inherits Twowheeler │
   └──────────────────────┘


   Interfaces                Generic Implementations
   ─────────────────────     ──────────────────────────────────────
   Iexteriordesign           Igenric<T>     ──► Igenricimp<T>
   Iinteriordesign           Ivehicle<T>    ──► Ivehicleimp<T>
                             (where T : Vehicle — constrained)

   Enum
   ────────────────────────────────────
   Vehicletype { personal, familly, sports, racing }
```

---

### 📋 Class & Interface Reference

#### `Vehicle` — Abstract Base Class
> `Vehicle.cs`

The root of the entire hierarchy. Defines the shared data contract for all vehicles and mandates that every subclass provides its own `detail()` implementation.

| Member | Type | Description |
|---|---|---|
| `modelno` | `string` | Vehicle model name |
| `yearmake` | `int` | Manufacturing year |
| `numofgear` | `int` | Number of gears |
| `enginecapcc` | `int` | Engine capacity in CC |
| `vehicletype` | `Vehicletype` | Enum — personal / family / sports / racing |
| `detail()` | `abstract string` | Overridden by each subclass to format output |

---

#### `Twowheeler` — Concrete Intermediate Class
> `Twowheeler.cs` · Inherits `Vehicle` · Implements `Iexteriordesign`

Extends the base vehicle with motorcycle-specific properties. Implements the exterior design interface using an internal `List<string>` for dynamic feature storage.

| Member | Description |
|---|---|
| `stratm` | Start method (self/kick) |
| `mbph` | Max speed in BPH |
| `mnm` | Torque in Nm |
| `mileage` | Fuel efficiency |
| `cooling` | Cooling system type |
| `fbrake` / `rbrake` | Front and rear brake types |
| `addexteriordesign(params string[])` | Adds one or more exterior features |
| `getexteriordesign()` | Returns comma-joined exterior feature list |
| `detail()` | Overrides abstract base to format full spec string |

---

#### `Motorcycle` — Sealed Leaf Class
> `Motorcycle.cs` · Inherits `Twowheeler`

A `sealed` final class representing a specific motorcycle. Cannot be subclassed further. Delegates all logic to `Twowheeler` through constructor chaining.

```csharp
Motorcycle m = new Motorcycle(
    "pulsar 150", 2025, 5, 160,
    Vehicletype.personal, "self and kick",
    130, 120, 45, "air cooler", "disk", "disk"
);
m.addexteriordesign("vip horn", "comfort seat");
```

---

#### `Fourwheeler` — Concrete Intermediate Class
> `Fourwheeler.cs` · Inherits `Vehicle` · Implements `Iinteriordesign`

Extends the base vehicle with car-specific properties. Implements the interior design interface using an internal `List<string>`.

| Member | Description |
|---|---|
| `numofseat` | Number of seats |
| `numofdoor` | Number of doors |
| `addinteriordesign(params string[])` | Adds one or more interior features |
| `getinteriordesign()` | Returns comma-joined interior feature list |
| `detail()` | Overrides abstract base to format full spec string |

---

#### `Car` — Sealed Leaf Class
> `Car.cs` · Inherits `Fourwheeler`

A `sealed` final class representing a specific car. All behaviour is inherited from `Fourwheeler`.

```csharp
Car c = new Car(
    "toyota corolla", 2025, 5, 1200,
    Vehicletype.familly, 4, 4
);
c.addinteriordesign("wifi 8", "vip seat");
```

---

#### Interfaces

| Interface | File | Generic | Description |
|---|---|---|---|
| `Iexteriordesign` | `Iexteriordesign.cs` | No | Contract for exterior feature management on two-wheelers |
| `Iinteriordesign` | `Iinteriordesign.cs` | No | Contract for interior feature management on four-wheelers |
| `Igenric<T>` | `Igenric.cs` | Yes | Generic interface: `getdetail<T>(T obj)` — works on any type |
| `Ivehicle<T>` | `Ivehicle.cs` | Yes (constrained) | Generic interface: `getdetail<T>(T obj) where T : Vehicle` — restricted to vehicles only |

#### Generic Implementations

| Class | Implements | Constraint | Behaviour |
|---|---|---|---|
| `Igenricimp<T>` | `Igenric<T>` | None | Checks if `obj` is a `Vehicle` at runtime; calls `detail()` if so, otherwise returns `"not a vehicle"` |
| `Ivehicleimp<T>` | `Ivehicle<T>` | `where T : Vehicle` | Compile-time safety — directly calls `obj.detail()` with no runtime check needed |

#### `Vehicletype` — Enum
> `Vehicletype.cs`

```csharp
public enum Vehicletype {
    personal = 1,
    familly,
    sports,
    racing
}
```

---

## 🧠 OOP Concepts Demonstrated

| Concept | Where Applied |
|---|---|
| **Abstraction** | `Vehicle` is `abstract` — cannot be instantiated directly; forces subclasses to implement `detail()` |
| **Encapsulation** | Private `List<string>` fields for design features; exposed only through interface methods |
| **Inheritance** | `Twowheeler` → `Motorcycle` and `Fourwheeler` → `Car` both chain constructors via `base(...)` |
| **Polymorphism** | `detail()` is declared `abstract` in `Vehicle`, `override` in `Twowheeler` and `Fourwheeler` |
| **Interfaces** | `Iexteriordesign`, `Iinteriordesign`, `Igenric<T>`, `Ivehicle<T>` — separate contract from implementation |
| **Generics** | `Igenric<T>` and `Ivehicle<T>` allow type-safe reuse without duplication |
| **Generic Constraints** | `where T : Vehicle` in `Ivehicle<T>` enforces compile-time type safety |
| **Sealed Classes** | `Motorcycle` and `Car` are `sealed` — intentionally prevent further inheritance |
| **Enums** | `Vehicletype` classifies vehicles with named integer constants |
| **`params` keyword** | `addexteriordesign(params string[])` and `addinteriordesign(params string[])` accept variable-length arguments |
| **Auto-properties** | All model fields use `{ get; set; }` shorthand |
| **Constructor Chaining** | `base(...)` calls propagate initialization up the inheritance chain |
| **Runtime Type Checking** | `Igenricimp<T>` uses `is` and `as` for safe casting |
| **String Interpolation** | `detail()` overrides use `$"..."` for formatted output |

---

## 📁 Project Structure

```
c# oop project/
│
├── solutionbyatikur.slnx                  # Visual Studio solution file
│
├── basic c#/                              # Project 1 — C# Fundamentals
│   ├── Program.cs                         # All demos: types, control flow, exceptions
│   ├── basic c#.csproj
│   └── Properties/AssemblyInfo.cs
│
└── oopwithgenric/                         # Project 2 — OOP with Generics
    ├── Program.cs                         # Entry point — instantiates Motorcycle & Car
    ├── oopwithgenric.csproj
    ├── Properties/AssemblyInfo.cs
    │
    └── oopandgenric/                      # All OOP source files
        ├── Vehicle.cs                     # Abstract base class
        ├── Twowheeler.cs                  # Intermediate — 2-wheel vehicles
        ├── Motorcycle.cs                  # Sealed leaf — concrete motorcycle
        ├── Fourwheeler.cs                 # Intermediate — 4-wheel vehicles
        ├── Car.cs                         # Sealed leaf — concrete car
        ├── Vehicletype.cs                 # Enum — vehicle category
        ├── Iexteriordesign.cs             # Interface — exterior feature contract
        ├── Iinteriordesign.cs             # Interface — interior feature contract
        ├── Igenric.cs                     # Generic interface — unconstrained
        ├── Igenricimp.cs                  # Generic implementation — runtime type check
        ├── Ivehicle.cs                    # Generic interface — Vehicle-constrained
        └── Ivehicleimp.cs                 # Constrained generic implementation
```

---

## 🚀 Getting Started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community or higher)
- .NET Framework 4.7.2 (included with Visual Studio workloads)

### Running the Projects

**Option 1 — Visual Studio (Recommended)**

```
1. Open Visual Studio 2022
2. File → Open → Project/Solution
3. Select  solutionbyatikur.slnx
4. In Solution Explorer, right-click a project → Set as Startup Project
5. Press F5 or click ▶ Start to run
```

**Option 2 — .NET CLI**

```bash
# Clone or extract the project folder

# Run Project 1 — Basic C#
cd "basic c#"
dotnet run

# Run Project 2 — OOP with Generics
cd "../oopwithgenric"
dotnet run
```

### Expected Output — Project 2

```
pulsar 1502025160self and kickair coolerdiskdiskpersonal
vip horn, comfort seat

toyota corolla202551200familly
wifi 8, vip seat
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
