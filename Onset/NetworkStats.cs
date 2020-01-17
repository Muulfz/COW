using Onset.Runtime;

namespace Onset
{
    /// <summary>
    /// Containing information about the Onset networking.
    /// </summary>
    public struct NetworkStats
    {
        public int TotalPacketLoss { get; }

        public int LastSecondPacketLoss { get; }

        public int MessagesInResendBuffer { get; }

        public int BytesInResendBuffer { get; }

        public int BytesSend { get; }

        public int BytesReceived { get; }

        public int BytesResend { get; }

        public int TotalBytesSend { get; }

        public int TotalBytesReceived { get; }

        public bool IsLimitedByCongestionControl { get; }

        public bool IsLimitedByOutgoingBandwidthLimit { get; }

        internal NetworkStats(ReturnData data)
        {
            TotalPacketLoss = data.Value<int>("totalPacketLoss");
            LastSecondPacketLoss = data.Value<int>("lastSecondPacketLoss");
            MessagesInResendBuffer = data.Value<int>("messagesInResendBuffer");
            BytesInResendBuffer = data.Value<int>("bytesInResendBuffer");
            BytesSend = data.Value<int>("bytesSend");
            BytesReceived = data.Value<int>("bytesReceived");
            BytesResend = data.Value<int>("bytesResend");
            TotalBytesSend = data.Value<int>("totalBytesSend");
            TotalBytesReceived = data.Value<int>("totalBytesReceived");
            IsLimitedByCongestionControl = data.Value<bool>("isLimitedByCongestionControl");
            IsLimitedByOutgoingBandwidthLimit = data.Value<bool>("isLimitedByOutgoingBandwidthLimit");
        }
    }
}