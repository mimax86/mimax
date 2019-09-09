namespace Mimax.Cache
{

    public class RestrictedSecurity
    {
        public TransactionType TransactionType { get; set; }

        public RestrictionCategory RestrictionCategory { get; set; }

        public RestrictionStatus RestrictionStatus { get; set; }
    }
}