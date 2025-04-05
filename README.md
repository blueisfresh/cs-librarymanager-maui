# Library Management System – .NET MAUI Version

This repository is part of a **multi-platform Library Management System** built in three different .NET frameworks:

- **WPF** – A desktop application using MVVM and a local SQL database.  
- **Blazor** – Provides a REST API and a web front end.  
- **.NET MAUI (this repo)** – A mobile version (Android-focused) that consumes the Blazor REST API.

All three versions share the same purpose: **managing books, students, and borrowing/returning operations** in a library. The .NET MAUI version extends these capabilities to a **mobile** platform, allowing you to manage your library on the go.

> **Why multiple versions?**  
> These projects were created during my apprenticeship to demonstrate how the same functionality can be implemented across desktop (WPF), web (Blazor), and mobile (.NET MAUI) platforms.

## Table of Contents

1. [Overview](#overview)  
2. [Core Features](#core-features)  
3. [Installation and Usage](#installation-and-usage)  
   - [REST API Setup](#rest-api-setup)  
   - [Build and Run on Android](#build-and-run-on-android)  

## Overview

The **.NET MAUI** version of the Library Management System provides:

- **Mobile UI** built with .NET MAUI, targeting **Android** primarily (though .NET MAUI can also target iOS, Windows, and macOS).  
- **MVVM Architecture** to maintain separation of concerns between UI and logic.  
- **Consumption of a REST API**: The app communicates with the Blazor-based backend ([CS Library Manager (Blazor)](#related-projects)) to fetch and manage data (books, students, borrow records).

### Highlights

- **On-the-Go Library Management**: Conveniently track books, students, and borrowed items from your Android phone or tablet.  
- **API Service Class**: A singleton class handles API calls (GET, POST, PUT, DELETE) against the Blazor REST API.  
- **MVVM**: View Models bind to .NET MAUI UI pages, ensuring a clean separation of UI and logic.

## Core Features

1. **Add/Edit/Delete Books**: Perform CRUD operations for library books through the REST API.  
2. **Add/Edit/Delete Students**: Manage student records with direct REST API calls.  
3. **Borrow/Return Tracking**: Log or update which student borrowed which book, and when it’s returned.  
4. **Statistics (Optional)**: If exposed by the REST API (e.g., top borrowed books), you can display them on mobile.  

> **Important**: Since this version relies on the **Blazor REST API**, any functionality (such as import/export) must be supported by that API. 

## Installation and Usage

### REST API Setup

1. **Ensure the Blazor REST API is Running**  
   - Clone and run your [Blazor project](https://github.com/blueisfresh/cs-librarymanager-blazor).  
   - Confirm that the API endpoints are reachable (e.g., via `https://localhost:5001/api/books`).

2. **Configure the REST API URL**  
   - In your MAUI project, locate the **API Service** configuration (often in an `ApiService.cs` or `AppSettings.json`-like file).  
   - Update the **base URL** to match your Blazor API address (e.g., `https://localhost:5001` or the publicly hosted equivalent).

> **Tip**: If testing from a mobile emulator, you might need to adjust the localhost references (e.g., `10.0.2.2` for Android emulators).

### Build and Run on Android

1. **Clone the Repository**  
   ```bash
   git clone https://github.com/blueisfresh/cs-librarymanager-maui.git
   
   cd cs-librarymanager-maui
   ```

2. **Open the Solution in Visual Studio**  
   - Double-click the `.sln` file to launch Visual Studio (ensure you have the **.NET MAUI workload** installed).

3. **Select the Android Target**  
   - In the **Visual Studio toolbar**, choose the **Android Emulator** or a connected Android device.  

4. **Restore NuGet Packages**  
   - Usually automatic; if needed, use `dotnet restore`.

5. **Build and Deploy**  
   - **F5** (Debug) or **Ctrl+F5** (Without Debug). Visual Studio will build the .NET MAUI app and deploy it to your selected emulator or device.

6. **Run the App**  
   - Once deployed, the app will launch on your emulator/device.  
   - Verify that your phone/emulator has network access to the Blazor API (e.g., use the correct IP/port, or connect to the same network).
