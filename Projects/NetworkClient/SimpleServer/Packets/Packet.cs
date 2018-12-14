using System;

namespace Packets
{
    public enum PacketType
    {
        EMPTY, NICKNAME, CHATMESSAGE
    }

    public class Packet
    {
        public PacketType type = PacketType.EMPTY;
    }

    public class ChatMessagePacket : Packet
    {
        string _message;

        ChatMessagePacket(string message)
        {
            type = PacketType.CHATMESSAGE;
            _message = message;
        }
    }

    public class NicknamePacket : Packet
    {
        string _nickName;

        NicknamePacket(string nickName)
        {
            type = PacketType.NICKNAME;
            _nickName = nickName;
        }
    }
}
