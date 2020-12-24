using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public abstract class PageProcessor<TSelect> : IOperationElement<TSelect>
    {
        public IOperationBaseElement Parent { get; set; }
        public ISelector<TSelect> Selector { get; set; }
        public HtmlParser Parser { get; set; }
        public IOperationBaseElement NextOperation { get; set; }

        public PageProcessor(ISelector<TSelect> Selector, HtmlParser Parser, IOperationBaseElement OpPipe)
        {
            this.Selector = Selector;
            this.Selector.Parent = this;

            this.Parser = Parser;
            this.Parser.Parent = this;

            this.NextOperation = OpPipe;
            if(this.NextOperation != null)
            {
                this.NextOperation.Parent = this;
            }
            
        }

        public TSelect Process(string page)
        {
            var parsedData = Parser.Parse(page);
            var selecselectedData = Selector.Select(parsedData);
            selecselectedData = PostProcessAfterSelected(selecselectedData);
            return selecselectedData;
        }

        protected abstract TSelect PostProcessAfterSelected(TSelect selected);

        IRootOperationElement IOperationBaseElement.GetRoot()
        {
            return this.Parent.GetRoot();
        }
    }
}
