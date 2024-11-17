using eventos_ger.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<Ger_Evento_Bd>(opt =>
//     opt.UseNpgsql("Host=pg-295ab08e-theo-5135.j.aivencloud.com;Port=22824;Database=defaultdb;Username=avnadmin;Password=AVNS_zrwaxsPZ9ItxsCq3g6b"));

builder.Services.AddDbContext<Ger_Evento_Bd>(opt => opt.UseInMemoryDatabase("Gerenciamento"));

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();