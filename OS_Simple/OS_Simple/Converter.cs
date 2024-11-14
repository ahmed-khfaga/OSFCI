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
        //public static List<byte[]> SplitBytes(byte[] data,int size = 1024)
        //{
        //    List<byte[]> list = new List<byte[]>();
        //    for(int i = 0; i < data?.Length;i += size) 
        //    {
        //        list.Add(data.Skip(i).Take(size).ToArray());
        //    }
        //    return list;
        //}
        public static List<byte[]> SplitBytes(byte[] bytes, int chunkSize = 1024)
        {
            List<byte[]> chunks = new List<byte[]>();

            if (bytes.Length == 0)
            {
                
                chunks.Add(new byte[chunkSize]);
                return chunks;
            }

            int fullChunks = bytes.Length / chunkSize;
            int remainder = bytes.Length % chunkSize;

            
            for (int i = 0; i < fullChunks; i++)
            {
                byte[] chunk = new byte[chunkSize];
                Array.Copy(bytes, i * chunkSize, chunk, 0, chunkSize);
                chunks.Add(chunk);
            }

            
            if (remainder > 0)
            {
                byte[] lastChunk = new byte[chunkSize];
                Array.Copy(bytes, fullChunks * chunkSize, lastChunk, 0, remainder);
                chunks.Add(lastChunk);
            }

            return chunks;
        }


        #region ss
        // Now we need other 6 method 
        // :Directory_EntryToBytes 
        //::BytesToDirectory_Entry
        //::BytesToDirectory_Entries
        //::Directory_EntriesToBytes
        /*
         * Note:- if you use another programming language
           like python or C# you will need to implement 
           another two methods, one to convert string to 
           array of bytes and the other to convert array of bytes to string. 
         */
        #endregion

        // Converts a string to a array of bytes
        public static byte[] StringToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        // Convert array of bytes to a string
        public static string ByteArrayToString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes).TrimEnd('\0');
        }
        public static List<byte>Directory_EntryToBytes(Directory_Entry E)
        {
            List<byte> list = new List<byte>(32);
            list.AddRange(Encoding.ASCII.GetBytes(E.Dir_Namee));

            list.Add(E.dir_Attr);

            list.AddRange(E.Dir_Empty);

            list.AddRange(IntToByte(E.dir_First_Cluster));

            list.AddRange(IntToByte(E.dir_FileSize));

            return list;
        }
        public static Directory_Entry BytesToDirectory_Entry(byte[] bytes)
        {

            char[] dirName = Encoding.ASCII.GetChars(bytes, 0, 11);


            byte dirAttr = bytes[11];


            byte[] dirEmpty = bytes.Skip(12).Take(12).ToArray();


            int dirFirstCluster = BitConverter.ToInt32(bytes, 24);


            int dirFileSize = BitConverter.ToInt32(bytes, 28);

            return new Directory_Entry(dirName, dirAttr, dirFirstCluster, dirFileSize);

        }

        public static List<Directory_Entry> BytesToDirectory_Entries(byte[] bytes) 
        {
            List<Directory_Entry> dir = new List<Directory_Entry>();    
            for(int i = 0; i < bytes.Length; i+=32)
            {
                byte[] entryBytes=bytes.Skip(i).Take(32).ToArray();


                if (entryBytes[0] == 0)
                    break;


                Directory_Entry entry = BytesToDirectory_Entry(entryBytes);
                dir.Add(entry);
            }
            return dir;
        }
        public static List<byte> Directory_EntriesToBytes(List<Directory_Entry> entries)
        {
            List<byte> bytes = new List<byte>(entries.Count * 32);

            foreach (Directory_Entry entry in entries)
            {
                List<byte> entryBytes = Directory_EntryToBytes(entry);
                bytes.AddRange(entryBytes);
            }

            return bytes;
        }


    }
}
