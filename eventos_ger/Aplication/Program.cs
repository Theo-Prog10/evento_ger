using eventos_ger.Model;
using eventos_ger.Repository;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Service;
using eventos_ger.Service.Interface;
using eventos_ger.Services;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração de serviços
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        try
        {
            builder.Services.AddDbContext<Ger_Evento_Bd>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
        catch (Exception ex)
        {
            // Log da exceção no console
            Console.WriteLine($"Erro ao configurar o DbContext: {ex.Message}");
            Console.WriteLine(ex.StackTrace);

            throw;
        }

        // Registro de repositórios
        builder.Services.AddScoped<IEventoRepository, EventoRepository>();
        builder.Services.AddScoped<ILocalRepository, LocalRepository>();
        builder.Services.AddScoped<IAssociacaoEventoPessoa, AssociacaoEventoPessoaRepository>();
        builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        // Registro de serviços
        builder.Services.AddScoped<IEventoService, EventoService>();
        builder.Services.AddScoped<ILocalService, LocalService>();
        builder.Services.AddScoped<IInscricaoService, InscricaoService>();
        builder.Services.AddScoped<IPessoaService, PessoaService>();

        // Adiciona suporte a controladores
        builder.Services.AddControllers();

        var app = builder.Build();

        // Configuração do Swagger
        
        app.UseSwagger();
        app.UseSwaggerUI();

        // Redirecionamento para HTTPS
        app.UseHttpsRedirection();

        // Mapeia controladores
        app.MapControllers();

        // Inicia o aplicativo
        app.Run();
    }
}
