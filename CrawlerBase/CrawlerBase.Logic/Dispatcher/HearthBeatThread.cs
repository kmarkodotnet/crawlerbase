using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CrawlerBase.Logic.Dispatcher
{
    public abstract class HearthBeatThread<Q, QI> : BaseThread<Q, QI>
        where Q : WaiterQueue<QueueElement<QI>>
        where QI : class
    {
        protected volatile bool _IsRunning;
        private long _HearthBeat;

        public HearthBeatThread()
        {
            _IsRunning = false;
            _HearthBeat = 0;
        }

        public void HearthBeat()
        {
            Interlocked.Increment(ref _HearthBeat);
        }


        public override void Start()
        {
            _IsRunning = true;
            StartThread();
            AfterStart();
        }

        /// <summary>
        /// többet nem fogadunk, de amit éppen fogadunk, azt még befejezzük
        /// </summary>
        public override void Stop()
        {
            _IsRunning = false;
            StopMessageReceived();
            WaitForThreadTermination();
            AfterStop();
        }

        protected override void Processor()
        {
            while (_IsRunning)
            {
                // várjunk egy picit, hogy ne izzon a processzor az üres ciklustól
                System.Threading.Thread.Sleep(100);
                // olvassuk ki atomilag, hogy mennyi hearthbeat-ünk volt
                var hearthBeat = Interlocked.Read(ref _HearthBeat);
                // ha több, mint 0, akkor dolgozni kell
                if (hearthBeat > 0)
                {
                    ProcessWaitingEntities();
                    // dolgoztunk egyet
                    Interlocked.Decrement(ref _HearthBeat);
                }
            }
        }

        protected abstract void ProcessWaitingEntities();

        protected abstract void AfterStart();
        protected abstract void AfterStop();
        protected abstract void StopMessageReceived();
    }
}
