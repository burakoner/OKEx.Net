using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Okex.Net.SocketObjects
{
    internal class SocketRequest
    {
        [JsonProperty("op"), JsonConverter(typeof(SocketOperationConverter))]
        public SocketOperation Operation { get; set; }

        [JsonProperty("args")]
        public List<string> Arguments { get; set; }

        public SocketRequest(SocketOperation op, params string[] args)
        {
            Operation = op;
            Arguments = args.ToList();
        }

        public SocketRequest(SocketOperation op, IEnumerable<string> args)
        {
            Operation = op;
            Arguments = args.ToList();
        }
    }

    internal enum SocketOperation
    {
        Subscribe,
        Unsubscribe,
        Login,
    }

    internal class SocketOperationConverter : BaseConverter<SocketOperation>
    {
        public SocketOperationConverter() : this(true) { }
        public SocketOperationConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SocketOperation, string>> Mapping => new List<KeyValuePair<SocketOperation, string>>
        {
            new KeyValuePair<SocketOperation, string>(SocketOperation.Subscribe, "subscribe"),
            new KeyValuePair<SocketOperation, string>(SocketOperation.Unsubscribe, "unsubscribe"),
            new KeyValuePair<SocketOperation, string>(SocketOperation.Login, "login"),
        };
    }
}
