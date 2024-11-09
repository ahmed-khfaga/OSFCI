using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simple
{
    internal class Mini_FAT
    {
        public static int[] FAT = new int[1024];


        public static void Initialization()
        {
            for (int i = 0; i < FAT.Length; i++) 
            {
                if(i==0 || i==4)
                {
                    FAT[i] = -1;
                }
                else if(i > 0 && i <= 3)
                {
                    FAT[i] = i + 1;
                }
                else
                    FAT[i] = 0;
            }
        }

        public static void printFat()
        {
            for(int i = 0;i < FAT.Length;i++)
            {
                Console.WriteLine(FAT[i]);
            }
        }
        public static void setFAT(int[]fatArray)
        {
            if (fatArray.Length == FAT.Length)
            {
                Array.Copy(fatArray, FAT, fatArray.Length);

            }
            else
                Console.WriteLine("Out Of Range , Your size must be 1024");
        }

        public static int getAvailabelClusters()
        {
            int counter = 0;
            for (int i = 0;i<FAT.Length;i++)
            {
                if (FAT[i]==0)
                {
                    counter++;
                }
            }
            return counter;
        }
        public static int getAvailabelCluster()
        {
            for(int i = 0;i<FAT.Length;i++)
            {
                if (FAT[i]==0)
                {
                    return i;
                }
            }
            return 1;
        }

        public static byte[]createSuperBlock()
        {
            int[] arr = new int[256];
            for(int i=0;i<arr.Length;i++)
            {
                arr[i] = 0;
            }
            return Converter.IntArrayToByteArray(arr);
        }


        public static int getFreeSize()
        {
            return getAvailabelClusters() * 1024;
        }
        public static void writeFAT()
        {
            byte[] FATBYTES = Converter.IntArrayToByteArray(FAT);
            List<byte[]> cluster = Converter.SplitBytes(FATBYTES);
            for(int i=0;i<cluster.Count;i++) 
            {
                Virtual_Disk.writeCluster(cluster[i], i + 1);
            }

        }
        public static void readFAT() 
        {
           List<byte>bytes= new List<byte>();
            for(int i=0;i<=4;i++)
            {
                bytes.AddRange(Virtual_Disk.readCluster(i));
            }
        }
        public static void InitializeOrOpenFileSystem(string name)
        {
            Virtual_Disk.CreateOrOpen_Disk(name);
            if(Virtual_Disk.isNew())
            {
                byte[]superBlock=createSuperBlock();
                Virtual_Disk.writeCluster(superBlock, 0);
                Initialization();
                writeFAT();
            }
            else
                readFAT();
        }
        public static void setNext(int clusterIndex,int pointer)
        {
            if(clusterIndex >= 0 && clusterIndex < FAT.Length && pointer >=0 && pointer < FAT.Length)
            {
                FAT[clusterIndex] = pointer;
            }
        }
        public static int getNext(int clusterindex)
        {
            if(clusterindex >= 0 && clusterindex < FAT.Length)
                return FAT[clusterindex];
            return -1;
        }

    }
}
