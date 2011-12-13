using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MSBuild.NuGet.Tasks.Util;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.NuGet.Tasks
{
    /// <summary>
    /// Creates a NuGet package based on the specified nuspec or project file
    /// </summary>
    public class Pack : Task
    {
        public string NuGetExePath { get; set; }

        public string ManifestFile { get; set; }
        public string ProjectFile { get; set; }

        public string OutputDirectory { get; set; }
        public string BasePath { get; set; }
        public bool Verbose { get; set; }
        public string Version { get; set; }
        public string Exclude { get; set; }
        public bool Symbols { get; set; }
        public bool Tools { get; set; }
        public bool Build { get; set; }
        public bool NoDefaultExcludes { get; set; }
        public string Properties { get; set; }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(ManifestFile) && string.IsNullOrWhiteSpace(ProjectFile))
            {
                Log.LogError("The \"Pack\" task was not given a value for one of the required parameters \"ManifestFile\" or \"ProjectFile\"");
                return false;
            }

            // If not bugetpath is passed in, assume it's registered globally
            if (string.IsNullOrWhiteSpace(NuGetExePath))
                NuGetExePath = "nuget";

            try
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        FileName = NuGetExePath,
                        Arguments = this.ToArgsString()
                    }
                };

                Log.LogMessage("Executing "+ NuGetExePath + " "+ this.ToArgsString() + Environment.NewLine);

                process.Start();
                process.WaitForExit();

                Log.LogMessage("");

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }

        /// <summary>
        /// Converts task into command line args string.
        /// </summary>
        /// <returns></returns>
        public string ToArgsString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("pack \"{0}\"", !string.IsNullOrWhiteSpace(ManifestFile) ? ManifestFile : ProjectFile);

            sb.AppendPathArg("OutputDirectory", OutputDirectory);
            sb.AppendPathArg("BasePath", BasePath);
            sb.AppendStringArg("Version", Version);
            sb.AppendStringArg("Exclude", Exclude);
            sb.AppendStringArg("Properties", Properties);
            sb.AppendBoolArg("Verbose", Verbose);
            sb.AppendBoolArg("Symbols", Symbols);
            sb.AppendBoolArg("Tools", Tools);
            sb.AppendBoolArg("Build", Build);
            sb.AppendBoolArg("NoDefaultExcludes", NoDefaultExcludes);

            return sb.ToString();
        }
    }
}
