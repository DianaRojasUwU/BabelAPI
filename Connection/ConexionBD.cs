namespace BabelAPI.Connection
{
    public class ConexionBD
    {
        private string connectionstring = string.Empty;
        public ConexionBD()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            connectionstring = builder.GetSection("ConnectionStrings:masterconnection").Value;
        }
        public string ConnectionString()
        {
            return connectionstring;
        }
    }
}
