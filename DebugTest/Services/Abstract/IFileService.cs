using DebugTest.Models;

namespace DebugTest.Services.Abstract
{
    public interface IFileService
    {
        void WriteWeatherInfoIntoFile<T>(T dataToSave, string fileName);
    }
}
