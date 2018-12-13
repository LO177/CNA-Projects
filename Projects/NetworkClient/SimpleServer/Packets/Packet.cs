using System;

namespace Packets
{
    public enum PacketType
    {
        EMPTY, NICKNAME, CHATMESSAGE
    }

    public class Packet
    {
        PacketType type;
    }

    public class ChatMessagePacket : Packet
    {
        string message;

        ChatMessagePacket(string message)
        {

        }
    }

    public class NicknamePacket : Packet
    {

    }
}
