using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Simple
{
    internal class Directory_Entry
    {
        public char[] Dir_Namee = new char[11];
        public byte dir_Attr;
        public byte[] Dir_Empty = new byte[12];
        public int dir_First_Cluster;
        public int dir_FileSize;

        public Directory_Entry(char[] Dir_Name, byte dir_Attribute,int f_Cluster,int dir_FileSize)
        {
            string s = new string(Dir_Name);
            if (Dir_Name.Length < 11)
            {
                for (int i = Dir_Name.Length; i < 11; i++)
                {
                    s += (char)' ';
                }
            }
            this.Dir_Namee = s.ToCharArray();
           // this.Dir_Namee = Dir_Name;
            this.dir_Attr = dir_Attribute;
            this.dir_First_Cluster = f_Cluster;
            this.dir_FileSize = dir_FileSize;
            for(int i=0;i<Dir_Empty.Length;i++) 
            {
                Dir_Empty[i] = 0;
            }
             
        }
    }
}
