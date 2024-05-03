namespace MapaAndMaya.Services.Models;

public class Town
{
    public int Id { get; set; }

    public string Name { get; set; } = "";
    
    public CumFum? CumFum { get; set; }
}