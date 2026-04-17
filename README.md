# DemosWinUI
## 1. SQLite

In deze folder staat een eenvoudige WinUI 3 applicatie die gebruikmaakt van een lokale SQLite database.
De implementatie is gebaseerd op de volgende [Microsoft-tutorial](https://learn.microsoft.com/en-us/windows/apps/develop/data-access/sqlite-data-access).

### Demo
Bij het starten van de applicatie wordt automatisch een lokale SQLite‑database aangemaakt, indien deze nog niet bestaat. Deze database wordt opgeslagen in de app‑specifieke lokale folder. Via de gebruikersinterface kunnen vervolgens eenvoudige acties worden uitgevoerd om data te lezen uit en te schrijven naar de database.

### Risico's
Voor deze PoC zijn geen technische risico’s geïdentificeerd.

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
Er bestaan geen automatische migratietools die een volledige UWP‑app kunnen converteren naar WinUI 3. In de [officiële documentatie](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw) beschrijft Microsoft wel een migratie‑aanpak, maar dit betreft een volledig handmatig proces. De migratie bestaat voornamelijk uit het opnieuw opzetten van het project en het stap voor stap overzetten en aanpassen van bestaande code.

In de documentatie wordt de [.NET Upgrade Assistant](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/upgrade-assistant) genoemd om te helpen bij de migratie, maar deze tool is inmiddels deprecated. Als alternatief suggereert Microsoft het gebruik van een [Copilot‑assistant](https://learn.microsoft.com/en-us/dotnet/core/porting/github-copilot-app-modernization/overview). Deze kan ondersteunen bij het aanpassen van code en het herkennen van API‑wijzigingen, maar voert geen automatische migratie uit.


Daarnaast zijn niet alle UWP‑features beschikbaar in WinUI 3. Microsoft [documenteert](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/what-is-supported) expliciet welke onderdelen wel en niet ondersteund worden binnen de Windows App SDK. Dit betekent dat bepaalde functionaliteiten herschreven of vervangen moeten worden.

Microsoft beschrijft de migratie globaal in de volgende stappen:
1. Maak een nieuwe WinUI 3 packaged desktop applicatie
2. Kopieer XAML/UI code
   - Vaak alleen namespace-wijzigingen  
     (`Windows.UI.*` → `Microsoft.UI.*`)
3. Kopieer applicatielogica
   - Sommige API’s vereisen aanpassing (zoals Popups, Pickers en SecondaryTiles)

### Demo
Voor de migratie heb ik een bestaand UWP‑project gebruikt, dat ik heb gecloned uit [deze GitHub‑repository](https://github.com/XamlBrewer/UWP-MVVM-Toolkit-Sample).
De migratie heb ik uitgevoerd aan de hand van de door Microsoft beschreven stappen:
* Nieuw WinUI 3 packaged desktop project aangemaakt
* Dezelfde mappenstructuur aangehouden
* Bestanden uit het UWP‑project gekopieerd
* Namespaces aangepast. Specifiek:
   - Windows.UI → Microsoft.UI
   - Microsoft.Toolkit → CommunityToolkit 
* Specifiekere problemen opgespoord en opgelost met behulp van GitHub Copilot

Het eindresultaat is een werkende WinUI 3‑applicatie. De originele UWP‑app kon niet meer gerund worden, maar op basis van de beschrijving en screenshots in de repository ziet de applicatie er hetzelfde uit. Alle knoppen reageren zoals verwacht en leveren geen errors op.

### Risico's
* Niet alle UWP‑functionaliteit bestaat ook in WinUI 3, waardoor sommige onderdelen niet direct overgezet kunnen worden.
* Omdat de migratie volledig handmatig is, is er een grotere kans op fouten tijdens het aanpassen en overzetten van code.
* Sommige UWP‑API’s hebben geen één-op-één vervanging en moeten anders worden geïmplementeerd.


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
3. **Backend en client koppelen aan WNS**
Na ontvangst van bevestiging van Microsoft dat de Package Family Name is gekoppeld aan de Azure App Registration, kunnen backend en client worden toegevoegd:
   * De WinUI 3 applicatie registreert zich bij WNS en ontvangt een push notification channel.
   * De backend slaat deze channel‑URI op en gebruikt deze om push notifications naar de client te versturen via WNS.

### Demo
De WinUI 3 applicatie biedt een interface waarmee een SignalR‑verbinding met de backend kan worden opgezet. Zodra deze verbinding actief is, kunnen berichten vanuit de applicatie worden verstuurd. Deze berichten resulteren in het tonen van een Windows‑notificatie bij de gebruiker.

Daarnaast kunnen berichten ook vanuit de backend worden verstuurd via een API. Door met een externe tool voor het versturen van HTTP‑requests (zoals Postman) een bericht naar de API te sturen, ontvangt de gebruiker op dezelfde manier een notificatie in de WinUI 3 applicatie.

Hoewel SignalR wordt gebruikt voor het versturen van berichten tussen backend en client, wordt het tonen van de notificaties uitgevoerd met de standaard Windows‑notificatie‑functionaliteit.

### Risico's
Voor deze PoC zijn geen technische risico’s geïdentificeerd.


