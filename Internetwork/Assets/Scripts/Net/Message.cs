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
            int count = BitConverter.ToInt32(data, 0);//�Ƚ�������  ����϶�������4���ֽ� ���count�������ַ��ĸ��� Ҳ�����ַ����� ����33 count����2
            if (startIndex - 4 >= count) //˵��������������  Ϊʲô ��Ϊ ��������2  ������1  ǰ�ĸ��ֽڴ洢���ǳ���  ��һ���ֽڴ洢���Ǿ����ֵ һ����5  ����5-4>=1 �պ��ܶ���2
            {
                //Console.WriteLine("ǰstartIndex" + startIndex);
                //Console.WriteLine("count" + count);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
               

                //string s = Encoding.UTF8.GetString(data, 4, count);//��4��ʼ�� ��Ϊǰ0123 �Ǵ�ĳ��� ��4��ʼ�Ŵ�����ݣ����ݳ���Ϊcount
                string s = Encoding.UTF8.GetString(data, 8, count - 4);//���ݳ���Ҫ��ȥ8 ���Ǻ�����������ֵ��ֽڳ���
                processDataCallback(actionCode, s);
                //Console.WriteLine("��������һ������" + s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
                Console.WriteLine("��startIndex" + startIndex);
            }
            else
            {
                break;
            }
        }


    }
    /// <summary>
    /// ��������  ����+��������  ���Ȱ���RequestCode���Ⱥ����ݳ��� ���Ƿ���˷��͸��ͻ��˵����� ������actionCode
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
    /// �������� �ͻ��˷�������˵�����  ���ǿͻ��˷��͸�����˵����� ����actionCode
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
