// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Extensions.Logging
{
	public enum ConnectionPoolType
	{
		Unknown = 0,
		SingleNode,
		Sniffing,
		Static,
		Sticky,
		StickySniffing,
		Cloud
	}
}
