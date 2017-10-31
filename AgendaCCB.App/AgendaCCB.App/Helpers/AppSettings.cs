namespace AgendaCCB.App.Helpers
{
    public static class AppSettings
    {
        public static string[] ReadPermissions = new string[] {
            "public_profile", "email"
        };

        public static string[] PublishPermissions = new string[] {
            "publish_actions"
        };

        public static string TextoAguarde { get { return "Aguarde..."; } }

        public static string AppName  { get { return "Agenda CCB"; }}
        public static string TelaInicial { get { return "PaginaPrincipalBottomBar"; }}
    }
}