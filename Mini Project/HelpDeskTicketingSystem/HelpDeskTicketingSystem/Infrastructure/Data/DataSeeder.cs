using HelpDeskTicketingSystem.Domain.Entities;
using HelpDeskTicketingSystem.Domain.Enums;
using HelpDeskTicketingSystem.Interfaces.Auth;

namespace HelpDeskTicketingSystem.Infrastructure.Data
{
    internal class DataSeeder
    {
        private readonly DataStore _store;
        private readonly IPasswordHasher _hasher;

        public DataSeeder(DataStore store, IPasswordHasher hasher)
        {
            _store = store;
            _hasher = hasher;
        }

        public void Seed()
        {
            if (_store.Users.Any())
                return;

            Admin admin = new Admin
            {
                Id = _store.NextUserId(),
                Name = "System Admin",
                Email = "admin@helpdesk.com",
                PasswordHash = _hasher.Hash("Admin123!")
            };

            Dispatcher dispatcher = new Dispatcher
            {
                Id = _store.NextUserId(),
                Name = "Dana Dispatcher",
                Email = "dispatcher@helpdesk.com",
                PasswordHash = _hasher.Hash("Dispatch123!")
            };

            Agent hwAgent1 = new Agent
            {
                Id = _store.NextUserId(),
                Name = "Alex Hardware",
                Email = "alex.hw@helpdesk.com",
                PasswordHash = _hasher.Hash("Agent123!"),
                Department = Department.Hardware,
                LastAssignedAt = DateTime.Now.AddHours(-5)
            };

            Agent hwAgent2 = new Agent
            {
                Id = _store.NextUserId(),
                Name = "Blair Hardware",
                Email = "blair.hw@helpdesk.com",
                PasswordHash = _hasher.Hash("Agent123!"),
                Department = Department.Hardware,
                LastAssignedAt = DateTime.Now.AddHours(-1)
            };

            Agent swAgent = new Agent
            {
                Id = _store.NextUserId(),
                Name = "Casey Software",
                Email = "casey.sw@helpdesk.com",
                PasswordHash = _hasher.Hash("Agent123!"),
                Department = Department.Software
            };

            Agent billingAgent = new Agent
            {
                Id = _store.NextUserId(),
                Name = "Drew Billing",
                Email = "drew.billing@helpdesk.com",
                PasswordHash = _hasher.Hash("Agent123!"),
                Department = Department.Billing
            };

            Client client1 = new Client
            {
                Id = _store.NextUserId(),
                Name = "Emma Client",
                Email = "emma@client.com",
                PasswordHash = _hasher.Hash("Client123!")
            };

            Client client2 = new Client
            {
                Id = _store.NextUserId(),
                Name = "Frank Client",
                Email = "frank@client.com",
                PasswordHash = _hasher.Hash("Client123!")
            };

            _store.Users.AddRange(new User[]
            {
                admin, dispatcher, hwAgent1, hwAgent2, swAgent, billingAgent, client1, client2
            });

            Ticket openTicket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = "Laptop won't turn on",
                Description = "Pressed the power button, nothing happens.",
                Status = TicketStatus.Open,
                ClientId = client1.Id,
                CreatedAt = DateTime.Now.AddHours(-2)
            };

            Ticket assignedTicket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = "Printer jamming constantly",
                Description = "Every print job jams on page 2.",
                Status = TicketStatus.Assigned,
                ClientId = client1.Id,
                AssignedUserId = hwAgent1.Id,
                Priority = TicketPriority.Medium,
                Department = Department.Hardware,
                CreatedAt = DateTime.Now.AddHours(-10),
                DueDate = DateTime.Now.AddHours(38)
            };

            Ticket inProgressTicket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = "VPN keeps disconnecting",
                Description = "Drops every 10 minutes while working from home.",
                Status = TicketStatus.InProgress,
                ClientId = client2.Id,
                AssignedUserId = swAgent.Id,
                Priority = TicketPriority.High,
                Department = Department.Software,
                CreatedAt = DateTime.Now.AddHours(-20),
                DueDate = DateTime.Now.AddHours(4)
            };
            inProgressTicket.Comments.Add(new Comment
            {
                Id = _store.NextCommentId(),
                TicketId = inProgressTicket.Id,
                AuthorId = client2.Id,
                Text = "Any update on this? It's been happening all week.",
                CreatedAt = DateTime.Now.AddHours(-15)
            });
            inProgressTicket.Comments.Add(new Comment
            {
                Id = _store.NextCommentId(),
                TicketId = inProgressTicket.Id,
                AuthorId = swAgent.Id,
                Text = "Looking into it now, checking your router logs.",
                CreatedAt = DateTime.Now.AddHours(-14)
            });

            Ticket overdueTicket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = "Invoice charged twice",
                Description = "My card was charged two times for the same invoice.",
                Status = TicketStatus.InProgress,
                ClientId = client1.Id,
                AssignedUserId = billingAgent.Id,
                Priority = TicketPriority.High,
                Department = Department.Billing,
                CreatedAt = DateTime.Now.AddHours(-30),
                DueDate = DateTime.Now.AddHours(-6)
            };

            Ticket freshResolvedTicket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = "Keyboard keys not responding",
                Description = "Several keys stopped working after a coffee spill.",
                Status = TicketStatus.Resolved,
                ClientId = client2.Id,
                AssignedUserId = hwAgent2.Id,
                Priority = TicketPriority.Low,
                Department = Department.Hardware,
                CreatedAt = DateTime.Now.AddHours(-30),
                DueDate = DateTime.Now.AddHours(42),
                ResolvedAt = DateTime.Now.AddHours(-1)
            };

            Ticket closedTicket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = "Monitor flickering",
                Description = "External monitor flickers on startup.",
                Status = TicketStatus.Closed,
                ClientId = client1.Id,
                AssignedUserId = hwAgent1.Id,
                Priority = TicketPriority.Low,
                Department = Department.Hardware,
                CreatedAt = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(-2),
                ResolvedAt = DateTime.Now.AddDays(-4)
            };

            Ticket autoCloseTicket = new Ticket
            {
                Id = _store.NextTicketId(),
                Title = "Application runs slowly",
                Description = "The accounting application takes minutes to open.",
                Status = TicketStatus.Resolved,
                ClientId = client2.Id,
                AssignedUserId = swAgent.Id,
                Priority = TicketPriority.Medium,
                Department = Department.Software,
                CreatedAt = DateTime.Now.AddDays(-6),
                DueDate = DateTime.Now.AddDays(-4),
                ResolvedAt = DateTime.Now.AddDays(-4)
            };

            _store.Tickets.AddRange(new Ticket[]
            {
                openTicket,
                assignedTicket,
                inProgressTicket,
                overdueTicket,
                freshResolvedTicket,
                closedTicket,
                autoCloseTicket
            });
        }
    }
}