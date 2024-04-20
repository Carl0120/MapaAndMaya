
using Microsoft.AspNetCore.Mvc;

namespace MapaAndMaya.Services
{
    public enum NotifySeverity
    {
        Succes,
        Warning,
        Error
    }
    
    public class ActionResult<T>
    {
        public bool Status { get; set; }
        public string Title { get; set; } = String.Empty;
        public T Result { get; set; }
        public NotifySeverity Severity { get; set; }
        public List<string> Errors { get; } = new List<string>();
        
    }
}