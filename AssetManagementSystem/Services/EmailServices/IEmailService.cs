﻿namespace AssetManagementSystem.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
