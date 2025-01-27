using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace API.Helper
{
    public static class FileHelper
    {
        public static string ReadReceivedFile(IFormFile file)
        {
            string fileContent = null;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                 fileContent = reader.ReadToEnd();
            }
            return fileContent;
        }
        public static string GetJsonSchema()
        {
            StreamReader reader = new("Resources/schema.json");

            string text = reader.ReadToEnd();
            return text;
        }
        public static bool IsJsonFileValidationSuccesfull(string receivedFile)
        {
            try 
            {
                JSchema schema = JSchema.Parse(FileHelper.GetJsonSchema());
                var receivedJsonObjects = new List<string>();
                var startIndex = receivedFile.IndexOf("{");
                var trimedReceivedFile = receivedFile.Substring(startIndex);
                foreach (var part in trimedReceivedFile.Split("},"))
                {
                    string jsonToAdd;
                    if (!part.Contains("}"))
                    {
                        jsonToAdd = part + "}";
                    }
                    else
                    {
                        jsonToAdd = part.Remove(part.IndexOf("]"));
                    }
                    receivedJsonObjects.Add(jsonToAdd);
                }

                foreach (var s in receivedJsonObjects)
                {
                    JObject json = JObject.Parse(s);
                    if (!json.IsValid(schema, out IList<string> errors))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
                      
        }
    }
}