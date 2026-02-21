using Propulse.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Propulse.Infrastructure.Services;

public class OpenAiService : IAiService
{
    private readonly ILogger<OpenAiService> _logger;

    public OpenAiService(ILogger<OpenAiService> logger)
    {
        _logger = logger;
    }

    public async Task<string> ParseMessageAsync(string messageContent)
    {
        // For now, bypass AI and return a dummy "success" signal so the app works.
        _logger.LogInformation("AI Skipped (No Key). Message processed as raw text.");
        
        // Return a dummy valid JSON so the Parser in Controller doesn't crash 
        // mimicking a "Not Processed but Saved" state
        return "{ \"is_offer\": false, \"note\": \"AI Disabled\" }";
    }
}
