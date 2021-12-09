using Newtonsoft.Json;

namespace DebugTest.Models
{
    public class SourceDto
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Url { get; set; }

        [JsonProperty("crawl_rate")]
        public int CrawlRate { get; set; }
    }
}
