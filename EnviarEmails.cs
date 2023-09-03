using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace EmailConfigReader
{
    class Email
    {
        public static EmailConfigurations GetConfig()
        {
            try
            {
                string configFile = "config.json";
                if (File.Exists(configFile))
                {
                    string configJson = File.ReadAllText(configFile);
                    EmailConfigurations? emailConfigs = JsonSerializer.Deserialize<EmailConfigurations>(configJson);

                    if (emailConfigs == null)
                    {
                        throw new ArgumentException("Erro ao ler as configurações de e-mail do arquivo.");
                    }
                    else
                    {
                        return emailConfigs;
                    }
                }
                else
                {
                    throw new ArgumentException("Arquivo de configuração não encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocorreu um erro: " + ex.Message);
            }
        }

        public static void SendEmail(EmailConfigurations emailConfigs, string emailBody)
        {
            try
            {
                if (emailConfigs.SenderEmail != null && emailConfigs.DestinationEmail != null) 
                {
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(emailConfigs.SenderEmail),
                        Subject = "Cotação de ativo",
                        Body = emailBody
                    };
                    mail.To.Add(emailConfigs.DestinationEmail);

                    SmtpClient smtpClient = new SmtpClient(emailConfigs.SmtpServer)
                    {
                        Port = emailConfigs.SmtpPort,
                        Credentials = new NetworkCredential(emailConfigs.SmtpUsername, emailConfigs.SmtpPassword),
                        EnableSsl = true
                    };

                    smtpClient.Send(mail);
                    Console.WriteLine("E-mail enviado com sucesso!");
                }
                else 
                {
                    Console.WriteLine("As configurações de e-mail estão incompletas.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar o e-mail: " + ex.Message);
            }
        }
    }
}

    class EmailConfigurations
    {
        public string? SenderEmail { get; set; }
        public string? DestinationEmail { get; set; }
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string? SmtpUsername { get; set; }
        public string? SmtpPassword { get; set; }
    }

