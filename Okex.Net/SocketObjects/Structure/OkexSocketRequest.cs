using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Okex.Net.SocketObjects.Structure
{
    internal class OkexSocketRequest
    {
        [JsonProperty("op"), JsonConverter(typeof(OkexSocketOperationConverter))]
        public OkexSocketOperation Operation { get; set; }

        [JsonProperty("args")]
        public List<string> Arguments { get; set; }

        public OkexSocketRequest(OkexSocketOperation op, params string[] args)
        {
            Operation = op;
            Arguments = args.ToList();
        }

        public OkexSocketRequest(OkexSocketOperation op, IEnumerable<string> args)
        {
            Operation = op;
            Arguments = args.ToList();
        }
    }

    internal enum OkexSocketOperation
    {
        Subscribe,
        Unsubscribe,
        Login,
    }

    internal class OkexSocketOperationConverter : BaseConverter<OkexSocketOperation>
    {
        public OkexSocketOperationConverter() : this(true) { }
        public OkexSocketOperationConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OkexSocketOperation, string>> Mapping => new List<KeyValuePair<OkexSocketOperation, string>>
        {
            new KeyValuePair<OkexSocketOperation, string>(OkexSocketOperation.Subscribe, "subscribe"),
            new KeyValuePair<OkexSocketOperation, string>(OkexSocketOperation.Unsubscribe, "unsubscribe"),
            new KeyValuePair<OkexSocketOperation, string>(OkexSocketOperation.Login, "login"),
        };
    }
}
