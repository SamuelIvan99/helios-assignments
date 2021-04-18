namespace PowerDemandDataFeed.Model
{
    public class ResponseMetadata
    {
        public int HttpCode { get; set; }

        public string ErrorType { get; set; }

        public string Description { get; set; }

        public string CappingApplied { get; set; }

        public int CappingLimit { get; set; }

        public string QueryString { get; set; }
    }
}