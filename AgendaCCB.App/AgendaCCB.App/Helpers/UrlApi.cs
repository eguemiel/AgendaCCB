namespace AgendaCCB.App.Helpers
{
    public static class UrlApi
    {
        private static string Version { get { return ""; } }
        public static string Login { get { return $"{Version}Login"; } }
        public static string GetCollaborators { get { return $"{Version}Collaborator"; } }
        public static string GetCollaborator { get { return Version + "Collaborator/{0}"; } }
        public static string GetUsefulPhones { get { return $"{Version}UsefulPhone"; } }
        public static string GetPhoneCongregation { get { return $"{Version}PhoneCongregation"; } }
    }
}
