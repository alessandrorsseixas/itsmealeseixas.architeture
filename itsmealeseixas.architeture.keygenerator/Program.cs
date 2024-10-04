using itsmealeseixas.architeture.utilities.Helpers;

namespace itsmealeseixas.architeture.keygenerator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal());
        }

        public static string ObterChaveSecreta(string tipoToken)
        {
            // Aqui você pode implementar a lógica para retornar a chave secreta
            // com base no tipo de token (homologação, produção, desenvolvimento)
            // Pode ser uma chave armazenada em um arquivo de configuração ou em um servidor seguro.
            // Por simplicidade, estou retornando chaves fixas para os tipos de token.

            switch (tipoToken)
            {
                case "Local":
                    return "U86Xg348Qs4luJhzYj2WkmyCbrfxtPTOdCsoyjYH9qcww8fJXMvRfB5st9XPmvXf";
                case "Develop":
                    return "IQJ8SISAPLsq+QEryYaQKBp7POktlS+Dh1ClWwcvD9hLoP+AUzKCZyexwrGqBXd/nwz11KBo7Uun1WtCOTeRwWdMuU/gA/C/kj/LNdxRmiSzvUOLKyyJ4S01";
                case "Homolog":
                    return null;
                case "Staging":
                    return null;
                case "Production":
                    return null;
                default:
                    return null;
            }
        }

        public static string GenerateKey(string key, string apptoken)
        {
            return UtilsHelpers.Encrypt(key, apptoken);

        }

        public static string DescriptKey(string key, string apptoken)
        {
            return UtilsHelpers.Decrypt(key, apptoken);

        }
    }
}