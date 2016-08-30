using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Text;
using System.Reflection;
using ZipcodeService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZipcodeService.Helpers;

namespace ZipcodeService.Controllers
{
    public class ZipcodeController : ApiController
    {
        //Move it to Config
        string[] separators = { "\t" };
        string key = "ZipCodes";

        // GET api/values
        public string Get()
        {
            return (string)GetCachedData("");
        }


        // GET api/values/5
        public string Get(string queryParameters)
        {
            return (string)GetCachedData(queryParameters);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }


        #region "Helper"

        private List<ZipCode> ExtractData(string[] seperators)
        {
            string pathToFile = System.Web.Configuration.WebConfigurationManager.AppSettings["myFilePath"].ToString();

            List<ZipCode> zipCodes = new List<ZipCode>();
            var lines = File.ReadAllLines(pathToFile);

            for (int i = 1; i < lines.Length; i++)
            {
                ZipCode zipcode = new ZipCode();

                var vals = lines[i].Split(separators, 2, StringSplitOptions.None);
                zipcode.Code = vals[0];
                zipcode.Place = vals[1];

                zipCodes.Add(zipcode);
            }

            return zipCodes;
        }

        private string GetZipCodes(List<ZipCode> list, string queryParams)
        {
            string json;
            IEnumerable<ZipCode> result;

            if (!String.IsNullOrEmpty(queryParams))
            {
                result = list.Where(s => s.Code.Contains(queryParams) || s.Place.Contains(queryParams));
            }
            else
            {
                result = null;
            }

            json = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return json;
        }

        private string GetCachedData(string queryParam)
        {
            List<ZipCode> list;
            if (CacheManager.GetValue(key) == null)
            {
                list = ExtractData(separators);
                CacheManager.Add(key, list, DateTimeOffset.Now.AddMinutes(20));
            }
            else
            {
                list = (List<ZipCode>)CacheManager.GetValue(key);
            }

            if (list != null)
            {
                return GetZipCodes(list, queryParam);
            }
            else
            {
                return "";
            }
        }


        #endregion

    }
}

