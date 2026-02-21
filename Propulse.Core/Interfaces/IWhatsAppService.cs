namespace Propulse.Core.Interfaces;

public interface IWhatsAppService
{
    Task SendMessageAsync(string to, string body);
}
