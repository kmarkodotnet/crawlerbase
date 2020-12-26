using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace CrawlerBase.Logic.Dispatcher
{
    public abstract class DispatcherBase<ST, PT, PTP, Q, QI>
        where ST : HearthBeatThread<Q, QI>, new()
        where PT : ProcessorThread<Q, QI>, new()
        where PTP : ProcessorThreadPool<PT, Q, QI>, new()
        where Q : WaiterQueue<QueueElement<QI>>, new()
        where QI : class
    {
        protected ST SelectorThread { get; set; }
        protected PTP ProcessorThreadPool { get; set; }
        protected Q Queue { get; set; }
        protected TimeSpan HearthBeat { get; set; }
        protected Timer _timer;

        protected double HearthBeatMillisec
        {
            get { return HearthBeat.TotalMilliseconds; }
        }

        public DispatcherBase(int processorThreadCount, int queueLength, TimeSpan hearthBeat)
        {
            this.SelectorThread = new ST();
            this.ProcessorThreadPool = new PTP();
            this.ProcessorThreadPool.SetSize(processorThreadCount);
            this.Queue = new Q();
            this.Queue.Initialize(queueLength);

            this.SelectorThread.Initialize(this.Queue);
            this.ProcessorThreadPool.Initialize(this.Queue);

            this.HearthBeat = hearthBeat;
        }

        public void Start()
        {
            // szálindítások
            ProcessorThreadPool.Start();
            SelectorThread.Start();

            // időzítő indul
            _timer = new Timer();
            _timer.Interval = HearthBeatMillisec;
            _timer.AutoReset = true;
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;

            SelectorThread.HearthBeat();
            _timer.Start();
        }

        /// <summary>
        /// Leállítjuk az össes feldolgozó szálat
        /// </summary>
        public void Stop()
        {
            _timer.Enabled = false;
            _timer.Stop();

            // többet nem fogadunk, de amit éppen fogadunk, azt még bejezzük
            SelectorThread.Stop();

            // többet már nem kommunikálunk, de ami még hátra van, azt betoljuk
            ProcessorThreadPool.Stop();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SelectorThread.HearthBeat();
        }
    }
}