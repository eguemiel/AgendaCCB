namespace AgendaCCB.App.Helpers
{
    public static class UrlApi
    {
        private static string Versao { get { return ""; } }
        public static string Login { get { return $"{Versao}Login"; } }
        public static string LoginFacebook { get { return Versao + "Login/Facebook"; } }

        public static string UsuarioCadastro { get { return $"{Versao}Usuario/Cadastrar"; } }
        public static string UsuarioExtrato { get { return Versao + "Usuario/Extrato/{0}"; } }
        public static string UsuarioVoucher { get { return Versao + "Usuario/Voucher/{0}"; } }

        public static string GetMarcas { get { return Versao + "marca/lista/{0}/{1}/"; } }
        public static string GetMarcasNome { get { return Versao + "marca/buscapornome/{0}/{1}/{2}/"; } }
        public static string GetMarcaRegra { get { return Versao + "marcaregra/buscarpormarca/{0}"; } }

        public static string GetRecompensaMarca { get { return Versao + "recompensa/listapormarca/{0}/{1}/{2}/"; } }
        public static string ListaEstabelecimentosPorRecompensa { get { return Versao + "estabelecimento/listaestabelecimentosporrecompensa/{0}/{1}/{2}/"; } }
        public static string ListaEstabelecimentosPorMarca { get { return Versao + "estabelecimento/listaestabelecimentospormarca/{0}/{1}/{2}/"; } }

        public static string PerguntaFrequente { get { return Versao + "perguntafrequente/lista"; } }
        public static string Configuracao { get { return Versao + "configuracao/{0}"; } }
        public static string Resgatar { get { return Versao + "resgate/resgatar/{0}"; } }


    }
}
