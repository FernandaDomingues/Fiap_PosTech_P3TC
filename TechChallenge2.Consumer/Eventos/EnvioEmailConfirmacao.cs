using MassTransit;
using Newtonsoft.Json;
using System.Text;
using TechChallenge2.Identity.Data.Dtos;

namespace TechChallenge2.Consumer.Eventos
{
    public class EnvioEmailConfirmacao : IConsumer<SignUpDto>
    {
        private string urlApi = "https://localhost:7287/api/mails";

        public Task Consume(ConsumeContext<SignUpDto> context)
        {
            Console.WriteLine(context.Message);
            SendEmail(context.Message);
            return Task.CompletedTask;
        }

        private async void SendEmail(SignUpDto dados)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(dados), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(urlApi, httpContent);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail. ", ex);
            }
        }
    }
}
