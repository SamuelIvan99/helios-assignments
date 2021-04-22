namespace TradeProcessing.DataAceess
{
    public static class DbInitializer
    {
        public static void Initialize(HeliosContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
