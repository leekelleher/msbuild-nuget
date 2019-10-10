using System;
using System.IO;
using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using MSBuild.NuGet.Tasks.Util;

namespace MSBuild.NuGet.Tasks
{
    /// <summary>
    /// Updates a nuspec manifest file.
    /// </summary>
    public class ManifestUpdate : Task
    {
        [Required]
        public string ManifestFile { get; set; }

        [Required]
        public string WorkingDirectory { get; set; }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Authors { get; set; }
        public string Owners { get; set; }
        public string Copyright { get; set; }
        public string RequireLicenseAcceptance { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ReleaseNotes { get; set; }
        public string Language { get; set; }
        public string ProjectUrl { get; set; }
        public string IconUrl { get; set; }
        public string LicenseUrl { get; set; }

        public ITaskItem[] Files { get; set; }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            try
            {
                // Load manifest
                var doc = new XmlDocument { PreserveWhitespace = true };
                doc.Load(ManifestFile);

                Log.LogMessage("Updating manifest " + ManifestFile);

                // Grab the namespace declaration from the manifest file
                var xmlns = doc.DocumentElement.Attributes["xmlns"].Value;

                // Instantiate an XmlNamespaceManager object. 
                var xmlnsManager = new XmlNamespaceManager(doc.NameTable);

                // Add the namespaces used in manifest to the XmlNamespaceManager.
                xmlnsManager.AddNamespace("nu", xmlns);

                // Update package metadata
                doc.UpdateNode(Log, xmlnsManager, Constants.ID_XPATH, Id);
                doc.UpdateNode(Log, xmlnsManager, Constants.TITLE_XPATH, Title);
                doc.UpdateNode(Log, xmlnsManager, Constants.VERSION_XPATH, Version);
                doc.UpdateNode(Log, xmlnsManager, Constants.AUTHORS_XPATH, Authors);
                doc.UpdateNode(Log, xmlnsManager, Constants.OWNERS_XPATH, Owners);
                doc.UpdateNode(Log, xmlnsManager, Constants.COPYRIGHT_XPATH, Copyright);
                doc.UpdateNode(Log, xmlnsManager, Constants.REQUIRE_LICENSE_ACCEPTANCE_XPATH, RequireLicenseAcceptance);
                doc.UpdateNode(Log, xmlnsManager, Constants.DESCRIPTION_XPATH, Description);
                doc.UpdateNode(Log, xmlnsManager, Constants.SUMMARY_XPATH, Summary);
                doc.UpdateNode(Log, xmlnsManager, Constants.RELEASE_NOTES_XPATH, ReleaseNotes);
                doc.UpdateNode(Log, xmlnsManager, Constants.LANGUAGE_XPATH, Language);
                doc.UpdateNode(Log, xmlnsManager, Constants.PROJECT_URL_XPATH, ProjectUrl);
                doc.UpdateNode(Log, xmlnsManager, Constants.ICON_URL_XPATH, IconUrl);
                doc.UpdateNode(Log, xmlnsManager, Constants.LICENSE_URL_XPATH, LicenseUrl);

                // Append files
                if (Files != null && Files.Length > 0)
                {
                    Log.LogMessage("Adding "+ Files.Length + " files to Manifest");

                    var filesNode = doc.SelectSingleNode(Constants.FILES_XPATH, xmlnsManager);
                    if (filesNode != null)
                    {
                        foreach (var file in Files)
                        {
                            if (!File.Exists(file.ItemSpec)) 
                                continue;

                            var fileInfo = new FileInfo(file.ItemSpec);
                            if (!fileInfo.FullName.StartsWith(WorkingDirectory)) 
                                continue;

                            Log.LogMessage("Adding file " + fileInfo.FullName);

                            var filePath = fileInfo.FullName.Substring(WorkingDirectory.Length + 1);
                            var fileRootDir = filePath.LastIndexOf(@"\") >= 0 ? filePath.Substring(0, filePath.LastIndexOf(@"\")) : "";
                            var fileNode = doc.CreateFileNode(Log, xmlns, filePath, fileRootDir);

                            filesNode.AppendChild(fileNode);
                        }
                    }
                }

                // Save manifest
                doc.Save(ManifestFile);

                Log.LogMessage("Manifest saved to " + ManifestFile);

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }

        }
    }
}
