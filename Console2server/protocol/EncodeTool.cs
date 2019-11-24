using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class EncodeTool {
    public static byte[] EncodePackage(byte[] data) {

        using (MemoryStream ms = new MemoryStream()) {
            using (BinaryWriter bw = new BinaryWriter(ms)) {
                int length = data.Length;
                bw.Write(length);
                bw.Write(data);
                byte[] res = new byte[(int)ms.Length];
                Buffer.BlockCopy(ms.GetBuffer(), 0, res, 0, (int)ms.Length);
                return res;
            }
        }
    }
    public static byte[] DecodePakeage(ref List<byte> data) {
        if (data.Count < 4) {

            //  Console.WriteLine("数据出现丢失 不完整");
            return null;
            throw new Exception("数据出现丢失 不完整");
        }
        using (MemoryStream ms = new MemoryStream(data.ToArray())) {
            using (BinaryReader br = new BinaryReader(ms)) {
                int length = br.ReadInt32();
                int remainL = (int)(ms.Length - ms.Position);
                if (length > remainL) {
                    return null;
                    //throw new Exception("数据长度大于总长度 出现错误");
                }
                byte[] res = br.ReadBytes(length);
                data.Clear();
                data.AddRange(br.ReadBytes(remainL));
                return res;
            }
        }
    }
    //Msg类工具
    //    Msg类转化为byte数组
    public static byte[] EncodeMsg(socketMsg msg) {
        MemoryStream ms = new MemoryStream();
        BinaryWriter bw = new BinaryWriter(ms);

        bw.Write(msg.OpCode);
        bw.Write(msg.SubCode);
        if (msg.value != null) {
            byte[] valueByte = EncodeObj(msg.value);
            bw.Write(valueByte);
        }
        byte[] res = new byte[(int)ms.Length];
        Buffer.BlockCopy(ms.GetBuffer(), 0, res, 0, (int)ms.Length);
        bw.Close();
        ms.Close();
        return res;

    }
    //    byte数组转化为msg类
    public static socketMsg DecodeMsg(byte[] data) {
        MemoryStream ms = new MemoryStream(data);
        BinaryReader br = new BinaryReader(ms);
        socketMsg msg = new socketMsg();
        msg.OpCode = br.ReadInt32();
        msg.SubCode = br.ReadInt32();
        if (ms.Length > ms.Position) {
            byte[] valueBtye = br.ReadBytes((int)(ms.Length - ms.Position));
            object obj = DecodeObj(valueBtye);
            msg.value = obj;
        }
        br.Close();
        ms.Close();
        return msg;

    }
    //object类
    //将object类转化为byte[]
    public static byte[] EncodeObj(object data) {
        using (MemoryStream ms = new MemoryStream()) {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, data);
            byte[] res = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, res, 0, (int)ms.Length);
            return res;
        }

    }
    //将byte[] 类转化为object
    public static object DecodeObj(byte[] data) {
        using (MemoryStream ms = new MemoryStream(data)) {
            BinaryFormatter bf = new BinaryFormatter();
            object res = bf.Deserialize(ms);
            return res;
        }

    }
}