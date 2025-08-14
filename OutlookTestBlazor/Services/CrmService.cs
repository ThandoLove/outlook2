using Microsoft.EntityFrameworkCore;

using OutlookTestBlazor.Models;

public interface ICrmService
{
    Task<CrmRecord?> FindRecordByEmailOrContent(string email, string content);
    Task<CrmRecord> CreateNewContact(string email, string content);
}

// Fix for IDE0290: Use primary constructor for CrmService
public class CrmService(ApplicationDbContext db) : ICrmService
{
    private readonly ApplicationDbContext _db = db;

    public async Task<CrmRecord?> FindRecordByEmailOrContent(string email, string content)
    {
        var contact = await GetContactSet().FirstOrDefaultAsync(c => c.Email == email);
        if (contact != null)
        {
            return new CrmRecord
            {
                    Id = Guid.NewGuid(), // Generate a new Guid since Contact.Id is an int
                Type = "Contact",
                Name = contact.FullName,
                Summary = $"Contact: {contact.FullName}, {contact.Email}"
            };
        }

        return null;
    }

    private DbSet<Contact> GetContactSet()
    {
        return _db.Contacts;
    }

    public async Task<CrmRecord> CreateNewContact(string email, string content)
    {
        var name = email.Split('@')[0];
        var contact = new Contact { Email = email, FullName = name };

        _db.Contacts.Add(contact);
        await _db.SaveChangesAsync();

        return new CrmRecord
        {
            Id = Guid.NewGuid(), // Generate a new Guid since Contact.Id is an int
            Type = "Contact",
            Name = contact.FullName,
            Summary = $"New Contact: {contact.FullName}, {contact.Email}"
        };
    }
}

// Fix for CS0051: Make ApplicationDbContext public to match the accessibility of CrmService's constructor
public class ApplicationDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; } = default!;
}
