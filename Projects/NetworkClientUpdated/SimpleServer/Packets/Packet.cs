using System;

namespace Packets
{
    public enum PacketType
    {
        EMPTY, NICKNAME, CHATMESSAGE
    }

    [Serializable]
    public class Packet
    {
        public PacketType type = PacketType.EMPTY;
    }

    [Serializable]
    public class ChatMessagePacket : Packet
    {
        public string _message;

        public ChatMessagePacket(string message)
        {
            type = PacketType.CHATMESSAGE;
            _message = message;
        }
    }

    [Serializable]
    public class NicknamePacket : Packet
    {
        public string _nickName;

        public NicknamePacket(string nickName)
        {
            type = PacketType.NICKNAME;
            _nickName = nickName;
        }
    }
}
