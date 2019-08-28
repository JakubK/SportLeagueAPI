using System.Collections.Generic;
using EntityGraphQL;

namespace SportLeagueAPI.Middleware
{
    public class NewQueryRequest
    {
        public QueryRequest Request {get;set;}
        public List<Meta> TokensToReplace {get;set;}

        public Dictionary<string, object> GetInputs()
        {
            var variables = Request.Variables;   
                    
            // foreach(var entry in variables)
            // {
            //     System.Console.Error.WriteLine(entry.Key + " " + entry.Value);
            // }
            // the following implementation seems brittle because of a lot of casting
            // and it depends on the types that ToInputs() creates.

            foreach (var info in TokensToReplace)
            {
                int i = 0;
                object o = variables;

                foreach (var p in info.Parts)
                {
                    var isLast = i++ == info.Parts.Count - 1;

                    if (p is string s)
                    {
                        if (isLast)
                            ((QueryVariables)o)[s] = info.File;
                        else
                            o = ((QueryVariables)o)[s];
                    }
                    else if (p is int index)
                    {
                        if (isLast)
                            ((List<object>)o)[index] = info.File;
                        else
                            o = ((List<object>)o)[index];
                    }
                }
            }

            // foreach(var entry in variables)
            // {
            //     System.Console.Error.WriteLine(entry.Key + " " + entry.Value);
            // }

            return variables;
        }
    }
}