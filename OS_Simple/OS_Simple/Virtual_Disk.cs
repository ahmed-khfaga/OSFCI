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

        public static void writeCluster(byte[] clusterdata , int clusterIndex)
        {
            //Virtual_Disk.CreateOrOpen_Disk();
            //byte[] buffer = Encoding.ASCII.GetBytes(cluster.ToArray());
            //disk.Seek(clusterIndex * clusterSize, SeekOrigin.Begin);
            //disk.Write(buffer, 0, buffer.Length);
            //disk.Flush();
            if (clusterdata.Length > clusterSize )
            {
                throw new ArgumentException($"Cluster must be {clusterSize} bytes");
            }
            disk.Seek(clusterIndex * clusterSize, SeekOrigin.Begin);
            disk.Write(clusterdata, 0, clusterSize);
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
            disk.Seek(0, SeekOrigin.End); //// Go to the end of the file and get the position
            //long diisk = disk.Length; we can calculate length of disk like this also 
            return diskSize - (int)disk.Position; // diskSize(1024*1024) - int(disk.Length)
        }

        //Checks if the disk is new by seeing if the file length is 0
        public static bool isNew()
        {

            // like FreeSpace if Disk.Length is == 0 so disk is New 
            disk.Seek(0,SeekOrigin.End);

            int size = (int)disk.Position;
            // int size = Disk.Length we can do also like this ... 
            return size == 0;
        }

        public static void closeDisk()
        {
            disk.Close();
        }



    }
}
