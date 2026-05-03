using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiShop.IdentityServer.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
        {
            // 1. Payload (Yük) kısmındaki talepleri (Claims) oluşturuyoruz
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(model.Id))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id));
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                claims.Add(new Claim(ClaimTypes.Name, model.UserName));
            }

            if (!string.IsNullOrEmpty(model.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, model.Role));
            }

            // Her token için benzersiz bir ID (JTI) eklemek güvenliği artırır
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            // 2. Security Key ve İmzalama Gereksinimleri
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Token ayarlarını yapılandırıyoruz
            var expireDate = DateTime.Now.AddMinutes(JwtTokenDefaults.ExpireDate);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtTokenDefaults.ValidIssuer,
                audience: JwtTokenDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expireDate,
                signingCredentials: signingCredentials
            );

            // 4. Token'ı string formatına dönüştürüp ViewModel ile dönüyoruz
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponseViewModel(handler.WriteToken(token), JwtTokenDefaults.ExpireDate);
        }
    }
} 