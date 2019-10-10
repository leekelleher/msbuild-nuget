namespace MSBuild.NuGet.Tasks
{
    public class Constants
    {
        // XPaths
        internal const string ID_XPATH = "/nu:package/nu:metadata/nu:id";
        internal const string TITLE_XPATH = "/nu:package/nu:metadata/nu:title";
        internal const string VERSION_XPATH = "/nu:package/nu:metadata/nu:version";
        internal const string AUTHORS_XPATH = "/nu:package/nu:metadata/nu:authors";
        internal const string OWNERS_XPATH = "/nu:package/nu:metadata/nu:owners";
        internal const string COPYRIGHT_XPATH = "/nu:package/nu:metadata/nu:owners";
        internal const string REQUIRE_LICENSE_ACCEPTANCE_XPATH = "/nu:package/nu:metadata/nu:requireLicenseAcceptance";
        internal const string DESCRIPTION_XPATH = "/nu:package/nu:metadata/nu:description";
        internal const string SUMMARY_XPATH = "/nu:package/nu:metadata/nu:summary";
        internal const string RELEASE_NOTES_XPATH = "/nu:package/nu:metadata/nu:releaseNotes";
        internal const string LANGUAGE_XPATH = "/nu:package/nu:metadata/nu:language";
        internal const string PROJECT_URL_XPATH = "/nu:package/nu:metadata/nu:projectUrl";
        internal const string ICON_URL_XPATH = "/nu:package/nu:metadata/nu:iconUrl";
        internal const string LICENSE_URL_XPATH = "/nu:package/nu:metadata/nu:licenseUrl";

        internal const string DEPENDENCIES_XPATH = "/nu:package/nu:metadata/nu:dependencies";
        internal const string FRAMEWORK_ASSEMBLIES_XPATH = "/nu:package/nu:metadata/nu:frameworkAssemblies";
        internal const string REFERENCES_XPATH = "/nu:package/nu:metadata/nu:references";

        internal const string FILES_XPATH = "/nu:package/nu:files";
    }
}
