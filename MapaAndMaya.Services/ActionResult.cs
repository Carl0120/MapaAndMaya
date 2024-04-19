
using Microsoft.AspNetCore.Mvc;

namespace MapaAndMaya.Services
{
    
    public class ActionResult<T>
    {
        public bool? Status { get; set; }
        public string? Title { get; set; }
        public T Result { get; }
        public List<(string, string)> Errors { get; } = new List<(string, string)>();

        public ActionResult(T result)
        {
            Result = result;
        }
    }
}