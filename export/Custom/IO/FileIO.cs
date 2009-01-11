using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Custom.IO
{
    public class FileIO
    {
        public void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            DirectoryInfo dest = new DirectoryInfo(destination.FullName);
            // create target directory
            int count = source.FullName.LastIndexOf("\\");
            string dirName = source.FullName.Substring(count + 1);

            destination.CreateSubdirectory(dirName);
            dest = new DirectoryInfo(dest.FullName  + "\\" + dirName);           

            // copy all fies in the directory

            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(dest.FullName+"\\" + fi.Name);
            }

            // for each directory in the source reapeat

            foreach (DirectoryInfo di in source.GetDirectories())
            {
                CopyDirectory(di, dest);
            }

        }
    }
}
