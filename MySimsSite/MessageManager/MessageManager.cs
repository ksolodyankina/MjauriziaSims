using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace MjauriziaSims.MessageManager
{
    public class MessageManager
    {
        public Dictionary<string, Msg> Messages { get; set; }
        private IHttpContextAccessor _httpContextAccessor;

        public MessageManager(IMsgRepository msgRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Messages = new Dictionary<string, Msg>();
            foreach (var msg in msgRepository.Messages.ToList())
            {
                Messages[msg.Code] = msg;
            }
        }

        public string Msg(string code)
        {
            var locale = Thread.CurrentThread.CurrentCulture;

            var msg = "";
            if (Messages.ContainsKey(code))
            {
                if (locale.ToString() == "en")
                {
                    msg = Messages[code].MsgEn;
                }
                else
                {
                    msg = Messages[code].MsgRu;
                }
            }
            
            return msg;
        }
    }
}
