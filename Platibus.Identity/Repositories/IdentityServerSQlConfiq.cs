namespace Platibus.Identity.Repositories
{
    public class IdentityServerSQlConfiq : IConnectionString
    {
        // The connection string used to connect to database
        public string _connectionstring;

        public IdentityServerSQlConfiq()
        {
            var db = "rfxoqowa";
            var user = "rfxoqowa";
            var passwd = "oSeKXhosgqtffV2cYgWy-7zDoXRyS91e";
            var port =  5432;
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                "manny.db.elephantsql.com", db, user, passwd, port);
            _connectionstring = connStr;
        }

        public string GetConnectionString()
        {
            return _connectionstring;
        }
    }
}