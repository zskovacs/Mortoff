# MORTOFF - ASP.net fejlesztési feladat 

Ez a projekt egy interjúnek a fejlesztési részéhez készült. Az elkészült feladatot ezen a linken tudod megtekinteni: [https://mortoff.kovacs.id/](https://mortoff.kovacs.id/)

## Feladat leírás

A feladat egy olyan ASP.net .net framework vagy ASP.net core alkalmazás fejlesztése, ami részvények árfolyamának alakulását tudja bemutatni. 

## A megvalósítással kapcsolatos elvárások

### Feladat leadásának módja

* A feladat forrása publikus Github Repo-n keresztül legyen elérhető. Emailben ennek a reponak az URL-jét várjuk visszaküldeni.
* Lehetőleg több lépésben, beszédes commit üzenetekkel kerüljenek be a források, hogy követhető, érthető legyen a fejlesztési folyamat.

### Az elkészült alkalmazással kapcsolatos elvárások

* Az alkalmazást asp.net MVC vagy asp.net core MVC alkalmazásként kell elkészíteni
* Az oldal megjelenéséhez a Bootstrap CSS eszköztárát használjuk
* Az adatok perzisztens tárolásához használhatunk SQL LocalDB-t vagy SQLite-t is
* Az elkészült alkalmazásnak le kell fordulnia és hiba nélkül el kell indulnia
* Indítás után legyen elérhető és működőképes
* Az elkészült kód legyen jól olvasható, jól tesztelhető, tiszta kód.
* Plusz pont, ha a CSV feldolgozó logikára unit teszt is készül 


## Install

1. Telepítsd a legújabb [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
2. Telepítsd a legújabb [Node.js](https://nodejs.org/en/)
3. Navigálj a `src/WebUI/ClientApp` könyvtárba és futtasd az `npm install` paranmcsot, ezzel telepíted a klienshez szükséges csomagokat
4. Navigálj a `src/WebUI/ClientApp` könyvtárba és futtasd az `npm start` parancsot, ami elindítja az Angular klienst
5. Navigálj a `src/WebUI` könyvtárba és futtasd a `dotnet run` parancsot, ami elindítja a ASP.NET Core MVC backendet

### Alternatív futtatás

Lehetősége van docker containerben futtatni az alkalmazást. Ehhez a következő parancsot futtasd:

`docker run --rm -p 5000:80 --name mortoff zsovacs/mortoffapp`

## Technológiák

* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [Dapper](https://github.com/DapperLib/Dapper)
* [Angular 13](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [CSVHelper](https://github.com/JoshClose/CsvHelper)
* [Docker](https://www.docker.com/)

