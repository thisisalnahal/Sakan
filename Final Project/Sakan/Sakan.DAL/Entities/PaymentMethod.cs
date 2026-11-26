using Sakan.DAL.Enums.PaymentMethod;

namespace Sakan.DAL.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public MethodType MethodType { get; set; }
        public int? ExpiryMonth { get; set; }
        public int? ExpiryYear { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }

        //nav. prop.
        public ApplicationUser User { get; set; } = null!;
    }
}
