using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simple
{
    internal class Virtual_Disk
    {

        public static FileStream disk;
        public const int clusterSize= 1024;
        public const int clusters = 1024;
        public const int diskSize = clusterSize * clusters;



        public static void CreateOrOpen_Disk(string path)
        {
            try
            {
                // here we Open File 
                    disk = new FileStream(path, FileMode.Open,FileAccess.ReadWrite);

                // check if File can't Read or Can't Write g Exeption and create New File 
                    if(!disk.CanRead || !disk.CanWrite) 
                    {
                        throw new IOException("Failed to open disk for reading and writing.");
                    }

            }
            catch(FileNotFoundException) // here we catch Exeption and Create New File 
            {
                disk = new FileStream(path, FileMode.Create, FileAccess.Write);
                disk.Close();

               // After Make File We Open it 
                disk = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            }

        }

        public static void writeCluster(byte[] cluster , int clusterIndex)
        {
            //Virtual_Disk.CreateOrOpen_Disk();
            //byte[] buffer = Encoding.ASCII.GetBytes(cluster.ToArray());
            //disk.Seek(clusterIndex * clusterSize, SeekOrigin.Begin);
            //disk.Write(buffer, 0, buffer.Length);
            //disk.Flush();
            if (cluster.Length > clusterSize )
            {
                throw new ArgumentException($"Cluster must be {clusterSize} bytes");
            }
            disk.Seek(clusterIndex * clusterSize, SeekOrigin.Begin);
            disk.Write(cluster, 0, clusterSize);
            disk.Flush(); 
        }
        public static byte[] readCluster(int clusterIndex) 
        {
            disk.Seek(clusterIndex * clusterSize, SeekOrigin.Begin);
            byte [] bytes = new byte[clusterSize];
            disk.Read(bytes, 0, clusterSize);
            return bytes;
        }

        // METHODS GETFREE_SPACE
        public static int getFreeSpace()
        {
            disk.Seek(0, SeekOrigin.End);
            return diskSize - (int)disk.Position;
        }

        public static bool isNew()
        {
            disk.Seek(0,SeekOrigin.End);

            int size = (int)disk.Position;

            return size == 0;
        }

        public static void closeDisk()
        {
            disk.Close();
        }



    }
}
