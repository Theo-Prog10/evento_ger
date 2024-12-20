﻿using eventos_ger.Model;
using eventos_ger.Repository;
using eventos_ger.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

// Registra outros serviços
builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();