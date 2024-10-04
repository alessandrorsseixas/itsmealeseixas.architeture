using Newtonsoft.Json;
using NodaTime;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.Helpers
{
    public static class UtilsHelpers
    {

        private const int SALT_SIZE = 16; // 128 bit 
        private const int KEY_SIZE = 32; // 256 bit
        private const int INTERATIONS = 10000;
        private const int KeySize = 256;
        private const int BlockSize = 128;
        private static readonly Random random = new Random();

        /// <summary>
        /// Gera um id para o objeto a partir dos itens enviados usando o algortímo de MD5 hash. Se não forem fornecidor items, o id será randômico utilizando GUID
        /// </summary>
        /// <param name="itens"></param>
        /// <returns></returns>
        public static string GenerateId(params string[] itens)
        {
            if (itens.Length == 0)
            {
                return Guid.NewGuid().ToString().Replace("-", "").ToLower();
            }
            else
            {
                return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Join(":", itens)))).Replace("-", "").ToLower();
            }
        }

        public static byte[] GenerateSHA256Key(string secret, string appToken)
        {
            byte[] key;
            using (var sha256 = SHA256.Create())
            {
                key = sha256.ComputeHash(Encoding.UTF8.GetBytes(secret + appToken));
            }
            return key;
        }

        public static DateTime GetDatetime()
        {

            DateTime localDateTime = DateTime.Now;
            return localDateTime.ToUniversalTime();
        }

        public static DateTime GetDatetimeUtc()
        {

            DateTime localDateTime = DateTime.UtcNow;
            return localDateTime.ToUniversalTime();
        }
        public static string GetPreviousMonthIfFirstDay(DateTime today)
        {
            // Obtém o mês anterior
            DateTime previousMonth = today.AddMonths(-1);
            // Retorna a data no formato MM/yyyy
            return previousMonth.ToString("MM/yyyy");
        }

        public static Guid GenerateId()
        {
            return Guid.NewGuid();
        }



        /// <summary>
        /// Gerar hash da senha passada.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>hash da senha</returns>
        public static string HashPassword(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(password, SALT_SIZE, INTERATIONS, HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(KEY_SIZE));
            var salt = Convert.ToBase64String(algorithm.Salt);
            return $"{INTERATIONS}.{salt}.{key}";
        }

        /// <summary>
        /// Verificar se a senha é equivalente ao hash.
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="password"></param>
        /// <returns>true se a senha é equivalente ao hash, false caso a validação naõ tenha sucesso.</returns>
        public static bool CheckPassword(string hash, string password)
        {
            var parts = hash.Split('.', 3);
            if (parts.Length != 3)
            {
                // TODO: log "Unexpected hash format. Should be formatted as `{iterations}.{salt}.{hash}`"
                return false;
            }
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            using var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(KEY_SIZE);
            var verified = keyToCheck.SequenceEqual(key);
            return verified;
        }

        /// <summary>
        /// Verificar se o CPF do usuário de válido
        /// </summary>
        /// <param name="vrCPF"></param>
        /// <returns>true se o cpf é válido, false caso a validação naõ tenha sucesso.</returns>
        public static bool CheckCPF(string cpf)
        {
            if (cpf == null) return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);

        }

        /// <summary>
        /// Verificar se o CPF do usuário de válido
        /// </summary>
        /// <param name="vrCPF"></param>
        /// <returns>true se o cpf é válido, false caso a validação naõ tenha sucesso.</returns>
        public static bool CheckCNPJ(string cnpj)
        {

            if (cnpj == null) return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }


        public static bool BeAValidePT_BRDateShortDate(string date)
        {

            if (DateTime.TryParse(date, new CultureInfo("pt-BR"), DateTimeStyles.None, out var data))
                return true;
            return false;
        }

        public static bool BeTimeReferenceValid(string timeRef)
        {
            return DateTime.TryParseExact(timeRef, "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }


        public static bool CompairAValideDate(DateTime initialDate, DateTime finalDate)
        {
            if (finalDate > initialDate)
                return true;


            return false;
        }

        public static bool CompairStringValideDate(string initialDate, string finalDate)
        {
            if (DateTime.Parse(finalDate) > DateTime.Parse(initialDate))
                return true;


            return false;
        }

        public static DateTime? GetValidDateTime(string input)
        {
            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static bool BeGuidValidate(string code)
        {
            if (Guid.TryParse(code, out var data))
                return true;
            return false;
        }

        public static bool BeNumberValidate(string number)
        {
            if (float.TryParse(number, out var data))
                return true;
            return false;
        }

        public static bool CheckBrazilianPostalCode(string postalcode)
        {

            if (postalcode == null) return false;

            Regex Rgx = new Regex(@"^\d{5}-\d{3}$");

            if (!Rgx.IsMatch(postalcode))
                return false;
            else
                return true;
        }
        public static bool CheckBrazilianStates(string state)
        {
            if (string.IsNullOrEmpty(state)) return false;
            var states = GetBrazilianStates();
            if (!states.ContainsKey(state)) return false;
            return true;
        }

        public static string GetTimeZoneByUF(string uf)
        {
            // Carrega o banco de dados de fusos horários
            var tzdb = DateTimeZoneProviders.Tzdb;

            // Mapeamento das UF para os fusos horários correspondentes
            var ufFusoHorario = new System.Collections.Generic.Dictionary<string, string>
        {
            {"AC", "America/Rio_Branco"},
            {"AL", "America/Maceio"},
            {"AP", "America/Belem"},
            {"AM", "America/Manaus"},
            {"BA", "America/Bahia"},
            {"CE", "America/Fortaleza"},
            {"DF", "America/Sao_Paulo"},
            {"ES", "America/Sao_Paulo"},
            {"GO", "America/Sao_Paulo"},
            {"MA", "America/Fortaleza"},
            {"MT", "America/Cuiaba"},
            {"MS", "America/Campo_Grande"},
            {"MG", "America/Sao_Paulo"},
            {"PA", "America/Belem"},
            {"PB", "America/Fortaleza"},
            {"PR", "America/Sao_Paulo"},
            {"PE", "America/Recife"},
            {"PI", "America/Fortaleza"},
            {"RJ", "America/Sao_Paulo"},
            {"RN", "America/Fortaleza"},
            {"RS", "America/Sao_Paulo"},
            {"RO", "America/Porto_Velho"},
            {"RR", "America/Boa_Vista"},
            {"SC", "America/Sao_Paulo"},
            {"SP", "America/Sao_Paulo"},
            {"SE", "America/Maceio"},
            {"TO", "America/Araguaina"}
        };

            // Verifica se a UF está no mapeamento
            if (ufFusoHorario.TryGetValue(uf.ToUpper(), out string fusoHorarioId))
            {
                // Obtém o objeto DateTimeZone correspondente ao ID do fuso horário
                var fusoHorarioObj = tzdb.GetZoneOrNull(fusoHorarioId);

                // Verifica se o fuso horário foi encontrado
                if (fusoHorarioObj != null)
                {
                    return fusoHorarioObj.Id;
                }
            }

            return null;
        }
        public static Dictionary<string, string> GetBrazilianStates()
        {
            Dictionary<string, string> brazilianStates = new Dictionary<string, string>();
            brazilianStates.Add("AC", "Acre");
            brazilianStates.Add("AL", "Alagoas");
            brazilianStates.Add("AP", "Amapá");
            brazilianStates.Add("AM", "Amazonas");
            brazilianStates.Add("BA", "Bahia");
            brazilianStates.Add("CE", "Ceará");
            brazilianStates.Add("DF", "Distrito Federal");
            brazilianStates.Add("ES", "Espírito Santo");
            brazilianStates.Add("GO", "Goiás");
            brazilianStates.Add("MA", "Maranhão");
            brazilianStates.Add("MT", "Mato Grosso");
            brazilianStates.Add("MS", "Mato Grosso do Sul");
            brazilianStates.Add("MG", "Minas Gerais");
            brazilianStates.Add("PA", "Pará");
            brazilianStates.Add("PB", "Paraíba");
            brazilianStates.Add("PR", "Paraná");
            brazilianStates.Add("PE", "Pernambuco");
            brazilianStates.Add("PI", "Piauí");
            brazilianStates.Add("RJ", "Rio de Janeiro");
            brazilianStates.Add("RN", "Rio Grande do Norte");
            brazilianStates.Add("RS", "Rio Grande do Sul");
            brazilianStates.Add("RO", "Rondônia");
            brazilianStates.Add("RR", "Roraima");
            brazilianStates.Add("SC", "Santa Catarina");
            brazilianStates.Add("SP", "São Paulo");
            brazilianStates.Add("SE", "Sergipe");
            brazilianStates.Add("TO", "Tocantins");
            return brazilianStates;
        }
        public static string ConverteObjectParaJSon<T>(T obj)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(obj);
                return jsonString;
            }
            catch
            {
                throw;
            }
        }
        public static T ConverteJSonParaObject<T>(string jsonString)
        {
            try
            {
                //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                //T obj = (T)serializer.ReadObject(ms);


                var item = JsonConvert.DeserializeObject<T>(jsonString);
                return item;
            }
            catch
            {
                throw;
            }
        }

        public static string GenerateUniqueCustomerCode()
        {
            Random random = new Random();
            int clientNumber = random.Next(100000, 1000000);

            // Calculate the check digit
            int checkDigit = CalculateCustomerCodeCheckDigit(clientNumber);

            // Combine the client number and check digit to form the final client code
            string clientCode = $"{clientNumber:D6}-{checkDigit}";

            return clientCode;


        }
        private static int CalculateCustomerCodeCheckDigit(int clientNumber)
        {
            // Convert the client number to an array of digits
            int[] digits = new int[6];
            for (int i = 0; i < 6; i++)
            {
                digits[i] = clientNumber % 10;
                clientNumber /= 10;
            }

            // Calculate the weighted sum of the digits
            int weightedSum = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    digits[i] *= 2;
                    if (digits[i] > 9)
                    {
                        digits[i] -= 9;
                    }
                }
                weightedSum += digits[i];
            }

            // Calculate the check digit
            int checkDigit = (10 - (weightedSum % 10)) % 10;

            return checkDigit;
        }
        public static string GenerateUniqueCustomerToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] codeArray = new char[6];

            for (int i = 0; i < 6; i++)
            {
                codeArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(codeArray);

            //var random = new Random();
            //var accountNumberBase = $"{random.Next(10000000, 99999999)}"; // Número de conta de 8 dígitos
            //var checksum = CalculateChecksum(accountNumberBase);
            //return $"{accountNumberBase}-{checksum}";
        }

        public static bool ValidateCustomerToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || token.Length != 6)
            {
                return false;
            }

            // Use a regular expression to check if the code contains only alphanumeric characters
            return Regex.IsMatch(token, "^[a-zA-Z0-9]+$");
        }

        public static bool ValidateCustomerCode(string clientCode)
        {

            if (string.IsNullOrWhiteSpace(clientCode))
            {
                return false;

            }
            // Remover qualquer caractere não numérico
            var cleanAccountNumber = new string(clientCode.Where(char.IsDigit).ToArray());

            if (cleanAccountNumber.Length != 7 || !int.TryParse(cleanAccountNumber, out int clientNumber))
            {
                return false;
            }


            // Extract the client number and check digit from the input
            int inputClientNumber = int.Parse(cleanAccountNumber.Substring(0, 6));
            int inputCheckDigit = int.Parse(cleanAccountNumber.Substring(6, 1));

            // Calculate the check digit for the extracted client number
            int calculatedCheckDigit = CalculateCustomerCodeCheckDigit(inputClientNumber);

            // Validate the check digit
            return inputCheckDigit == calculatedCheckDigit;
        }

        public static string Encrypt(string plainText, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(password);
                aesAlg.Mode = CipherMode.CFB; // Escolha o modo de cifragem desejado
                aesAlg.BlockSize = BlockSize;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(password);
                aesAlg.Mode = CipherMode.CFB; // Use o mesmo modo usado durante a criptografia
                aesAlg.BlockSize = BlockSize;

                if (!string.IsNullOrEmpty(cipherText))
                {
                    byte[] fullCipher = Convert.FromBase64String(cipherText);
                    aesAlg.IV = fullCipher.Take(BlockSize / 8).ToArray();

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    try
                    {
                        using (MemoryStream msDecrypt = new MemoryStream(fullCipher.Skip(BlockSize / 8).ToArray()))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                {
                                    return srDecrypt.ReadToEnd();
                                }
                            }
                        }
                    }
                    catch (CryptographicException ex)
                    {
                        // Lidar com a exceção de padding inválido ou outra exceção criptográfica, se necessário
                        Console.WriteLine($"Erro de descriptografia: {ex.Message}");
                        return null;
                    }

                }
                else
                {

                    return null;
                }

            }
        }

        private static byte[] GenerateKey(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static string GenerateMfaCode()
        {
            const string chars = "0123456789";
            char[] codeArray = new char[6];

            for (int i = 0; i < 6; i++)
            {
                codeArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(codeArray);
        }

        public static string GenerateUniqueAccountNumber()
        {
            var random = new Random();
            var accountNumberBase = $"{random.Next(10000000, 99999999)}"; // Número de conta de 8 dígitos
            var checksum = CalculateChecksum(accountNumberBase);
            return $"{accountNumberBase}-{checksum}";
        }

        // Método para calcular o dígito verificador usando o algoritmo de Luhn
        private static int CalculateChecksum(string number)
        {
            var reversedDigits = number.Reverse().Select(c => int.Parse(c.ToString())).ToArray();
            var sum = 0;

            for (var i = 0; i < reversedDigits.Length; i++)
            {
                var digit = reversedDigits[i];

                if (i % 2 == 1)
                {
                    digit *= 2;
                    digit = digit > 9 ? digit - 9 : digit;
                }

                sum += digit;
            }

            return (sum * 9) % 10;
        }

        public static bool ValidateAccountNumber(string accountNumber)
        {

            if (string.IsNullOrEmpty(accountNumber)) return false;
            // Remover qualquer caractere não numérico
            var cleanAccountNumber = new string(accountNumber.Where(char.IsDigit).ToArray());

            if (cleanAccountNumber.Length < 9 || cleanAccountNumber.Length > 10)
            {
                // Número de conta inválido
                return false;
            }

            var numberBase = cleanAccountNumber.Substring(0, cleanAccountNumber.Length - 1);
            var checksum = int.Parse(cleanAccountNumber.Substring(cleanAccountNumber.Length - 1));

            return CalculateChecksum(numberBase) == checksum;
        }

        public static string GenerateUniqueDocumentProtocol(string documentName)
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            string hash = CalculateHash(documentName);

            string protocol = $"{timestamp}-{hash}";

            return protocol;
        }
        private static string CalculateHash(string input)
        {
            // Implemente a lógica de hash adequada para o seu caso.
            // Aqui, estamos usando SHA256 como exemplo.

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static bool ValidateProtocol(string protocol)
        {
            // Extrai o timestamp e o hash do protocolo.
            string[] partes = protocol.Split('-');

            if (partes.Length == 2)
            {
                string timestamp = partes[0];
                string hash = partes[1];

                // Verifica se o timestamp está dentro de um intervalo aceitável (por exemplo, nos últimos 24 horas).
                if (ValidateTimestamp(timestamp))
                {
                    // Implemente a lógica de validação do hash conforme necessário.
                    // Aqui, estamos usando uma validação simples para fins de exemplo.
                    return hash.Length == 64; // Verifica se o hash tem o comprimento esperado para SHA256.
                }
            }

            return false;
        }

        private static bool ValidateTimestamp(string timestamp)
        {
            // Implemente a lógica de validação do timestamp conforme necessário.
            // Aqui, estamos verificando se o timestamp está dentro dos últimos 24 horas.

            if (DateTime.TryParseExact(timestamp, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
            {
                TimeSpan diferenca = DateTime.UtcNow - data;
                return diferenca.TotalHours <= 24;
            }

            return false;
        }

        public static bool ValidateBase64String(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) return false;
                // Tenta decodificar a string em Base64.
                byte[] data = Convert.FromBase64String(input);
                return true; // A decodificação foi bem-sucedida, a string é Base64 válida.
            }
            catch (FormatException)
            {
                return false; // A string não é válida em Base64.
            }
        }

        public static void Base64ToExcel(string base64String, string outputPath)
        {
            try
            {
                byte[] data = Convert.FromBase64String(base64String);

                using (MemoryStream stream = new MemoryStream(data))
                {
                    IWorkbook workbook = new XSSFWorkbook(stream);

                    // Realize qualquer validação ou manipulação adicional necessária aqui.
                    // Por exemplo, você pode adicionar verificações específicas do Excel.

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fileStream);
                    }
                }
            }
            catch (FormatException ex)
            {
                // Lidar com o caso em que a string não é válida em Base64.
                throw new ArgumentException("A string não é uma representação válida em Base64.", ex);
            }
            catch (Exception ex)
            {
                // Lidar com outros erros, se necessário.
                throw new ApplicationException("Erro ao converter a string para Excel.", ex);
            }
        }

        public static string GenerateUniqueContractNumber()
        {
            // Generate a unique contract number based on a combination of timestamp and random digits.
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            string randomDigits = random.Next(10000, 99999).ToString(); // Adjust the range as needed.

            return $"{timestamp}-{randomDigits}";
        }

        private static readonly HashSet<string> KnownContractNumbers = new HashSet<string>();

        public static bool ValidateUniqueContractNumber(string contractNumber)
        {
            if (string.IsNullOrWhiteSpace(contractNumber)) return false;


            lock (KnownContractNumbers)
            {
                // Simulate checking against a collection of known contract numbers.
                // In a real-world scenario, you would perform a database query or use another source.
                if (KnownContractNumbers.Contains(contractNumber)) return false;


                // Add the contract number to the collection to mark it as used.
                KnownContractNumbers.Add(contractNumber);
            }

            return true; // Contract number is unique.
        }



        //Datas Métodos



    }
}
