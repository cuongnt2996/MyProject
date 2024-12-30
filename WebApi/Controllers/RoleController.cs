namespace WebApi.Controllers;

using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
[ApiController]
[Route("api/[controller]")]
public class RoleController:BaseController
{
    public IEnumerable<Role> GetRoles(){
        return  Provider.Role.GetRoles();
    }
    [HttpPost]
    public int Add(Role obj){
        return Provider.Role.Add(obj);
    }
    [HttpGet("{id}")]
    public Role? GetRole(int id){
        return Provider.Role.GetRole(id);
    }
    [HttpPut]
    public int Edit(Role obj){
        return Provider.Role.Edit(obj);
    }
    [HttpPut("delete")]
    public int Delete(Role obj){
        return Provider.Role.Delete(obj);
    }
}