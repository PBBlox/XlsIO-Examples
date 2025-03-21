# How to create a calculated column in a table.?

Step 1: Create a new C# Console Application project.

Step 2: Name the project.

Step 3: Install the [Syncfusion.XlsIO.Net.Core](https://www.nuget.org/packages/Syncfusion.XlsIO.Net.Core) NuGet package as reference to your .NET Standard applications from [NuGet.org](https://www.nuget.org).

Step 4: Include the following namespaces in the **Program.cs** file.
{% tabs %}  
{% highlight c# tabtitle="C#" %}
using System.IO;
using Syncfusion.XlsIO;
{% endhighlight %}
{% endtabs %}  

Step 5: Include the below code snippet in **Program.cs** to create a calculated column in a table.
{% tabs %}
{% highlight c# tabtitle="C#" %}
using (ExcelEngine excelEngine = new ExcelEngine())
{
	IApplication application = excelEngine.Excel;
	application.DefaultVersion = ExcelVersion.Xlsx;
	IWorkbook workbook = application.Workbooks.Create(1);
	IWorksheet worksheet = workbook.Worksheets[0];

	//Create Table with data in the given range
	IListObject table = worksheet.ListObjects.Create("Table1", worksheet["A1:D3"]);

	//Create data
	worksheet[1, 1].Text = "Products";
	worksheet[1, 2].Text = "Rate";
	worksheet[1, 3].Text = "Quantity";
	worksheet[1, 4].Text = "Total";

	worksheet[2, 1].Text = "Item1";
	worksheet[2, 2].Number = 200;
	worksheet[2, 3].Number = 2;

	worksheet[3, 1].Text = "Item2";
	worksheet[3, 2].Number = 300;
	worksheet[3, 3].Number = 3;

	//Set table formula
	table.Columns[3].CalculatedFormula = "SUM(20,[Rate]*[Quantity])";

	#region Save
	//Saving the workbook
	FileStream outputStream = new FileStream(Path.GetFullPath("Output/CalculatedColumn.xlsx"), FileMode.Create, FileAccess.Write);
	workbook.SaveAs(outputStream);
	#endregion

	//Dispose streams
	outputStream.Dispose();
}
{% endhighlight %}
{% endtabs %}