using log4net.Layout;
using log4net.Util;

namespace Elastic.Apm.Log4Net
{
	public class ElasticApmPatternLayout : PatternLayout
	{
		public ElasticApmPatternLayout()
		{
			AddConverter(new ConverterInfo { Name = "ElasticApmTraceId", Type = typeof(TraceIdPatternConverter) });
			AddConverter(new ConverterInfo { Name = "ElasticApmTransactionId", Type = typeof(TransactionIdPatternConverter) });
		}
	}
}
