using eventos_ger.Model;
using eventos_ger.Repository;
using eventos_ger.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<Ger_Evento_Bd>(opt =>
//     opt.UseNpgsql("Host=pg-295ab08e-theo-5135.j.aivencloud.com;Port=22824;Database=defaultdb;Username=avnadmin;Password=AVNS_zrwaxsPZ9ItxsCq3g6b"));

builder.Services.AddDbContext<Ger_Evento_Bd>(opt => opt.UseInMemoryDatabase("Gerenciamento"));

    // Registra o repositório no contêiner de dependências
builder.Services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>(); 
builder.Services.AddScoped<IPalestranteRepository, PalestranteRepository>();
builder.Services.AddScoped<IOrganizadorRepository, OrganizadorRepository>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<IInscricaoRepository, InscricaoRepository>();
builder.Services.AddScoped<IAssociacaoEventoPessoa, AssociacaoEventoPessoaRepository>();

    // Registra outros serviços
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();