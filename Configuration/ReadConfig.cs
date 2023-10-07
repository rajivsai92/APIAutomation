using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace APIAutomation.Configuration
{
    public class ReadConfig
    {


        private IConfiguration? _configuration { get; set; }

        public IConfiguration GetConfigData()
        {
            try
            {


                _configuration = new ConfigurationBuilder()
                    .SetBasePath((new FileInfo(AppContext.BaseDirectory)).Directory.Parent.Parent.Parent.FullName.ToString() + @"\Configuration\")
                    .AddJsonFile("Config.json")
                    .Build();
                return _configuration;
                    

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
    }
}
