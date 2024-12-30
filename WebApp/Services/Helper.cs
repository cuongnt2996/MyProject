using System.Security.Cryptography;
using System.Text;

namespace WebApp.Services;
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
    // public static string? Upload(string root, IFormFile f, int len = 32){
    //     if(f!=null){
    //         string ext = Path.GetExtension(f.FileName);
    //         string fileName= RandomString(len - ext.Length) + ext;
    //         using(Stream stream = new FileStream(Path.Combine(root, fileName), FileMode.Create)){
    //             f.CopyTo(stream);
    //         }
    //         return fileName;
    //     }
    //     return null;
    // }
    // public static string? Upload(IFormFile f, int len = 32){
    //     string root = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images");
    //     return Upload(root,f,len);
    // }


}