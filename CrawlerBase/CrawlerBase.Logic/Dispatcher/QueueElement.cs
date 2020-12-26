using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dispatcher
{
    public enum ProcessingInstruction
    {
        // dolgozd fel
        Process = 1,
        // állítsd le a szálat
        Stop = 2,
    }

    public class QueueElement<T> where T : class
    {
        public T Element { get; protected set; }

        public ProcessingInstruction ProcessingInstruction { get; protected set; }
    }
}
