using System;

namespace Sensoria.SmokeTest.Api.Models
{
    public class Achievement
    {
        public string Type { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public int? NameId { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public int UserId { get; set; }
        public int QueryId { get; set; }
        public DateTimeOffset? AchievedDateTime { get; set; }
        public int? SessionId { get; set; }
        public int? MessageTemplateId { get; set; }
        public string IconClass {
            get {
                if (Type == "Badge") {
                    return AchievedDateTime.HasValue ? string.Format("badge-{0}_{1}", IconBaseFilename, IconSize) : string.Format("badge-{0}_{1}_disabled", IconBaseFilename, IconSize);
                }
                return AchievedDateTime.HasValue ? string.Format("pr-{0}_{1}", IconBaseFilename, IconSize) : string.Format("pr-{0}_disabled_{1}", IconBaseFilename, IconSize);
             }
        }

        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int? CategoryOrderId { get; set; }
        public string IconBaseFilename { get; set; }
        public string IconSize { get; set; }
        public decimal? Distance { get; set; }
        public int? Duration { get; set; }
    }
}
