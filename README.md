# Sakan
Property Rental &amp; Co-Living Marketplace

---


## Conventions
 
1. **Menus only talk to the console and to services.** A menu never uses `AppDbContext` and never calls `SaveChanges`.
2. **Services take IDs, not entities.** A menu passes `clientId` and `ticketId`. Do not keep a `Ticket` object between actions.
3. **Failures use the Try pattern.** A method returns `bool` and gives the reason in `out string error`. The menu only shows it with `PrintError`.
4. **Services save their own changes.**
5. **There is one shared `AppDbContext`** for the whole run, created in `Program.cs` and passed to every service.
6. **Looking up a ticket returns `null` when it does not exist or is not yours.** The menu shows one generic "Ticket not found".
7. **Load the data the screen needs.** Comment authors and names are navigation properties, so include them:
```csharp
_db.Tickets
    .Include(t => t.Client)
    .Include(t => t.AssignedUser)
    .Include(t => t.Comments).ThenInclude(c => c.Author)
    .FirstOrDefault(t => t.Id == ticketId && t.ClientId == clientId);
```
 
8. **Never set `ticket.Status` directly.** Use `ITicketStatusService.TryTransition`. It enforces the order and the permissions, sets `ResolvedAt`, and saves.
9. **Never rewrite the comment rules.** Call `ICommentPermissionService.CanComment(ticket, user)`.
10. **Show tickets with `TicketDisplayHelper`.** It prints the table and details with the overdue marking, so overdue looks the same on every screen.
11. **Logout means `Show` returns.** Each menu loops until the user picks Logout, then returns to `WelcomeMenu`.

## Each person does three things:
 
1. Implement the interface in the service file. The constructor takes `AppDbContext` and any shared services you need.
2. Add your service to the constructor of your menu stub and fill in `Show`.
3. Edit only your own lines in `Program.cs`.
Example for the Client module in `Program.cs`:
 
```csharp
IClientService clientService = new ClientService(db, statusService, commentPermissionService);
 
List<IRoleMenu> menus = new List<IRoleMenu>
{
    new ClientMenu(ui, clientService, authService, ticketDisplay),
    new DispatcherMenu(ui),
    new AgentMenu(ui),
    new AdminMenu(ui)
};
```
 ---

 
## Project Structure

This document describes the project's folder structure, file responsibilities, and the role of each module.

## 1. Folders

| Folder | Responsibility | Why it exists |
| --- | --- | --- |
| `Domain/Entities` | Business objects: users, tickets, and comments | Holds data and relationships in one place, without console or database-connection code. |
| `Domain/Enums` | Fixed value sets: status, priority, department, and role | Prevents typos such as `"Closed "` and makes allowed values explicit. |
| `Interfaces/Auth` | Contracts for login/registration and password hashing | Lets menus and services depend on contracts rather than concrete classes. |
| `Interfaces/Tickets` | Contracts for shared ticket rules: status, comments, assignment, due dates, and auto-close | Defines rules used by several roles in one place. |
| `Interfaces/Roles` | One contract per role: Client, Dispatcher, Agent, and Admin | Lets each teammate code against a fixed contract without waiting for others. |
| `Services/Auth` | Implements login, registration, and password changes | Keeps account business rules separate from the console. |
| `Services/Tickets` | Implements shared ticket rules | Keeps status, comment, due-date, and auto-close logic in one place. |
| `Services/Roles` | Implements each role's operations, such as creating tickets, assigning tickets, resolving tickets, and deleting users | Separates each module's logic from its menu. |
| `Infrastructure/Data` | `AppDbContext`, seeder, and EF Core configurations | Keeps SQL Server-specific knowledge in one place. |
| `Infrastructure/Data/Configurations` | Table, key, index, and relationship settings per entity | Keeps database rules out of entity classes. |
| `Infrastructure/Migrations` | Generated database migration files | Lets teammates recreate the same database. |
| `Infrastructure/Security` | Password-hashing implementation | Allows the hashing algorithm to be swapped without changing services. |
| `Presentation/UI` | Console input and output: menus, tables, colors, and prompts | Keeps all `Console.*` calls in one place. |
| `Presentation/Menus` | Screens for each role, plus login and routing menus | Menus display information, collect input, and call services. |
| `Presentation/Helpers` | Shared ticket table and details printing | Keeps overdue-ticket marking consistent across screens. |

### Dependency Direction

- **Presentation** calls Interfaces.
- **Services** implement Interfaces and use the database.
- **Domain** depends on nothing.

## 2. Files

### Root

| File | What it does | Why it exists |
| --- | --- | --- |
| `Program.cs` | Applies migrations, seeds data, creates the shared `AppDbContext` and services, builds the menu list, and starts `WelcomeMenu`. | The composition root: the one place where everything is connected. |
| `README.md` | Documents setup, conventions, ownership, and business rules. | Helps teammates get started without needing to ask for instructions. |
| `HelpDeskTicketingSystem.csproj` | Contains project settings and EF Core packages. | Required by .NET. |

### `Domain/Enums`

| File | What it does |
| --- | --- |
| `Department.cs` | Defines Hardware, Software, Network, Billing, and GeneralQuestions. Used when a dispatcher takes a ticket personally. |
| `Role.cs` | Defines Client, Dispatcher, Agent, and Admin. |
| `TicketPriority.cs` | Defines Low, Medium, High, and Urgent; priority determines the due date. |
| `TicketStatus.cs` | Defines Open, Assigned, InProgress, Resolved, and Closed; these represent the ticket lifecycle. |

