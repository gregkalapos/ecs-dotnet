using System.IO;
using log4net.Util;

namespace Elastic.Apm.Log4Net
{
	public class TraceIdPatternConverter : PatternConverter
	{
		protected override void Convert(TextWriter writer, object state)
		{
			if (!Agent.IsConfigured) return;
			if (Agent.Tracer.CurrentTransaction == null) return;

			writer.Write(Agent.Tracer.CurrentTransaction.TraceId);
		}
	}
}
