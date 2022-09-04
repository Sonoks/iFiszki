using Newtonsoft.Json;

namespace iFiszki
{
    public class Row
    {
        public string OriginalWord { get; set; }
        public string TranslatedWord { get; set; }
        public int OriginalWordCounter { get; set; }
        public int TranslatedWordCounter { get; set; }

        [JsonIgnore]
        public bool Passed { get; set; } = false;
    }
}
