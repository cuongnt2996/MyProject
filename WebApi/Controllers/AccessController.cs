using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi.Models;
[ApiController,Route("api/[controller]")]
public class AccessController:BaseController
{
    // [Authorize]
    public IEnumerable<Access>? GetAccesses(){
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(string.IsNullOrEmpty(userId)){
            return null;
        }
        IEnumerable<Access> list = Provider.Access.GetAccessesByUser(userId);
        Dictionary<int,List<Access>> dict = new Dictionary<int, List<Access>>();
        List<Access> accesses = new List<Access>();
        foreach(var item in list){
            if(item.ParentId is null){
                accesses.Add(item);
            }else{
                int k = item.ParentId.Value;
                if(!dict.ContainsKey(k)){
                    dict[k] = new List<Access>();
                }
                dict[k].Add(item);
            }
        }
        foreach(var item in list){
            if(dict.ContainsKey(item.AccessId)){
                item.Children = dict[item.AccessId];
            }
        }
        return accesses;
    }
    [HttpPost]
    public int Add(Access obj){
        return Provider.Access.Add(obj);
    }
    [HttpGet("parents")]
    public IEnumerable<Access> GetParents(){
        return Provider.Access.GetParent();
    }
    [HttpGet("accesscheckeds/{id}")]
    public IEnumerable<AccessChecked> GetAccessCheckeds(int id){
        return Provider.Access.GetAccessesCheckedsByRole(id);
    }
    [HttpGet("accesscheckeds")]
    [Authorize]
    public IEnumerable<AccessChecked>? GetAccessChecked(){
        // IEnumerable<Access> list = Provider.Access.GetAccesses();
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(string.IsNullOrEmpty(userId)){
            return null;
        }
        IEnumerable<AccessChecked> list = Provider.Access.GetAccessesChecked(userId);
        Dictionary<int, List<AccessChecked>> dict = new Dictionary<int, List<AccessChecked>>();
        List<AccessChecked>  accesses = new List<AccessChecked>();
        foreach(var item in list){
            if(item.ParentId is null){
                accesses.Add(item);
            }else{
                int k = item.ParentId.Value;
                if(!dict.ContainsKey(k)){
                    dict[k] = new List<AccessChecked>();
                }
                dict[k].Add(item);
            }
        }
        foreach (var item in list){
            if(dict.ContainsKey(item.AccessId)){
                item.Children = dict[item.AccessId];
            }
        }
        return accesses;
    }
}