using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Application.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }

        public string EmailFrom { get; set; } = "xxx@gmail.com";
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
        public string SmtpUser { get; set; } = "xxx@gmail.com";
        public string SmtpPass { get; set; } = "";//only for this app
    }

}
