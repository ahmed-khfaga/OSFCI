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
            #region Test0
            //string s = new string(Dir_Name);
            //if (Dir_Name.Length < 11)
            //{
            //    for (int i = Dir_Name.Length; i < 11; i++)
            //    {
            //        s += (char)' ';
            //    }
            //}
            //this.Dir_Namee = s.ToCharArray();
            // this.Dir_Namee = Dir_Name; 
            #endregion

            assignDirName(Dir_Name);
            this.dir_Attr = dir_Attribute;
            this.dir_First_Cluster = f_Cluster;
            this.dir_FileSize = dir_FileSize;
            //for(int i=0;i<Dir_Empty.Length;i++) 
            //{
            //    Dir_Empty[i] = 0;
            //}

            // Initilize Dir_Empty to zero 
            Array.Clear(Dir_Empty, 0, Dir_Empty.Length);
             
        }
        public void assignDirName(char[] name)
        {
            // Initilize Dir_Namee  

            Array.Clear(Dir_Namee,0,Dir_Namee.Length);

            #region test1
            //string s = new string(name);

            //if(name.Length < 11) 
            //{
            //    for(int i = name.Length; i < 11; i++)
            //    {
            //        s += (char)' ';
            //    }
            //} 
            #endregion

            for(int i=0;i < name.Length && i < Dir_Namee.Length;i++)
            {
                Dir_Namee[i] = name[i];
            }
            // if name less than 11 
            for (int i = name.Length; i < Dir_Namee.Length; i++)
                Dir_Namee[i] = ' ';
        }
    }
}   
