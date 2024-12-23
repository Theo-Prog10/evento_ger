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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<Ger_Evento_Bd>(opt => opt.UseInMemoryDatabase("Gerenciamento"));

        // Registra o repositório no contêiner de dependências
        builder.Services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
        builder.Services.AddScoped<IEventoRepository, EventoRepository>();
        builder.Services.AddScoped<IPalestranteRepository, PalestranteRepository>();
        builder.Services.AddScoped<IOrganizadorRepository, OrganizadorRepository>();
        builder.Services.AddScoped<ILocalRepository, LocalRepository>();
        builder.Services.AddScoped<IAssociacaoEventoPessoa, AssociacaoEventoPessoaRepository>();
        
        builder.Services.AddScoped<IParticipanteService, ParticipanteService>();
        builder.Services.AddScoped<IPalestranteService, PalestranteService>();
        builder.Services.AddScoped<IOrganizadorService, OrganizadorService>();
        builder.Services.AddScoped<ILocalService, LocalService>();
        builder.Services.AddScoped<IEventoService, EventoService>();
        builder.Services.AddScoped<InscricaoService, InscricaoService>();


        // Registra outros serviços
        builder.Services.AddControllers();
        var app = builder.Build();

        // Adiciona URLs para escutar em todas as interfaces de rede
        app.Urls.Add("http://0.0.0.0:8080"); // Porta HTTP
        

        // Configura Swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        // Configuração de HTTPS redirection
        app.UseHttpsRedirection();

        // Mapeia controladores
        app.MapControllers();

        // Inicia o aplicativo
        app.Run();
    }
}