using Microsoft.EntityFrameworkCore;

namespace ApiCoppel.Data.Context
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
