using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface IExport
    {
        void Export();
    }
    public class WordExport : IExport
    {
        public void Export()
        {
            throw new NotImplementedException();
        }
    }
    public class ExcelExport : IExport
    {
        public void Export()
        {
            throw new NotImplementedException();
        }
    }


    //Third Party DLL
    //Incompatible with IExport interface
    public class PdfExport
    {
        public void Save()
        {
            throw new NotImplementedException();
        }
    }

    //We need to make PdfExport work with IExport interface
    //Object Adapter
    //Make a call to instance of Pdf Export class
    //Wrapper
    public class PdfObjectAdapter : IExport
    {
        //Internally it calls save
        public void Export()
        {
            PdfExport c = new PdfExport();
            c.Save();
        }
    }

    //Class Adapter using inheritance
    public class PdfClassAdapter : PdfExport, IExport
    {
        public void Export()
        {
            this.Save();
        }
    }
}
