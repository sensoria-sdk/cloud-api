using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sensoria.SmokeTest.Api.Models
{
    public class ClosetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string BrandName { get; set; }
        public bool Selected { get; set; }
        public int? SizeId { get; set; }
        public float? Price { get; set; }
        public System.DateTime? OwnedSince { get; set; }
        public byte? RateComfort { get; set; }
        public byte? RatePrice { get; set; }
        public bool Recommend { get; set; }
        public byte? RateDurability { get; set; }
        public byte? RateAffection { get; set; }
        public byte? RateOverall { get; set; }
        public byte? RateUsageFrequency { get; set; }
        public byte? RateSizeFit { get; set; }
        public byte? RateWidthFit { get; set; }
        public byte? RateArchSupport { get; set; }
        public string Comment { get; set; }
        public decimal? Distance { get; set; }
        public string Tags { get; set; }
        public System.DateTime? RetiredDate { get; set; }
        public string Description { get; set; }
        public string ProductUrl { get; set; }
        public bool? Owned { get; set; }
        public int? UsageCount { get; set; }
        public decimal? ImpactScore { get; set; }
        public decimal? LeftHeelFactor { get; set; }
        public decimal? RightHeelFactor{ get; set; }
        public decimal? HeelFactor { get; set; }

    }

    public class Closet
    {
        public List<ClosetItem> ClosetItems { get; set; }
    }
}
