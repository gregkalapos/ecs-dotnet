// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.CommonSchema.Serialization;
using Utf8Json;
using Utf8Json.Resolvers;
using JsonSerializer = Utf8Json.JsonSerializer;

namespace Elastic.CommonSchema
{
	[JsonFormatter(typeof(BaseJsonFormatter))]
	public partial class Base
	{
		private static JsonSerializerOptions SerializerOptions { get; } =
			new JsonSerializerOptions { IgnoreNullValues = true, WriteIndented = false, };

		public byte[] Serialize() => System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(this, SerializerOptions);
		public Task SerializeAsync(Stream s) => System.Text.Json.JsonSerializer.SerializeAsync(s, this, SerializerOptions);
		public void Serialize(Stream s)
		{
			using var writer = new Utf8JsonWriter(s);
			System.Text.Json.JsonSerializer.Serialize(writer, this, SerializerOptions);
		}
	}

	public class BaseJsonConverter : JsonConverter<Base>
	{
		public override Base Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{

		}

		public override void Write(Utf8JsonWriter writer, Base value, JsonSerializerOptions options)
		{

		}
	}
}
