namespace Propulse.Core.Interfaces;

public interface IAiService
{
    Task<string> ParseMessageAsync(string messageContent);
    // Future: Task<string> GenerateReplyAsync(...);
}
