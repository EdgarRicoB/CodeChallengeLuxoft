using CodeChallengeLuxoft.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeLuxoft.Utilities
{
    public static class JsonParser
    {
        /// <summary>
        /// Deserialize a Json into a JToken from a path given
        /// </summary>
        /// <param name="fileName">path to the json</param>
        /// <returns>a JToken of the json given</returns>
        public static JToken LoadJsonFile(string fileName)
        {
            try
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json = r.ReadToEnd();
                    var jToken = JsonConvert.DeserializeObject<JToken>(json);
                    return jToken;
                }
            }
            catch (Exception ex)
            {
                Logger.log_message($@"An exception occurred {Environment.NewLine}"
                                   + "Message: {ex.Message}{Environment.NewLine}"
                                   + "StackTrace: {ex.StackTrace}", nameof(LogLevels.Exception));
                throw ex;
            }

        }

        /// <summary>
        /// Method to find all the occurrences of a name as a key token
        /// </summary>
        /// <param name="containerToken"></param>
        /// <param name="name"></param>
        /// <returns>a list of matches if any</returns>
        public static List<JToken> FindTokens(this JToken containerToken, string name)
        {
            List<JToken> matches = new List<JToken>();
            FindTokens(containerToken, name, matches);
            if (!matches.Any())
            {
                return null;
            }
            return matches;
        }

        /// <summary>
        /// Recursive method to iterate and find occurrences in a JToken
        /// </summary>
        /// <param name="containerToken">Container to look into</param>
        /// <param name="name">Name token that is been searched</param>
        /// <param name="matches">list of all the matches found if any</param>
        private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        {
            if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    FindTokens(child, name, matches);
                }
            }
            else if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (child.Name == name)
                    {
                        matches.Add(child.Value);
                    }
                    FindTokens(child.Value, name, matches);
                }
            }
        }
    }
}
