using SrpLab.Support;

namespace SrpLab;

/// <summary>
/// Support ticket: coordinates ticket data, priority analysis, SLA tracking, and reply composition.
/// </summary>
public sealed class SupportTicket
{
    private readonly TicketPriorityAnalyzer _priorityAnalyzer = new();
    private readonly SlaPolicy _slaPolicy = new();
    private readonly SupportReplyComposer _replyComposer = new();

    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;
        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText() =>
        Priority = _priorityAnalyzer.Analyze(Subject, Body);

    public DateTimeOffset SlaDeadline() => _slaPolicy.Deadline(Priority, OpenedAt);

    public bool IsBreached(DateTimeOffset now) => _slaPolicy.IsBreached(Priority, OpenedAt, now);

    public string DraftPublicReply(string agentName) =>
        _replyComposer.DraftPublicReply(Id, Priority, agentName, SlaDeadline());

    public string InternalEscalationBlurb() =>
        _replyComposer.InternalEscalationBlurb(Id, Priority, SlaDeadline());
}
