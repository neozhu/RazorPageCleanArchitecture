// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Extensions
{
    public class EmptyNumberConverter : JsonConverter<decimal?>
    {
        public override bool HandleNull => true;
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(decimal?);
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) {
                return null;
            }
            else
            {
                if(reader.GetString().Trim()==string.Empty)
                {
                    return null;
                }
                else
                {
                    return decimal.Parse(reader.GetString());
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options) {
            if(value==null )
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteNumberValue(value.Value);
            }
        }
           
    }
    public class EmptyIntConverter : JsonConverter<int?>
    {
        public override bool HandleNull => true;
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(int?);
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            else
            {
                if (reader.GetString().Trim() == string.Empty)
                {
                    return null;
                }
                else
                {
                    return int.Parse(reader.GetString());
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteNumberValue(value.Value);
            }
        }

    }
}
