using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{

    // Each Invoice includes one or many shippments under the same Consumer 
    // Each Shippment can be only back traced to one Invoice
    public class Invoice
    {
        public bool ModelIsvalid { get; set; }
        public int Id { get; set; }

        [ForeignKey("Id")]
        public Shippment Shippment { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalCharge { get; set; }

        public enum Currency 
        {
            USD,
            CNY,
            NZD
        }
        public enum PaymentMethods
        {
            WeChatPay,
            PayPal,
            AliPay,
            CreditCard,
            BankTransfer,

        }
        public bool IsInvoicePaid { get; set; } = true;

    }
}
