namespace Seller.Shared.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;
    public class Message
    {
        public string serializedData;

        public Message(object data, string id)
        {
            Id = id;
            Data = data;
        }
          

        private Message()
        {
        }

        public string Id { get;  set; }

        public Type Type { get;  set; }

        public bool Published { get;  set; }

        public void MarkAsPublished() => this.Published = true;

        [NotMapped]
        public object Data
        {
            get => JsonConvert.DeserializeObject(this.serializedData, this.Type,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            set
            {
                this.Type = value.GetType();

                this.serializedData = JsonConvert.SerializeObject(value,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
        }
    }
}
