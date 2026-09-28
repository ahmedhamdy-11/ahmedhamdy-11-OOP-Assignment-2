namespace SrpLab.Support;

/// <summary>Customer-facing reply templates — changes with CX team.</summary>
public sealed class SupportReplyComposer
{
    public string DraftPublicReply(string ticketId, string priority, string agentName, DateTimeOffset slaDeadline)
    {
        var apology = priority == "P1" ? "We are treating this as a critical incident." : "Thanks for reaching out.";
        return $"Hi,\n{apology}\nTicket {ticketId} is with {agentName}. Next update before {slaDeadline:u}.\n";
    }

    public string InternalEscalationBlurb(string ticketId, string priority, DateTimeOffset slaDeadline) =>
        $"ESCALATE {ticketId} priority={priority} breachAt={slaDeadline:u} keywords-scanned=yes";
}