### `Domain/Entities`

| File | What it does | Why it exists |
| --- | --- | --- |
| `User.cs` | Abstract base class containing name, email, password hash, `MustChangePassword`, abstract `Role`, and owned tickets. | Stores fields shared by all four account types. |
| `Client.cs` | Adds the list of tickets submitted by the client. | Supports the one-to-many relationship between a client and their tickets. |
| `Agent.cs` | Adds `Department` and `LastAssignedAt`. | Department determines routing; `LastAssignedAt` breaks load-balancing ties. |
| `Dispatcher.cs` | Returns `Role.Dispatcher`. | Provides a distinct type for routing and permissions. |
| `Admin.cs` | Returns `Role.Admin`. | Provides a distinct type for administrator permissions. |
| `Ticket.cs` | Contains title, description, status, nullable priority and department, due date, resolution time, client, assigned user, comments, and `IsOverdue()`. | The central business object. Priority and department are nullable because a new ticket has neither. |
| `Comment.cs` | Contains text, time, ticket, and author. | Represents the conversation thread for a ticket. |

### Interfaces

| File | Contract |
| --- | --- |
| `IAuthService.cs` | Login, client registration, and forced or voluntary password changes. |
| `IPasswordHasher.cs` | `Hash` and `Verify`. |
| `ITicketStatusService.cs` | `CanTransition` (validation only) and `TryTransition` (validate, change, and save). |
| `ICommentPermissionService.cs` | `CanComment(ticket, user)`. |
| `IAssignmentService.cs` | `GetLeastBusyAgent(department)`. |
| `IDueDateCalculator.cs` | `Calculate(priority, from)`. |
| `IAutoCloseService.cs` | `CloseExpiredResolvedTickets()`. |
| `IClientService.cs` | Create tickets, view own tickets, comment, close tickets, and edit name/email. |
| `IDispatcherService.cs` | View open tickets, assign tickets to a department, take ownership, and work on owned tickets. |
| `IAgentService.cs` | View active/closed tickets, start progress, comment, and resolve tickets. |
| `IAdminService.cs` | Create agents/dispatchers, view/filter users and tickets, and delete users/tickets. |

### Services (Module 1)

| File | What it does | Why it exists |
| --- | --- | --- |
| `AuthService.cs` | Looks up users by case-insensitive email, verifies password hashes, rejects duplicate emails, and changes passwords. | Keeps account rules (doc 8) in one place. |
| `TicketStatusService.cs` | Allows only the next status in order, checks who may make each change, sets `ResolvedAt`, and saves changes. | Acts as the single gatekeeper for the ticket lifecycle (doc 3). |
| `CommentPermissionService.cs` | Clients can comment until a ticket is Closed; the owner can comment only after starting progress; nobody can comment on Closed tickets. | Reuses the same rules across three roles (doc 6.2). |
| `DueDateCalculator.cs` | Calculates due dates of 4, 24, 48, or 72 hours according to priority. | Implements the SLA rule (doc 5). |
| `AutoCloseService.cs` | Closes tickets that have been Resolved for at least 3 days, using `TicketStatusService`. | Implements the housekeeping rule (doc 9). |

### Infrastructure (Module 1)

| File | What it does | Why it exists |
| --- | --- | --- |
| `AppDbContext.cs` | Defines tables for users, agents, tickets, and comments; configures the connection string; loads entity configurations. | Provides the EF Core entry point. |
| `UserConfiguration.cs` | Uses one `Users` table for all four types with a `UserType` column, a unique email, and required fields. | Implements single-table inheritance and enforces unique emails. |
| `AgentConfiguration.cs` | Stores the agent's department as text. | `Department` belongs only to agents. |
| `TicketConfiguration.cs` | Configures required fields, enums as text, two user foreign keys with Restrict delete, and an index on status and assigned user. | Prevents deletion of linked accounts (doc 8.3) and speeds up workload counts. |
| `CommentConfiguration.cs` | Deleting a ticket deletes its comments; deleting an author is blocked. | Lets an admin delete a ticket cleanly. |
| `DbSeeder.cs` | Inserts 8 users, 7 tickets, and comments if the database is empty. | Provides test data covering every status, an overdue ticket, and an empty department. |
| `PasswordHasher.cs` | Uses SHA-256 hashing and verifies an attempt by hashing the supplied password. | Implements `IPasswordHasher`. |

### Presentation (Module 1)

| File | What it does | Why it exists |
| --- | --- | --- |
| `IConsoleUI.cs` / `ConsoleUI.cs` | Provides colored messages, menus, tables with row colors, validated input, yes/no confirmation, and pause functionality. | Provides a shared, injectable toolkit instead of relying on static methods. |
| `TicketDisplayHelper.cs` | Prints ticket tables (overdue rows in red) and ticket details with comments. | Keeps overdue-ticket display consistent everywhere. |
| `IRoleMenu.cs` | Defines a menu with a `Role` and `Show(user)`. | Establishes a common structure for all role menus. |
| `MenuRouter.cs` | Selects the menu whose role matches the user. | Replaces the old `switch` statement. |
| `WelcomeMenu.cs` | Runs the login/register/exit loop, handles forced password changes, and runs auto-close whenever the user returns. | Implements the application entry flow. |
| `ClientMenu.cs`, `DispatcherMenu.cs`, `AgentMenu.cs`, `AdminMenu.cs` | Stubs that print `"not implemented yet"`. | Provide plug-in points for each role's menu implementation. |
