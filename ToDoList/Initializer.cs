using Microsoft.AspNetCore.Cors.Infrastructure;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Service.Implementations;
using SocialProgrammer.Service.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SocialProgrammer
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository<UserEntity>, UserRepository>();
            services.AddScoped<IProfileRepository<ProfileEntity>, ProfileRepository>();
            services.AddScoped<ILikeRepository<LikeEntity>, LikeRepository>();
            services.AddScoped<IArticleRepository<ArticleEntity>, ArticleRepository>();
            services.AddScoped<ICommentRepository<CommentEntity>, CommentRepository>();
            services.AddScoped<IMessageRepository<MessageEntity>, MessageRepository>();
            services.AddScoped<IMessageHistoryRepository<MessageHistoryEntity>, MessageHistoryRepository>();
            services.AddScoped<ISearchRepository<SearchEntity>, SearchRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
