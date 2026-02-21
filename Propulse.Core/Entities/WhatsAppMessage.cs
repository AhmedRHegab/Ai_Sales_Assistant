namespace Propulse.Core.Entities;

public class WhatsAppMessage : BaseEntity
{
    public string SenderPhoneNumber { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty; // Often sent by Twilio/WhatsApp
    public string GroupId { get; set; } = string.Empty; // If from a group
    public string Content { get; set; } = string.Empty;
    public string MessageType { get; set; } = "text"; // text, image, etc.
    
    // For AI Phase (Phase 2 & 3)
    public bool IsProcessed { get; set; } = false;
    public bool IsOffer { get; set; } = false;
    public string? StructuredDataJson { get; set; } // To store parsed AI result
}
