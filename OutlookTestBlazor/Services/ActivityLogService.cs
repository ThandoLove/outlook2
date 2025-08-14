public class ActivityLogService
{
    public Task LogCreation(string entityType, string email, string name)
    {
        Console.WriteLine($"[LOG] {entityType} created for {email} ({name}) at {DateTime.Now}");
        return Task.CompletedTask;
    }
}

