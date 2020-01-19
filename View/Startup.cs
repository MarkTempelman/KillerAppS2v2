using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.SQLContext;
using Data.Interfaces;
using Logic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace View
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(Configuration);

            services.AddTransient<IMovieContext, MovieSQLContext>(m =>
            {
                string c = Configuration.GetConnectionString("DefaultConnection");
                return new MovieSQLContext(c);
            });
            services.AddTransient<IGenreContext, GenreSQLContext>(g =>
            {
                string c = Configuration.GetConnectionString("DefaultConnection");
                return new GenreSQLContext(c);
            });
            services.AddTransient<IUserContext, UserSQLContext>(u =>
            {
                string c = Configuration.GetConnectionString("DefaultConnection");
                return new UserSQLContext(c);
            });
            services.AddTransient<IPlaylistContext, PlaylistSQLContext>(p =>
            {
                string c = Configuration.GetConnectionString("DefaultConnection");
                return new PlaylistSQLContext(c);
            });
            services.AddTransient<IMediaContext, MediaSQLContext>(m =>
            {
                string c = Configuration.GetConnectionString("DefaultConnection");
                return new MediaSQLContext(c);
            });
            services.AddTransient<IRatingContext, RatingSQLContext>(r =>
            {
                string c = Configuration.GetConnectionString("DefaultConnection");
                return new RatingSQLContext(c);
            });

            services.AddTransient(m =>
            {
                IMovieContext mc = m.GetService<IMovieContext>();
                GenreLogic gl = m.GetService<GenreLogic>();
                SearchLogic sl = m.GetService<SearchLogic>();
                PlaylistLogic p = m.GetService<PlaylistLogic>();
                MediaLogic me = m.GetService<MediaLogic>();
                RatingLogic rl = m.GetService<RatingLogic>();
                return new MovieLogic(mc, gl, sl, p, me, rl);
            });

            services.AddTransient(g =>
            {
                IGenreContext gc = g.GetService<IGenreContext>();
                return new GenreLogic(gc);
            });

            services.AddTransient(s =>
            {
                GenreLogic gl = s.GetService<GenreLogic>();
                return new SearchLogic(gl);
            });

            services.AddTransient(s =>
            {
                IUserContext uc = s.GetService<IUserContext>();
                PlaylistLogic pl = s.GetService<PlaylistLogic>();
                RatingLogic rl = s.GetService<RatingLogic>();
                return new UserLogic(uc, pl, rl);
            });

            services.AddTransient(p =>
            {
                IPlaylistContext pl = p.GetService<IPlaylistContext>();
                IMediaContext m = p.GetService<IMediaContext>();
                return new PlaylistLogic(pl, m);
            });

            services.AddTransient(me =>
            {
                IMediaContext m = me.GetService<IMediaContext>();
                return new MediaLogic(m);
            });

            services.AddTransient(r =>
            {
                IRatingContext rc = r.GetService<IRatingContext>();
                MediaLogic m = r.GetService<MediaLogic>();
                return new RatingLogic(rc, m);
            });

            

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/User/Login";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));
                options.AddPolicy("User", p => p.RequireAuthenticatedUser().RequireRole("User"));
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Movie}/{action=Index}/{id?}");
            });
        }
    }
}
