# Reserve-iT

----------------------------------- German version below -----------------------------------


Reserve-iT is a WPF application for managing hotel bookings. 
The application allows users to book rooms, leave reviews, and manage bookings. 
Administrators have additional functions for managing bookings and reviews.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Dependencies](#dependencies)

## Features

- **Room Booking**: Users can book rooms based on date, category, room type and view all details.
- **Reviews**: Users can leave reviews for their bookings.
- **Review Management**: Administrators can approve or delete reviews.
- **Booking Management**: Administrators can cancel bookings.

## Installation

1. **Prerequisites**:
   - .NET 9 SDK
   - Visual Studio 2022

2. **Clone the repository**:

3. **Open the project**:
   - Open the `Reserve-iT.sln` file in Visual Studio 2022.

4. **Restore dependencies**:
   - Visual Studio will automatically restore the dependencies when the project is opened.

5. **Set up the database**:
   - Ensure a MySQL database is set up and the connection string in `DatabaseService.cs` is correctly configured.

## Usage

1. **Start the application**:
   - Press `F5` in Visual Studio to start the application.

2. **Book a room**:
   - Select the date, category, and room type, then click "Check Availability".
   - If a room is available, click "Proceed to Order" and enter the payment details.

3. **Leave reviews**:
   - Navigate to the review page, enter the order number and review text, and click "Submit Review".

4. **Administrator functions**:
   - Log in as an administrator by entering the admin password and clicking "Login".
   - Navigate to the admin page to view or delete bookings and manage reviews.

## Dependencies

- [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- [CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm/)
- [MySql.Data](https://www.nuget.org/packages/MySql.Data/)

--------------------------------------------------------------------------------------------

# Reserve-iT

Reserve-iT ist eine WPF-Anwendung zur Verwaltung von Hotelbuchungen. 
Die Anwendung erm�glicht es Benutzern, Zimmer zu buchen, Bewertungen abzugeben und Buchungen zu verwalten. 
Administratoren haben zus�tzliche Funktionen zur Verwaltung von Buchungen und Bewertungen.

## Inhaltsverzeichnis

- [Funktionen](#funktionen)
- [Installation](#installation)
- [Verwendung](#verwendung)
- [Abh�ngigkeiten](#abh�ngigkeiten)

## Funktionen

- **Zimmerbuchung**: Benutzer k�nnen Zimmer basierend auf Datum, Kategorie und Zimmerart buchen und alle Details anzeigen lassen.
- **Bewertungen**: Benutzer k�nnen Bewertungen f�r ihre Buchungen abgeben.
- **Bewertungsverwaltung**: Administratoren k�nnen Bewertungen freigeben oder l�schen.
- **Buchungsverwaltung**: Administratoren k�nnen Buchungen stornieren.

## Installation

1. **Voraussetzungen**:
   - .NET 9 SDK
   - Visual Studio 2022

2. **Repository klonen**:

3. **Projekt �ffnen**:
   - �ffne die `Reserve-iT.sln`-Datei in Visual Studio 2022.

4. **Abh�ngigkeiten wiederherstellen**:
   - Visual Studio wird automatisch die Abh�ngigkeiten wiederherstellen, wenn das Projekt ge�ffnet wird.

5. **Datenbank einrichten**:
   - Stelle sicher, dass eine MySQL-Datenbank eingerichtet ist und die Verbindungszeichenfolge in `DatabaseService.cs` korrekt konfiguriert ist.

## Verwendung

1. **Anwendung starten**:
   - Dr�cke `F5` in Visual Studio, um die Anwendung zu starten.

2. **Zimmer buchen**:
   - W�hle das Datum, die Kategorie und die Zimmerart aus und klicke auf "Verf�gbarkeit pr�fen".
   - Wenn ein Zimmer verf�gbar ist, klicke auf "Weiter zur Bestellung" und gib die Zahlungsdetails ein.

3. **Bewertungen abgeben**:
   - Navigiere zur Bewertungsseite, gib die Auftragsnummer und die Bewertung ein und klicke auf "Bewerten".

4. **Administratorfunktionen**:
   - Melde dich als Administrator an, indem du das Administratorpasswort eingibst und auf "Login" klickst.
   - Navigiere zur Verwaltungsseite, um Buchungen anzuzeigen oder zu l�schen und Bewertungen zu verwalten.

## Abh�ngigkeiten

- [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- [CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm/)
- [MySql.Data](https://www.nuget.org/packages/MySql.Data/)

   