namespace AnalizadorSoftware.Models;

public class Proceso
{
    public int IdProceso { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    
    public List<Subproceso> Subprocesos { get; set; } = new();
}

public class Subproceso
{
    public int IdSubproceso { get; set; }
    public int IdProceso { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    
    public Proceso? Proceso { get; set; }
    public List<CasoUso> CasosUso { get; set; } = new();
}

public class CasoUso
{
    public int IdCasoUso { get; set; }
    public int IdSubproceso { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? ActorPrincipal { get; set; }
    public short? TipoCasoUso { get; set; } // 1=Funcional, 2=No Funcional, 3=Sistema
    public string? Precondiciones { get; set; }
    public string? Postcondiciones { get; set; }
    public string? CriteriosDeAceptacion { get; set; }
    
    public Subproceso? Subproceso { get; set; }
}
