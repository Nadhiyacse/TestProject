using System;
using System.IO;
using TechTalk.SpecFlow;

namespace Automation_Framework.Helpers
{
    public static class FileHelper
    {
        private const string DOWNLOADS_FOLDER_NAME = "Downloads";
        private const string DATA_FOLDER = "DataFiles";
        private const string IMPORT_TEMPLATE_FOLDER = "ImportTemplateData";
        private const string IMAGES = "Images";
        private const string DOCUMENT_FOLDER = "Documents";
        private const string IMPORT_RATECARD_FOLDER = "ImportRatecardData";
        private const string IMPORT_CLASSIFICATIONFILTER_FOLDER = "ImportClassificationFilterData";

        public static string GetWorkingDirectoryPath()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var workingDirectory = baseDirectory.Substring(0, baseDirectory.LastIndexOf("bin"));
            return workingDirectory;
        }

        public static string GetImportMediaScheduleFilePath(string featureTitle)
        {
            var workingDirectory = GetWorkingDirectoryPath();
            var dataFilePath = string.Format(Path.Combine(workingDirectory, DATA_FOLDER, IMPORT_TEMPLATE_FOLDER, featureTitle)) + ".xlsx";
            return dataFilePath;
        }

        public static string GetImportClassificationFilterFilePath(string featureTitle)
        {
            var workingDirectory = GetWorkingDirectoryPath();
            var dataFilePath = string.Format(Path.Combine(workingDirectory, DATA_FOLDER, IMPORT_CLASSIFICATIONFILTER_FOLDER, featureTitle)) + ".xlsx";
            return dataFilePath;
        }

        public static string GetImageFilePath(string imageName)
        {
            var workingDirectory = GetWorkingDirectoryPath();
            var imagesFilePath = string.Format(Path.Combine(workingDirectory, DATA_FOLDER, IMAGES, imageName));
            return imagesFilePath;
        }

        public static string GetDownloadsFolderPath()
        {
            var downloadsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DOWNLOADS_FOLDER_NAME);

            if (!Directory.Exists(downloadsFolderPath))
            {
                downloadsFolderPath = Directory.CreateDirectory(downloadsFolderPath).FullName;
            }

            return downloadsFolderPath;
        }

        public static bool FileExists(string directory, string fileName)
        {
            return File.Exists(Path.Combine(directory, fileName));
        }

        public static string GetDocumentFilePath(string fileName)
        {
            var workingDirectory = GetWorkingDirectoryPath();
            var dataFilePath = string.Format(Path.Combine(workingDirectory, DATA_FOLDER, DOCUMENT_FOLDER, fileName));
            return dataFilePath;
        }

        public static string GetImportRatecardFilePath(string fileName)
        {
            var workingDirectory = GetWorkingDirectoryPath();
            var dataFilePath = string.Format(Path.Combine(workingDirectory, DATA_FOLDER, IMPORT_RATECARD_FOLDER, fileName));
            return dataFilePath;
        }
    }
}
