namespace MapaAndMaya.Services.Contracts;

public interface ICrudService <TE,TM> where TE : class where TM: class 
{
    public Task<ActionResult<TE>> Create(TM model);
    
    public Task<ActionResult<TE>> Update(TM model);
    
    public Task<ActionResult<TE>> Delete(TE entity);
    
    public Task<ICollection<TE>> FindAll();
    
    public Task<TE?> Find(int id);
}