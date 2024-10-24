using DevExpress.Xpo;
using Newtonsoft.Json.Linq;
using SevenZipExtractor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RMS.Core.ModelSbis.Controllers
{
    public static class ModelSbisController
    {
        public static Glossary GetGlossary(this ReportSbisModel baseObj, string propertyName)
        {
            var glossary = baseObj.Glossaries
                ?.Where(w => !string.IsNullOrWhiteSpace(w.Name) && w.Name == propertyName)
                ?.FirstOrDefault();

            return glossary;
        }

        public static async System.Threading.Tasks.Task<ReportSBIS> CreateReportSBISAsync(Session session, ReportSbisModel reportSbisModel)
        {
            var glossary = reportSbisModel.GetGlossary("ИдКомплекта");

            if (glossary != null)
            {
                if (Guid.TryParse(glossary.Value?.ToString(), out Guid result))
                {
                    var obj = await new XPQuery<ReportSBIS>(session)
                        .Where(w => w.Guid != null 
                            //&& !string.IsNullOrWhiteSpace(w.Guid) 
                            && w.Guid.Equals(result.ToString()))
                        .FirstOrDefaultAsync();

                    if (obj is null)
                    {
                        obj = new ReportSBIS(session);
                    }

                    obj.SetValue(reportSbisModel);
                    obj.SetCustomer();

                    return obj;
                }
            }

            return default;
        }

        public static async System.Threading.Tasks.Task<List<ReportSBIS>> CreateReportSBISAsync(this IEnumerable<ReportSbisModel> collection, Session session)
        {
            var result = new List<ReportSBIS>();
            foreach (var item in collection)
            {
                result.Add(await CreateReportSBISAsync(session, item));
            }
            return result;
        }

        public static List<ReportSbisModel> GetTemperaturesByArchives(IEnumerable<string> paths)
        {
            var result = new List<ReportSbisModel>();

            foreach (var path in paths)
            {
                result.AddRange(GetTemperaturesByArchive(path));
            }

            return result;
        }

        public static List<ReportSbisModel> GetTemperaturesByArchive(string path)
        {
            var collection = new List<ReportSbisModel>();
            if (System.IO.File.Exists(path))
            {
                using (ArchiveFile archiveFile = new ArchiveFile(path))
                {
                    foreach (Entry entry in archiveFile.Entries)
                    {
                        var fileNameFull = entry.FileName;
                        var fileName = fileNameFull.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)?.LastOrDefault();
                        if (entry.IsFolder is false && fileNameFull.ToLower().Contains(".json"))
                        {
                            var json = GetJsonByArchiveFile(entry);
                            if (!string.IsNullOrWhiteSpace(json))
                            {
                                var glossaries = GetGlossaries(json);
                                collection.Add(new ReportSbisModel(fileNameFull, fileName, glossaries));
                            }
                        }
                    }
                }
            }

            return collection;
        }

        private static List<Glossary> GetGlossaries(string json, string parentName = default)
        {
            var result = new List<Glossary>();
            JArray array = JArray.Parse($"[{json}]");

            foreach (var jobject in array.Children<JObject>())
            {
                foreach (var jproperty in jobject.Properties())
                {
                    var nameProperty = jproperty?.Name;
                    if (!string.IsNullOrWhiteSpace(nameProperty) && !nameProperty.Equals("s"))
                    {
                        var value = jproperty.Value.ToString();

                        if (jproperty.Value.HasValues)
                        {
                            if (!nameProperty.Equals("d"))
                            {
                                parentName += $"\\{nameProperty}";
                            }

                            result.AddRange(GetGlossaries(value, parentName));
                        }
                        else if (!string.IsNullOrWhiteSpace(value) && !value.Equals("[]"))
                        {
                            var glossary = new Glossary(nameProperty, value);
                            glossary.SetParentName(parentName);

                            result.Add(glossary);
                        }
                    }
                }
            }

            return result;
        }

        private static string GetJsonByArchiveFile(Entry entry)
        {
            using (var memoryStream = new MemoryStream())
            {
                entry.Extract(memoryStream);
                using (var streamReader = new StreamReader(memoryStream))
                {
                    memoryStream.Position = 0;
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
