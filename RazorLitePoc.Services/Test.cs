using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using RazorLight;

namespace RazorLitePoc.Services
{
    public class Test
    {
        private List<EmailDigestVm>  model = new List<EmailDigestVm>
        {
            new EmailDigestVm("hey1", "look at me"),
            new EmailDigestVm("hey2", "don't look")
        };

        public async Task GetTemplateByPath()
        {
            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(@"C:\projects\Temp\RazorLitePoc\RazorLitePoc.Services\EmailTemplates/")
                .UseMemoryCachingProvider()
                .Build();

            var result = await engine.CompileRenderAsync("NotificationDigest", model);

            await SendEmail(result);
        }

        public async Task GetTemplateByResource()
        {
            var engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(Test))
                .UseMemoryCachingProvider()
                .Build();

            var result = await engine.CompileRenderAsync("EmailTemplates.NotificationDigest", model);

            await SendEmail(result);
        }


        private async Task SendEmail(string result)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("senderName", "senderEmail@localhost"));
                message.To.Add(new MailboxAddress("senderName", "senderEmail@localhost"));
                message.Subject = "rl test";
                message.Body = new BodyBuilder { HtmlBody = result }.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync("localhost", 25, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}