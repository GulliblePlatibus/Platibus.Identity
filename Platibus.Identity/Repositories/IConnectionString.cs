namespace Platibus.Identity.Repositories
{
    public interface IConnectionString
    {
        /// <summary>
        ///
        /// An interface to provide a connection string for the DB. 
        /// </summary>
        /// <returns>The connectionstring to the DB</returns>
        string GetConnectionString();
    }
}
