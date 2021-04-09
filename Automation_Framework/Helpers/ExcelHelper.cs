using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace Automation_Framework.Helpers
{
    public static class ExcelHelper
    {
        public static void SaveFileWithoutChanges(string fileName)
        {
            var excelApp = new Application();
            var filePath = string.Format(Path.Combine(FileHelper.GetDownloadsFolderPath(), fileName));
            var excelWorkbook = excelApp.Workbooks.Open(filePath);
            excelWorkbook.Save();
            excelWorkbook.Close();
            excelApp.Quit();
        }
    }
}
