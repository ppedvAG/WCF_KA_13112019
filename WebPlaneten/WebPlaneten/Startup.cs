using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebPlaneten.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebPlaneten
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
            services.AddControllersWithViews();

            //var op = new DbContextOptionsBuilder<WebPlanetenContext>();
            //op.UseSqlServer(Configuration.GetConnectionString("WebPlanetenContext"));
            var www = new DbContextOptionsBuilder<WebPlanetenContext>();



            services.AddDbContext<WebPlanetenContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("WebPlanetenContext"));
            });

            DbContextOptionsBuilder bla = new DbContextOptionsBuilder();
            bla.UseSqlServer(Configuration.GetConnectionString("WebPlanetenContext"));


            services.AddMvc().AddJsonOptions(x =>
            {
                var cc = new WebPlanetenContext(bla.Options);
                x.JsonSerializerOptions.Converters.Add(new mmm(cc));
            });
        }


        class mmm : JsonConverter<HashSet<Mond>>
        {
            private WebPlanetenContext context;
            public mmm(WebPlanetenContext context)
            {
                this.context = context;
            }

            public mmm()
            {

            }

            public override HashSet<Mond> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var mondeIds = reader.GetString();



                var lll = new HashSet<Mond>();
                foreach (var item in mondeIds.Split(","))
                {
                    //var mondFromDb = context.Mond.Find(int.Parse(item)); //evtl. aus cache
                    var mondFromDb = context.Mond.FirstOrDefault(x => x.Id == int.Parse(item)); //immer von DB
                    if (mondFromDb != null)
                    {
                        lll.Add(mondFromDb);
                    }

                    
                }
                return lll;
            }

            public override void Write(Utf8JsonWriter writer, HashSet<Mond> value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(string.Join(",", value.Select(x => x.Id)));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
