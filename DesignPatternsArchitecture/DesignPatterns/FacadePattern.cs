using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class BillingFacade
    {
        private readonly Accounting _accounting;
        private readonly Billing _billing;
        public BillingFacade(Accounting accounting, Billing billing)
        {
            _accounting = accounting;
            _billing = billing;
        }
        public void StartBilling()
        {
            _billing.GenerateInvoice();
            _accounting.DebitCard();
        }
    }
    public class Accounting
    {
        public void DebitCard()
        {

        }
    }

    public class Billing
    {
        public void GenerateInvoice()
        {

        }
    }
}
