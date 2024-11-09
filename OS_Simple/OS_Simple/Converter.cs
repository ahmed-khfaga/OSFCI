using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simple
{
    internal class Converter
    {
        public static byte[] IntToByte(int n)
        {
            return BitConverter.GetBytes(n);
        }
        public static int ByteToInt(byte[] bytes) 
        {
            return BitConverter.ToInt32(bytes, 0);
        }
        public static byte[] IntArrayToByteArray(int[]n)
        {
            return n.SelectMany(IntToByte).ToArray();
        }
        public static int[] ByteArrayToIntArray(byte[] bytes)
        {
            int[] n = new int[bytes.Length / 4];
            for(int i=0;i<n.Length; i++)
            {
                n[i] = BitConverter.ToInt32(bytes, i*4);
            }
            return n;
        }
        public static List<byte[]> SplitBytes(byte[] data,int size = 1024)
        {
            List<byte[]> list = new List<byte[]>();
            for(int i = 0; i < data?.Length;i += size) 
            {
                list.Add(data.Skip(i).Take(size).ToArray());
            }
            return list;
        }




    }
}
