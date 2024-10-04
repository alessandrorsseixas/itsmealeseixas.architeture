using itsmealeseixas.architeture.utilities.Seedworks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class NotificationMock
    {
        public static List<Notification> GetListMocks()
        {
            List<Notification> mocks = new List<Notification>();

            Notification notification = new Notification(1, "Registros Extraídos com Sucesso", "O Conjunto de Registrados Foi Extraído da base de dado com sucesso", true);
            mocks.Add(notification);
            notification = new Notification(2, "Registro Extraído com Sucesso", "O Registrado Foi Extraído da base de dado com sucesso", true);
            mocks.Add(notification);
            notification = new Notification(3, "Registro Existente", "O Registro já Existe na base de dados", true);
            mocks.Add(notification);
            notification = new Notification(4, "Registro Inexistente", "Não Existe registro na base de dados na base de dados", true);
            mocks.Add(notification);
            notification = new Notification(200, "Atualizado com Sucesso", "Registro Atualizado com sucesso na base de dados", true);
            mocks.Add(notification);
            notification = new Notification(201, "Cadastro com Sucesso", "Registro Cadastrado com sucesso na base de dados", true);
            mocks.Add(notification);
            notification = new Notification(204, "Registro Excluído", "O Registro foi excluído da Base de Dados", true);
            mocks.Add(notification);
            notification = new Notification(405, "Autenticação de usuário", "Usuário e/ou senha incorreto ", true);
            mocks.Add(notification);
            notification = new Notification(409, "Problema na Atualização", "Não foi possível realizar a atualização do registro na base de dados", true);
            mocks.Add(notification);
            notification = new Notification(422, "Regra de négocio", "Validação de regra de negócio da aplicação", true);
            mocks.Add(notification);
            notification = new Notification(500, "Ocorreu um erro inesperado", "O ocorreu um erro não tratado na aplicação e o problema será encaminhado ao time de TI", true);
            mocks.Add(notification);
            return mocks;
        }
    }
}
