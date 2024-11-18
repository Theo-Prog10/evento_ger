namespace eventos_ger.Model;

using Microsoft.EntityFrameworkCore;


public class Ger_Evento_Bd : DbContext
{
    public Ger_Evento_Bd(DbContextOptions<Ger_Evento_Bd> options) : base(options)
    {
    }

    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Local> Locais { get; set; }
    public DbSet<Organizador> Organizadores { get; set; }
    public DbSet<Palestrante> Palestrantes { get; set; }
    public DbSet<Participante> Participantes { get; set; }
    
}