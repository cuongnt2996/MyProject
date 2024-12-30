using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Services;
public class Helper
{
    public static string RandomString(int len = 32)
    {
        string pattern = "1234567890qwertyuiopasdfghjklzxcvbnm";
        char[] arr = new char[len];
        Random random = new Random();
        for (int i = 0; i < len; i++)
        {
            arr[i] = pattern[random.Next(0, pattern.Length)];
        }
        return string.Join(string.Empty, arr);
    }
    public static string HashPassword(string password, string salt)
    {
        HashAlgorithm algorithm = SHA512.Create();
        return Convert.ToBase64String(algorithm.ComputeHash(Encoding.ASCII.GetBytes(password + salt)));
    }
    public static string? Upload(string root, IFormFile f, int len = 32){
        if(f!=null){
            string ext = Path.GetExtension(f.FileName);
            string fileName= RandomString(len - ext.Length) + ext;
            using(Stream stream = new FileStream(Path.Combine(root, fileName), FileMode.Create)){
                f.CopyTo(stream);
            }
            return fileName;
        }
        return null;
    }
    public static string? Upload(IFormFile f, int len = 32){
        string root = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images");
        return Upload(root,f,len);
    }
    public static bool Delete(string root, string fileName){
        string path = Path.Combine(root,fileName);
        if(File.Exists(path)){
            try{
                File.Delete(path);
                return true;
            }catch (Exception ex){
                System.Console.WriteLine(ex.Message);
                return false;
            }
        }
        return false;
    }
    public static bool Delete(string fileName){
        return Delete(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images"),fileName);
    }
    public static string GenerateToken(IEnumerable<Claim> claims, string secretKey){
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken token = new JwtSecurityToken(issuer: "cse.net.vn", audience: "cse.net.vn", claims: claims, signingCredentials: credentials, expires: DateTime.Now.AddHours(2));
        return handler.WriteToken(token);
    }
    public static string HmaxSHA(string key, string plaintext){
        using(HMACSHA512 hmac = new HMACSHA512(Encoding.ASCII.GetBytes(key))){
            return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(plaintext)));
        }
    }

}