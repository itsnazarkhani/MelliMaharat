using MelliMaharat.Dal.DbContexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace MelliMaharat.Web.Middlewares
{
    public class CustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ApplicationDbContext _db;

        public CustomAuthHandler(
                IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger,
                UrlEncoder encoder,
                SystemClock clock,
                ApplicationDbContext db
            ) : base(options, logger, encoder, clock)
        {
            _db = db;
        }
    }
}
