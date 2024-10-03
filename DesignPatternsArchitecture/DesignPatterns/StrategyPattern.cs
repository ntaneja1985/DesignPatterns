using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
   public interface ITaxDef
    {
        decimal Calculate(decimal amount);
    }

    public class GstTaxDef : ITaxDef
    {
        public decimal Calculate(decimal amount)
        {
            throw new NotImplementedException();
        }
    }

    public class VatTaxDef : ITaxDef
    {
        public decimal Calculate(decimal amount)
        {
            throw new NotImplementedException();
        }
    }

    public class TaxContext
    {
        private ITaxDef _taxDef;
        public TaxContext(ITaxDef taxDef)
        {
            _taxDef = taxDef;
        }

        public void CalculateTax(decimal amount)
        {
            _taxDef.Calculate(amount);
        }
    }
}
