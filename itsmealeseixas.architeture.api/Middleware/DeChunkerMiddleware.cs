using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.api.Middleware
{
    [ExcludeFromCodeCoverage]
    public class DeChunkerMiddleware
    {
        private readonly RequestDelegate _next;

        public DeChunkerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);

                // Define o comprimento do conteúdo diretamente após a geração do conteúdo
                context.Response.Headers.ContentLength = responseBody.Length;

                // Certifique-se de que o ponteiro do fluxo esteja no início antes de copiar
                responseBody.Seek(0, SeekOrigin.Begin);

                // Copia o corpo da resposta original de volta para o fluxo original
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
