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

        // Configuração do banco de dados PostgreSQL
        // Configuração do banco de dados PostgreSQL com tratamento de exceções
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

            // Se preferir, pode lançar novamente a exceção para interromper a execução
            throw;
        }

        // Registro de repositórios
        builder.Services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
        builder.Services.AddScoped<IEventoRepository, EventoRepository>();
        builder.Services.AddScoped<IPalestranteRepository, PalestranteRepository>();
        builder.Services.AddScoped<IOrganizadorRepository, OrganizadorRepository>();
        builder.Services.AddScoped<ILocalRepository, LocalRepository>();
        builder.Services.AddScoped<IAssociacaoEventoPessoa, AssociacaoEventoPessoaRepository>();

        // Registro de serviços
        builder.Services.AddScoped<IParticipanteService, ParticipanteService>();
        builder.Services.AddScoped<IPalestranteService, PalestranteService>();
        builder.Services.AddScoped<IOrganizadorService, OrganizadorService>();
        builder.Services.AddScoped<IEventoService, EventoService>();
        builder.Services.AddScoped<ILocalService, LocalService>();
        builder.Services.AddScoped<IInscricaoService, InscricaoService>();

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
