using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIAutomation.APISupport
{
    public class HandleContent
    {



        public static T GetJsonContent<T>(dynamic str)
        {

			try
			{
				JsonSerializerSettings setting = new JsonSerializerSettings();
				setting.MissingMemberHandling = MissingMemberHandling.Error;
				return JsonConvert.DeserializeObject<T>(str, setting);


			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}


        }




		public static string SerializeJsonString(dynamic Content)
		{


			return JsonConvert.SerializeObject(Content, Formatting.Indented);

		}

		public static T ParseJson<T>(string file)
		{

			return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
		}

    }
}
