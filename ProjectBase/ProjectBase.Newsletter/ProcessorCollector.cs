using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBase.Newsletter
{
	public class ProcessorCollector
	{
		private readonly INewsletterProcessor[] processors;

		public ProcessorCollector( INewsletterProcessor[] processors )
		{
			this.processors = processors;
		}

		public INewsletterProcessor[] Processors
		{
			get { return processors; }
		}
	}
}
