# DemosWinUI

## 1. SQLite

In deze folder staat een eenvoudige WinUI 3 applicatie die gebruikmaakt van een lokale SQLite database.
De implementatie is gebaseerd op de volgende [Microsoft-tutorial](https://learn.microsoft.com/en-us/windows/apps/develop/data-access/sqlite-data-access).

### Demo

- Bij het starten van de applicatie wordt een database aangemaakt (indien deze nog niet bestaat)
- De database wordt opgeslagen in de app-specifieke lokale folder.
- Vanuit de UI kunnen eenvoudige acties worden uitgevoerd die data lezen of schrijven.

## 2. WCF

In deze folder staat een eenvoudige WCF Host-implementatie, gerealiseerd met behulp van de legacy WCF Service Host template. De host fungeert als backend‑service waarmee een WinUI 3 client berichten kan versturen en ontvangen.

Voor deze implementatie is gekozen voor .NET 8, omdat dit een stabiele Long-Term Support (LTS) versie van het .NET‑platform is. Hierdoor is de oplossing toekomstbestendig en geschikt voor langdurig onderhoud.

Door WCF te combineren met .NET 8 ontstaat een pragmatische oplossing waarin:

- bestaande WCF‑kennis en infrastructuur hergebruikt kan worden
- moderne .NET‑runtimevoordelen (prestaties, stabiliteit, tooling) benut worden
- communicatie met een moderne WinUI 3 client mogelijk blijft

### Demo

Bij het starten van deze demo dient de WCF host actief te zijn, zodat deze bereikbaar is voor de WinUI 3 client. Zodra de host draait, kan de client verbinding maken met de service.
Binnen de WinUI 3 applicatie kan vervolgens een nummer worden ingevoerd en verstuurd naar de WCF‑service. Dit nummer wordt via de WCF‑client verstuurd naar de host, waar het wordt ontvangen en verwerkt. Eventuele terugkoppeling vanuit de service kan daarna weer door de client worden afgehandeld en getoond.

### Risico's

1. .NET Framework 4.8 End-of-Life
   - Mainstream support van het framework eindigt in 2025 brengt risico met zich mee dat er geen toekomstige updates, security patches of support voor dit framework is.
2. WCF is "niet langer aanbevolen"
   - WCF wordt momenteel gewoon in stand gehouden en moderne packages zoals System.ServiceModel zorgen ervoor dat WCF compatibel blijft met nieuwere versies van .NET. Mochten de packages breaking changes krijgen dan werkt de integratie niet meer.
3. Multi-framework coupling
   - Omdat er gebruik wordt gemaakt van versies .NET4.8 voor de WCF host en .NET8 voor de WinUI client wordt debuggen en deployment lastiger gemaakt.

## 3. Migratie

Er bestaan geen automatische migratietools die een volledige UWP-app converteren naar WinUI 3. In [officiële documentatie](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw) beschrijft Microsoft wel een migratie‑aanpak, maar dit betreft een handmatig proces.

In de documentatie wordt de [.NET Upgrade Assistant tool](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/upgrade-assistant) genoemd om te assisteren met migratie, maar deze is deprecated. In plaats daarvan wordt het gebruik van een [Copilot‑assistant](https://learn.microsoft.com/en-us/dotnet/core/porting/github-copilot-app-modernization/overview) gesuggereerd, wat ook ondersteuning biedt bij code‑aanpassingen, maar geen automatische migratie uitvoert.

Microsoft beschrijft de volgende stappen:

1. Maak een nieuwe WinUI 3 packaged desktop applicatie
2. Kopieer XAML/UI code
   - Vaak alleen namespace-wijzigingen  
     (`Windows.UI.*` → `Microsoft.UI.*`)
3. Kopieer applicatielogica
   - Sommige API’s vereisen aanpassing (zoals Popups, Pickers en SecondaryTiles)

### Demo

TODO

## 4. Notificaties

In deze folder staat een implementatie voor het versturen van berichten van de backend naar de WinUI 3 applicatie via **SignalR**.

In plaats van **Windows Push Notification Services (WNS)** is SignalR gebruikt, omdat het gebruik van WNS een **Azure Active Directory App Registration** en bijbehorende configuratie vereist die binnen onze huidige Azure‑omgeving niet aangevraagd of aangepast kon worden. SignalR is daarom voor nu gebruikt als technisch alternatief om real‑time communicatie tussen backend en client aan te tonen.

### Vervolgstappen

Met meer tijd en toegang tot de juiste Azure‑configuratie kan dezelfde functionaliteit worden gerealiseerd met WNS. Microsoft beschrijft dit expliciet voor WinUI 3 in de [officiële documentatie](https://learn.microsoft.com/en-us/windows/apps/develop/notifications/push-notifications/push-quickstart).

1. **Azure Active Directory (AAD) App Registration**
   - In de Azure‑portal moet een nieuwe **App Registration** worden aangemaakt. Hiervoor is het belangrijk dat de subscription waarmee wordt gewerkt wordt toegang heeft tot **Azure Active Directory (Microsoft Entra ID)** en dat de gebruiker rechten heeft om **App Registrations** aan te maken.

2. **Package Family Name (PFN) koppelen aan Azure App**
   - Omdat het gaat om een packaged WinUI 3 applicatie, moet de **Package Family Name (PFN)** van de app worden gekoppeld aan de Azure App Registration.
   - Deze koppeling kan niet zelf gedaan worden en moet worden aangevraagd bij Microsoft via: `Win_App_SDK_Push@microsoft.com`
   - Dit is een handmatige stap aan Microsoft‑zijde en heeft een wachttijd van ongeveer 1 week.

### Demo

- De WinUI 3 applicatie maakt een SignalR-verbinding met de backend via de 'verbind' button.
- Vanuit de backend worden berichten verstuurd.
- De client ontvangt deze berichten in real-time.
- TODO: uitleg via API
