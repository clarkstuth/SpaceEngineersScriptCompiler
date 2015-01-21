using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    static class CollectionDataExtensions
    {
        public static Dictionary<string, List<string>> Merge(this Dictionary<string, List<string>> thisDict, Dictionary<string, List<string>> otherDict)
        {
            var newDict = new Dictionary<string, List<string>>(thisDict);

            foreach (KeyValuePair<string, List<string>> item in otherDict)
            {
                if (newDict.ContainsKey(item.Key))
                {
                    foreach (var listItem in item.Value)
                    {
                        if (!newDict[item.Key].Contains(listItem))
                        {
                            newDict[item.Key].Add(listItem);
                        }
                    }
                }
                else
                {
                    newDict.Add(item.Key, item.Value);
                }
            }

            return newDict;
        }

    }
}
