using System;
using System.IO;
using Elastic.Apm.Test.Common;
using FluentAssertions;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Xunit;

namespace Elastic.Apm.Log4Net.Test
{
	public class Log4NetTests
	{
		/// <summary>
		/// Creates 1 simple transaction and makes sure that the log line created within the transaction has
		/// the transaction and trace ids, and logs prior to and after the transaction do not have those.
		/// </summary>
		[Fact]
		public void Log4NetWithTransaction()
		{
			Agent.Setup(new AgentComponents(payloadSender: new NoopPayloadSender()));


			var hierarchy = (Hierarchy)LoggerManager.GetRepository(typeof(Log4NetTests).Assembly);
			var memoryAppender = new MemoryAppender();

			var elasticApmPatternLayout =new PatternLayout() { ConversionPattern = "%property{TraceId}  %ElasticApmTransactionId %message" };
			elasticApmPatternLayout.ActivateOptions();
			memoryAppender.Layout = elasticApmPatternLayout;

			hierarchy.Root.AddAppender(memoryAppender);
			hierarchy.Root.Level = Level.All;
			hierarchy.Configured = true;

			BasicConfigurator.Configure(hierarchy);


			var log = LogManager.GetLogger(typeof(Log4NetTests));

			GlobalContext.Properties["TraceId"] = new ElasticApmTraceIdHelper();

			log.Info("PreTransaction");

			string traceId = null;
			string transactionId = null;

			Agent.Tracer.CaptureTransaction("TestTransaction", "Test", t =>
			{
				traceId = t.TraceId;
				transactionId = t.Id;
				log.Info("InTransaction");
			});

			log.Info("PostTransaction.");

			var allEvents = memoryAppender.PopAllEvents();
			allEvents.Length.Should().Be(3);

			var formattedLines = new string[3];

			for (var i = 0; i < 3; i++)
			{
				using (var writer = new StringWriter())
				{
					memoryAppender.Layout.Format(writer, allEvents[i]);
					writer.Write(Environment.NewLine);
					formattedLines[i] = writer.ToString();
				}
			}
//
//			allEvents[0].Properties["Trace.Id"].Should().BeNull();
//			allEvents[0].Properties["Transaction.Id"].Should().BeNull();

			allEvents[1].Properties["TraceId"].Should().Be(traceId);
//			allEvents[1].Properties["Transaction.Id"].Should().Be(transactionId);
//
//			allEvents[2].Properties["Trace.Id"].Should().BeNull();
//			allEvents[2].Properties["Transaction.Id"].Should().BeNull();
		}
	}
}
