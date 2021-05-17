using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DynamoDbContract
{
    [DynamoDBTable("MutantStats")]
    public class MutantStats
    {
        [DynamoDBProperty]
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty(typeof(DynamoDBNativeBooleanConverter))]
        public bool IsMutant { get; set; }
    }

    public class DynamoDBNativeBooleanConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value) => (bool)value ? "true" : "false";

        public object FromEntry(DynamoDBEntry entry)
        {
            var val = bool.Parse(entry.AsPrimitive().Value.ToString());
            return val;
        }
    }
}
