using System.Runtime.Serialization;

namespace ChangeApi.Dto
{
    [DataContract]
    public class CalculateDto
    {
        [DataMember(Name = "amountPaid")]
        public decimal AmountPaid { get; set; }

        [DataMember(Name = "totalAmountToPay")]
        public decimal TotalAmountToPay { get; set; }
    }
}
