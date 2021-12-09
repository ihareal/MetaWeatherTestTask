using DebugTest.Services.Abstract;
using Newtonsoft.Json;
using System.IO;

namespace DebugTest.Services
{
    public class FileService : IFileService
    {
        public virtual void WriteWeatherInfoIntoFile<T>(T dataToSave, string fileName)
        {            
            using (var streamWriter = new StreamWriter(fileName))
            {
                var serialized = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);

                streamWriter.Write(serialized);
            }
        }
    }
}
