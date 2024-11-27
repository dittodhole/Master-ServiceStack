using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Storage;

namespace HelloWorld.Website
{
    public class MessageServiceStorage : IStorage
    {
        public IMessageService MessageService { get; set; }

        public void Save(Profiler profiler)
        {
            using (var messageProducer = this.MessageService.CreateMessageProducer())
            {
                messageProducer.Publish(profiler);
            }
        }

        public Profiler Load(Guid id)
        {
            return null;
        }

        public List<Guid> GetUnviewedIds(string user)
        {
            return new List<Guid>(0);
        }
    }
}
