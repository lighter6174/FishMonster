using Newtonsoft.Json;
using System;
using UnityEngine;

public class TupleColorConvert : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return true;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartArray)
        {
            byte[] col = reader.ReadArrayIntoByteArray();
            Color color = new Color(col[0] / 255f, col[1] / 255f, col[2] / 255f);
            return color;
        }
        else
        {
            throw new Exception(string.Format("KeyRandomArea的序列化需要是string类型: {0}", objectType));
        }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        Color color = (Color)value;
        string text = "[" + color.r + "," + color.g + "," + color.b + "," + color.a + "]";
        writer.WriteValue(text);
    }
};