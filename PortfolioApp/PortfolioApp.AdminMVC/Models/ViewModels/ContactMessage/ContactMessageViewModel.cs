﻿namespace PortfolioApp.AdminMVC.Models.ViewModels.ContactMessage;
public class ContactMessageViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Subject { get; set; } = string.Empty;
    public string Message { get; set; }
    public DateTime SentDate { get; set; }
    public bool IsRead { get; set; }
    public string? Reply { get; set; }
    public DateTime? ReplyDate { get; set; }
}
