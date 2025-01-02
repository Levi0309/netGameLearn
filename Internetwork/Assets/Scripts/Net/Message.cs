using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;
public class Message : MonoBehaviour
{
    byte[] data = new byte[1024];
    int startIndex;
    int remainIndex;

    //public void AddCount(int count)
    //{
    //    startIndex += count;
    //}
    public int StartIndex { get => startIndex; }
    public int RemainIndex { get => data.Length - startIndex; }

    public byte[] Data { get => data; }


    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallback)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0);//先解析长度  这里肯定解析了4个字节 这个count是数字字符的个数 也就是字符长度 比如33 count就是2
            if (startIndex - 4 >= count) //说明数据是完整的  为什么 因为 比如数字2  长度是1  前四个字节存储的是长度  后一个字节存储的是具体的值 一共是5  所以5-4>=1 刚好能读出2
            {
                //Console.WriteLine("前startIndex" + startIndex);
                //Console.WriteLine("count" + count);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
               

                //string s = Encoding.UTF8.GetString(data, 4, count);//从4开始读 因为前0123 是存的长度 从4开始才存的数据，数据长度为count
                string s = Encoding.UTF8.GetString(data, 8, count - 4);//数据长度要减去8 就是后面的两个数字的字节长度
                processDataCallback(actionCode, s);
                //Console.WriteLine("解析出来一条数据" + s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
                Console.WriteLine("后startIndex" + startIndex);
            }
            else
            {
                break;
            }
        }


    }
    /// <summary>
    /// 包的整合  长度+具体数据  长度包括RequestCode长度和数据长度 这是服务端发送给客户端的数据 不包含actionCode
    /// </summary>
    /// <param name="requestData"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    //public static byte[] PackData(ActionCode actionCode, string data)
    //{
    //    byte[] requestByte = BitConverter.GetBytes((int)actionCode);
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    int dataAmount = requestByte.Length + dataBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //    byte[] newbytes = dataAmountBytes.Concat(requestByte).ToArray<byte>();
    //    newbytes = newbytes.Concat(dataBytes).ToArray<byte>();
    //    return newbytes;
    //}
    /// <summary>
    /// 包的整合 客户端发给服务端的数据  这是客户端发送给服务端的数据 包含actionCode
    /// </summary>
    /// <param name="requestData"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static byte[] PackData(RequestCode requestData, ActionCode actionCode, string data)
    {
        byte[] requestByte = BitConverter.GetBytes((int)requestData);
        byte[] actionByte = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestByte.Length + actionByte.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        byte[] newbytes = dataAmountBytes.Concat(requestByte).ToArray<byte>();
        newbytes = newbytes.Concat(actionByte).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
        return newbytes;
    }
}
