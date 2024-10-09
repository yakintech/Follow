using FluentValidation;
using Follow.API.DTO.BlogCategory;
using Follow.API.DTO.WebUser;
using Follow.API.Validators;
using Follow.Business.Repository;
using Follow.Data.Models;

namespace Follow.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<BlogCategory>, GenericRepository<BlogCategory>>();
            services.AddScoped<IGenericRepository<BlogCategory>, GenericRepository<BlogCategory>>();
            services.AddScoped<IGenericRepository<BlogPost>, GenericRepository<BlogPost>>();
            services.AddScoped<IGenericRepository<AdminUser>, GenericRepository<AdminUser>>();
            services.AddScoped<IGenericRepository<WebUser>, GenericRepository<WebUser>>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateBlogCategoryRequestDTO>, CreateBlogCategoryRequestValidator>();
            services.AddScoped<IValidator<CreateWebUserRequestDTO>, CreateWebUserRequestValidator>();
            return services;
        }
    }
}
