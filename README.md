Registrace a přihlášení uživatelů (Identity)

Role Admin / User

Správa knih (přidání, editace, mazání)

Správa autorů

Přiřazení knih uživatelům

Admin panel pro správu uživatelů

Automatické vytvoření role Admin

Automatické přiřazení role Admin uživateli „admin“

🛠 Použité technologie
ASP.NET Core 8

Entity Framework Core

SQL Server / LocalDB

ASP.NET Core Identity

Razor Pages / MVC

C#

📦 Instalace a spuštění
1️⃣ Naklonování repozitáře
Kód
git clone https://github.com/Ondrej456/Ctenarsky-denik.git
2️⃣ Migrace databáze
Ve Visual Studiu otevři Package Manager Console a spusť:

Kód
Update-Database
3️⃣ Spuštění aplikace
Kód
dotnet run
🔐 Role a administrátor
Aplikace automaticky vytvoří roli Admin a přiřadí ji uživateli s username admin, pokud existuje.

Heslo je bezpečně uloženo v databázi (hash), takže není viditelné.

📁 Struktura projektu
Kód
/Controllers
/Models
/Views
/Data
/wwwroot
📄 Licence
Projekt je licencován pod MIT licencí.

🤝 Autor
Vincent Havel
Junior ASP.NET Developer# Čtenářský deník
