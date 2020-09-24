using iTextSharp.text.pdf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UnitTest
{
    [TestClass]
    public class UnitHTML
    {

        [TestMethod]
        public void TestPDFConvertHTML()
        {

            string fileName = AppDomain.CurrentDomain.BaseDirectory + "//file/";
            var pfile = new Aspose.Pdf.Document(fileName + "444.pdf");
            pfile.Save(fileName + "output.docx", Aspose.Pdf.SaveFormat.DocX);
            pfile.Save(fileName + "output.pptx", Aspose.Pdf.SaveFormat.Pptx);
            pfile.Save(fileName + "output.html", Aspose.Pdf.SaveFormat.Html);

        }


        [TestMethod]
        public void TestPDFToXML()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "//file/";
            PdfReader reader = new PdfReader(fileName + "testo-184-configuration (1).pdf");
            XmlDocument xmlDoc = reader.AcroFields.Xfa.DomDocument;
            xmlDoc.Save(fileName + "test.xml");
        }

    }
}
