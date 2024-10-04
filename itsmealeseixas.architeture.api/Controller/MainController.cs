using itsmealeseixas.architeture.utilities.Helpers;
using itsmealeseixas.architeture.utilities.Mocks;
using itsmealeseixas.architeture.utilities.Seedworks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using itsmealeseixas.architeture.api.SeedWorks;

namespace itsmealeseixas.architeture.api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors]
    public class MainController : ControllerBase
    {
        private readonly IUrlHelper _urlHelper;
        private Notification notification;
        private List<Notification> notifications;
        public MainController()
        {
        
        }
        //GET: 
        //Retorne 200(OK) para caso de sucesso V
        //Retorne 404 (NOT FOUND) se a entidade não for encontrada V
        //POST: 
        //Retorne 201 (CREATED) para caso um novo recurso seja criado com sucesso V
        //Retorne 400 (BAD REQUEST) caso a solicitação contenha dados inválidos
        //Retorne 422 (Unprocessable Entity) caso a solicitação caia em alguma regra de negócio
        //PUT:
        //Retorne 200 (OK) se for atualizar um recurso existente
        //Retorne 400 (BAD REQUEST) caso a solicitação contenha dados inválidos
        //Considere utilizar 409 (CONFLICT) caso não consiga atualizar um recurso existente
        //DELETE:
        //Retorne 204 (No Content) para sucesso
        //Retorne 404 (NOT FOUND) se a entidade não for encontrada
        //public MainController()
        //{
        //    var notifications = NotificationMock.GetListMocks();
        //}

        //TODO Refactoring
        protected ActionResult CustomResponse([ActionResultStatusCode] int statusCode, [ActionResultObjectValue] object value = null, object errors = null, bool lotOfRegisters = false, string customMessage = "", int messagecode = 0)
        {
            var notifications = NotificationMock.GetListMocks();
            switch (statusCode)
            {
                case 200:

                    if (value != null)
                    {
                        if (lotOfRegisters)
                        {
                            messagecode = messagecode != 0 ? messagecode : 1;
                            notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                            return Ok(new CustomReturn
                            {
                                transactionExecute = true,
                                statusCode = 200,
                                messagecode = notification.CodeNumber,
                                messageTitle = notification.Description,
                                message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                                data = value

                            }
                          );

                        }
                        else
                        {
                            messagecode = messagecode != 0 ? messagecode : 2;
                            notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                            return Ok(new CustomReturn
                            {
                                transactionExecute = true,
                                statusCode = 200,
                                messagecode = notification.CodeNumber,
                                messageTitle = notification.Description,
                                message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                                data = value

                            }
                          );
                        }

                    }
                    else
                    {
                        messagecode = messagecode != 0 ? messagecode : 200;
                        notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                        return Ok(new CustomReturn
                        {
                            transactionExecute = true,
                            statusCode = 200,
                            messagecode = notification.CodeNumber,
                            messageTitle = notification.Description,
                            message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                            data = value

                        }
                      );

                    }
                case 201:
                    messagecode = messagecode != 0 ? messagecode : 201;
                    notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                    return StatusCode(201, new CustomReturn
                    {
                        transactionExecute = true,
                        statusCode = 201,
                        messagecode = notification.CodeNumber,
                        messageTitle = notification.Description,
                        message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                        data = value

                    }
                 );

                //return CreatedAtRoute(notification.Message, new { id = id }, new
                //{
                //    transactionExecute = true,
                //    statusCode = 201,
                //    messagecode = notification.CodeNumber,
                //    messageTitle = notification.Description,
                //    message = notification.Message,
                //    data = value

                //});
                case 204:
                    messagecode = messagecode != 0 ? messagecode : 201;
                    notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                    return StatusCode(204, new CustomReturn
                    {
                        transactionExecute = true,
                        statusCode = 204,
                        messagecode = notification.CodeNumber,
                        messageTitle = notification.Description,
                        message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                        data = value

                    }
                  );
                case 400:
                    if (errors != null)
                    {
                        messagecode = messagecode != 0 ? messagecode : 500;
                        notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                        return StatusCode(400, new CustomReturn
                        {
                            transactionExecute = false,
                            statusCode = 400,
                            messagecode = 999,
                            messageTitle = "Validação de Campos regra de campos do Registro",
                            message = errors,
                            data = value

                        });
                    }
                    else
                    {
                        messagecode = messagecode != 0 ? messagecode : 3;
                        notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                        return StatusCode(400, new CustomReturn
                        {
                            transactionExecute = false,
                            statusCode = 400,
                            messagecode = notification.CodeNumber,
                            messageTitle = notification.Description,
                            message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                            data = value

                        });

                    }
                case 404:
                    messagecode = messagecode != 0 ? messagecode : 4;
                    notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                    return StatusCode(404, new CustomReturn
                    {
                        transactionExecute = false,
                        statusCode = 404,
                        messagecode = notification.CodeNumber,
                        messageTitle = notification.Description,
                        message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                        data = value

                    }
                  );
                case 405:
                    messagecode = messagecode != 0 ? messagecode : 405;
                    notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                    return StatusCode(405, new CustomReturn
                    {
                        transactionExecute = false,
                        statusCode = 405,
                        messagecode = notification.CodeNumber,
                        messageTitle = notification.Description,
                        message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                        data = value

                    }
                  );
                case 409:
                    messagecode = messagecode != 0 ? messagecode : 4;
                    notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                    return StatusCode(409, new CustomReturn
                    {
                        transactionExecute = false,
                        statusCode = 409,
                        messagecode = notification.CodeNumber,
                        messageTitle = notification.Description,
                        message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                        data = value

                    }
                  );
                case 415:

                    if (errors != null)
                    {
                        messagecode = messagecode != 0 ? messagecode : 500;
                        notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                        return StatusCode(415, new CustomReturn
                        {
                            transactionExecute = false,
                            statusCode = 415,
                            messagecode = 999,
                            messageTitle = "Validação de Campos regra de campos do Registro",
                            message = errors,
                            data = value

                        });
                    }
                    else
                    {
                        messagecode = messagecode != 0 ? messagecode : 3;
                        notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                        return StatusCode(415, new CustomReturn
                        {
                            transactionExecute = false,
                            statusCode = 415,
                            messagecode = notification.CodeNumber,
                            messageTitle = notification.Description,
                            message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                            data = value

                        });

                    }
                case 422:

                    if (errors != null)
                    {
                        messagecode = messagecode != 0 ? messagecode : 422;
                        notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                        return StatusCode(422, new CustomReturn
                        {
                            transactionExecute = false,
                            statusCode = 422,
                            messagecode = 999,
                            messageTitle = "Validação de Campos regra de campos do Registro",
                            message = errors,
                            data = value

                        });
                    }
                    else
                    {
                        messagecode = messagecode != 0 ? messagecode : 422;
                        notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                        return StatusCode(422, new CustomReturn
                        {
                            transactionExecute = false,
                            statusCode = 422,
                            messagecode = notification.CodeNumber,
                            messageTitle = notification.Description,
                            message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                            data = value

                        });

                    }
                case 500:

                    messagecode = messagecode != 0 ? messagecode : 500;
                    notification = notifications.Where(x => x.CodeNumber == messagecode).SingleOrDefault();
                    return StatusCode(500, new CustomReturn
                    {
                        transactionExecute = false,
                        statusCode = 500,
                        messagecode = notification.CodeNumber,
                        messageTitle = notification.Description,
                        message = string.IsNullOrEmpty(customMessage) ? notification.Message : customMessage,
                        data = value

                    }
                  );

                default:
                    return Ok();
            }
        }
    }
}
