namespace Elastic.Apm.Log4Net
{
	public class ElasticApmTraceIdHelper
	{
		public override string ToString()
		{
			if (!Agent.IsConfigured) return string.Empty;

			if (Agent.Tracer.CurrentTransaction == null) return string.Empty;



			return Agent.Tracer.CurrentTransaction.TraceId;
		}
	}
}
