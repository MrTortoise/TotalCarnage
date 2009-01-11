using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Utils.Zip;
using Custom.Interfaces;
using System.IO;

namespace Custom.Zip
{
	public class ZipFile : IContainsResource 
	{	
		protected Ionic.Utils.Zip.ZipFile mZipFile;  	

		protected string mFileName;
		public string FileName
		{
			get { return mFileName; }
			set { mFileName = value; }
		}

		public void CreateFile(string fullPath)
		{
			mZipFile = new Ionic.Utils.Zip.ZipFile(fullPath);
			mZipFile.Save();
		}


		public Stream GetFile(string theFileName)
		{
			Stream retVal = null;
			foreach (ZipEntry z in mZipFile)
			{
				if (z.FileName == theFileName)
				{
					z.Extract(retVal);
					return retVal;
				}
			}
			return null;
		}

		#region IContainsResource Members

		protected bool mIsOpen = false;
		public bool IsOpen
		{
			get { return mIsOpen ; }
		}

		public void Open()
		{
			if (mIsOpen == true)
			{
				Close();
			}
			mZipFile = new Ionic.Utils.Zip.ZipFile(mFileName);
			mIsOpen = true;
		}

		public void Close()
		{
			if (mIsOpen)
			{
				mZipFile.Dispose();
				mZipFile = null;
				mIsOpen = false;
			}
		}

		#endregion
	}
}
