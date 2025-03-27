namespace UsersAPI.Application.Configurations
{
    public static class CorsConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            // Configuração do CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // URL da aplicação Angular
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        public static void UseCorsConfiguration(this IApplicationBuilder app)
        {
            // Ativando o CORS antes do UseAuthorization
            app.UseCors("AllowAngularApp");
        }
    }
}
