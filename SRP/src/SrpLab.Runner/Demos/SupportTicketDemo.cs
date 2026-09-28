using SrpLab;

namespace SrpLab.Runner.Demos;

public sealed class SupportTicketDemo : ILabDemo
{
    public void Run()
    {
        var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);
        Console.WriteLine(ticket.DraftPublicReply("Nora"));
    }
}
