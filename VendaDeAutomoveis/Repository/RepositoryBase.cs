namespace VendaDeAutomoveis.Repository
{
    public class RepositoryBase<DBEntity>
        where DBEntity : class
    {
        protected readonly GDCarsContext _context;

        public RepositoryBase(GDCarsContext context)
        {
            _context = context;
        }
    }
}