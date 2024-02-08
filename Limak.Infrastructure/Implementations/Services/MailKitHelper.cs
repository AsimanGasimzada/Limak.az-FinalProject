using Limak.Application.Abstractions.Helpers;
using Limak.Application.DTOs.Common;
using Limak.Application.DTOs.RepsonseDTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MimeKit;

namespace Limak.Infrastructure.Implementations.Services;

public class MailKitHelper : IEmailHelper
{
    private readonly IConfiguration _configuration;
    private readonly MailKitConfigurationDto _configurationDto;

    public MailKitHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _configurationDto = _configuration.GetSection("MailkitOptions").Get<MailKitConfigurationDto>();

    }

    public async Task<ResultDto> SendEmailAsync(MailRequestDto mailRequestDto)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_configurationDto.Mail);
        email.To.Add(MailboxAddress.Parse(mailRequestDto.ToEmail));
        email.Subject = mailRequestDto.Subject;
        var builder = new BodyBuilder();
        builder.HtmlBody = mailRequestDto.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_configurationDto.Host, int.Parse(_configurationDto.Port), SecureSocketOptions.StartTls);
        smtp.Authenticate(_configurationDto.Mail, _configurationDto.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);

        return new("Mail successfully sended");
    }
}
