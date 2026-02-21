using Propulse.Core.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Propulse.Infrastructure.Services;

public class TwilioWhatsAppService : IWhatsAppService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TwilioWhatsAppService> _logger;
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromNumber;

    public TwilioWhatsAppService(IConfiguration configuration, ILogger<TwilioWhatsAppService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        
        _accountSid = _configuration["Twilio:AccountSid"] ?? "";
        _authToken = _configuration["Twilio:AuthToken"] ?? "";
        _fromNumber = _configuration["Twilio:FromNumber"] ?? "whatsapp:+14155238886"; // Default sandbox number
        
        if (string.IsNullOrEmpty(_accountSid) || string.IsNullOrEmpty(_authToken))
        {
            _logger.LogWarning("Twilio credentials are missing in configuration.");
        }
        else
        {
            TwilioClient.Init(_accountSid, _authToken);
        }
    }

    public async Task SendMessageAsync(string to, string body)
    {
        if (string.IsNullOrEmpty(_accountSid)) 
        {
            _logger.LogWarning($"Simulated sending to {to}: {body}");
            return;
        }

        try
        {
            var message = await MessageResource.CreateAsync(
                from: new PhoneNumber(_fromNumber),
                to: new PhoneNumber(to),
                body: body
            );
            _logger.LogInformation($"Message sent to {to}, SID: {message.Sid}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send WhatsApp message to {to}");
        }
    }
}
