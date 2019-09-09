using System.Linq;

namespace Mimax.Cache
{
    public class Listing
    {
        private SynchronizedCache<TransactionType, RestrictionCategory> _restrictions =
            new SynchronizedCache<TransactionType, RestrictionCategory>();

        public string Restrictions { get; private set; }

        public void AddRestriction(RestrictedSecurity restrictedSecurity)
        {
            if (restrictedSecurity.RestrictionStatus == RestrictionStatus.Disabled)
                _restrictions.Delete(restrictedSecurity.TransactionType);
            if (restrictedSecurity.RestrictionStatus == RestrictionStatus.Active)
                _restrictions.Add(restrictedSecurity.TransactionType, restrictedSecurity.RestrictionCategory);
            var restrictions = _restrictions.Select(((TransactionType, RestrictionCategory) value) =>
            {
                var (type, category) = value;
                return $"{type}{category}";
            });
            Restrictions = restrictions.Any() ? $"RL Category {string.Join("|", restrictions)}" : string.Empty;
        }

        public RestrictionCategory CheckRestriction(TransactionType transactionType) =>
            _restrictions.Read(transactionType);
    }
}