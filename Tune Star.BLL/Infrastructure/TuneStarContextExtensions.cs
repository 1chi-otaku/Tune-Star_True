using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tune_Star.DAL.EF;

namespace Tune_Star.BLL.Infrastructure
{
    public static class TuneStarContextExtensions
    {
        public static void AddSongTuneContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<TuneStarContext>(options => options.UseSqlServer(connection));
        }
    }
}
