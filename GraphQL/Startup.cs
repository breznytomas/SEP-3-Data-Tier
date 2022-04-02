using CarSharing_Database_GraphQL.ErrorsFilter;
using CarSharing_Database_GraphQL.Mutations;
using CarSharing_Database_GraphQL.Queries;
using Database_EFC.Persistence;
using Database_EFC.Repositories;
using Database_EFC.Repositories.Impl;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarSharing_Database_GraphQL
{
    public class Startup
    {
        // TODO 10.11 by Ion - Delete after development
        // exposes the details of the graphQL exceptions
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }
        // ------
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<CarSharingDbContext>();

            services
                .AddScoped<IVehicleRepo, VehicleRepo>()
                .AddScoped<IListingRepo, ListingRepo>()
                .AddScoped<ICustomerRepo, CustomerRepo>()
                .AddScoped<IAccountRepo, AccountRepo>()
                .AddScoped<ILeaseRepo, LeaseRepo>()
                .AddScoped<ICouponRepo, CouponRepo>();

            services
                .AddGraphQLServer()
                // TODO 10.11 by Ion - Delete after development
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = _env.IsDevelopment())
                //---
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();
                
            services.AddErrorFilter<GraphQlErrorFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                });

            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }
    }
}