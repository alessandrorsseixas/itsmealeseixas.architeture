using itsmealeseixas.architeture.utilities.Helpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.api.SeedWorks
{
    public static class TokenManager
    {
        public static string GerarJwt(string appToken,
                                      string secret,
                                      string issuer,
                                      string audience,
                                      string expires,
                                      IList<Claim> claims)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = UtilsHelpers.GenerateSHA256Key(UtilsHelpers.Decrypt(secret, appToken), appToken);


            var token = new JwtSecurityToken(
            issuer: UtilsHelpers.Decrypt(issuer, appToken),
            audience: UtilsHelpers.Decrypt(audience, appToken),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(int.Parse(UtilsHelpers.Decrypt(expires, appToken))),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            token.Header.Add("kid", "JWT");


            return tokenHandler.WriteToken(token);


        }



        public static string GerarJwtPat(string appToken,
                                      string secret,
                                      string issuer,
                                      string audience,
                                      DateTime expires,
                                      IList<Claim> claims)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = UtilsHelpers.GenerateSHA256Key(UtilsHelpers.Decrypt(secret, appToken), appToken);


            var token = new JwtSecurityToken(
            issuer: UtilsHelpers.Decrypt(issuer, appToken),
            audience: UtilsHelpers.Decrypt(audience, appToken),
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            token.Header.Add("kid", "JWT");


            return tokenHandler.WriteToken(token);


        }


        public static string RefreshToken(string appToken, string token, string secret, string issuer, string audience, string expires)
        {
            var key = Encoding.ASCII.GetBytes(UtilsHelpers.Decrypt(secret, appToken));
            //var key = GenerateKey(UtilsHelpers.Decrypt(secret, appToken), appToken);
            var securityKey = new SymmetricSecurityKey(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            // Validate token
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidIssuer = UtilsHelpers.Decrypt(issuer, appToken),
                ValidAudience = UtilsHelpers.Decrypt(audience, appToken)
            };

            ClaimsPrincipal principal;
            SecurityToken validatedToken;

            try
            {
                principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch
            {
                // Token validation failed
                return null;
            }

            // Generate new token with the same claims but a new expiration time
            //var claims = principal.Claims;
            //var newToken = new JwtSecurityToken(
            //    issuer: UtilsHelpers.Decrypt(issuer, appToken),
            //    audience: UtilsHelpers.Decrypt(audience, appToken),
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddHours(int.Parse(UtilsHelpers.Decrypt(expires, appToken))),
            //    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            //);

            var sctoken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = UtilsHelpers.Decrypt(issuer, appToken),
                Audience = UtilsHelpers.Decrypt(audience, appToken),
                Expires = DateTime.UtcNow.AddHours(int.Parse(UtilsHelpers.Decrypt(expires, appToken))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            // Adiciona o KeyId (kid) ao novo token
            ///newToken.Header.Add("kid", appToken);

            return tokenHandler.WriteToken(sctoken);
        }


    }
}
