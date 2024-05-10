
using Microsoft.AspNetCore.Mvc;

namespace MapaAndMaya.Services
{
    public enum NotifySeverity
    {
        Success,
        Warning,
        Error
    }

    public class ActionResult<T>
    {
        public bool Status { get; private set; }
        public string Title { get; private set; } = String.Empty;
        public T? Element { get; private set; }
        public NotifySeverity Severity { get; private set; }
        public List<string> Errors { get; } = new();

        public void CreateResponseSuccess(T entity)
        {
            Title = "Exito";
            Severity = NotifySeverity.Success;
            Status = true;
            Element = entity;
        }

        public void CreateResponseFail(Exception ex)
        {
            Title = "Fallo";
            Severity = NotifySeverity.Error;
            Status = false;
            Errors.Add((ex.Message));
        }

        public void CreateResponseInvalidAction()
        {
            Status = false;
            Severity = NotifySeverity.Warning;
            Title = "Accion Inválida";
        }
        
    }
    
}