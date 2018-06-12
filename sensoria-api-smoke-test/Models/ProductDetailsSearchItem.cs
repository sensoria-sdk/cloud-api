namespace Sensoria.Api.Core.Models
{
    public class ProductDetailsSearchItem
	{
		public string Id { get; set; }
		public string Name { get; set; }
        public string BrandName { get; set; }
        public string ProductUrl { get; set; }
        public string SecureImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
