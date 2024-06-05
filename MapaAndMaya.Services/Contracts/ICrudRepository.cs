namespace MapaAndMaya.Services.Contracts;

public interface ICrudRepository<TE, TM> where TE : class where TM : class
{
    public Task<ActionResult<TE>> Create(TM model);

    public Task<ActionResult<TE>> Update(TM model);

    public Task<ActionResult<IList<TE>>> Delete(IList<TE> entities);

    public IEnumerable<TE> Find();
}