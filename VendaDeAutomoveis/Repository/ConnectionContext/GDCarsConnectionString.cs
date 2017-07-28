using System.Configuration;

namespace VendaDeAutomoveis.DAO.ConnectionContext
{
    public class GDCarsConnectionString
    {
        static public string Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GDCarsContext"].ConnectionString;
            return connectionString;
        }
    }
}