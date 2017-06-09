using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectBase.Utils.FileImport;

namespace ProjectBase.WebTester
{
    public partial class TestCSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CsvReader csvReader = new CsvReader(@"C:\ImportedFiles\Libro.csv", 15, true, this.Separator.Text[0]);
            int count = 0;
            while(!csvReader.AtEndOfFile)
            {
                string[] arr = csvReader.ReadLine();
                if (arr != null)
                    Response.Write(string.Join(",", arr) + "<br/>");
                else
                    Response.Write("Nulo<br/>");
                count++;
            }
            csvReader.Dispose();

            Response.Write(count);
        }
    }
}
