using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_forecast_app_entities.Models
{
    [Serializable]
    public class InfoMessage
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public InfoMessage()
        {
            this.Id = string.Empty;
            this.Title = string.Empty;
            this.Description = string.Empty;
        }

        public InfoMessage(string id, string title, string description)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
        }

        public InfoMessage(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }
    }
    [Serializable]
    public class ObjResult
    {
        [Bindable(true)]
        public object? Result { get; set; }

        [Bindable(true)]
        public InfoMessage? Message { get; set; }

        public ObjResult()
        {
            this.Result = null;
        }

        public ObjResult(object data, InfoMessage message)
        {
            this.Result = data;
            this.Message = message;
        }
    }
}
