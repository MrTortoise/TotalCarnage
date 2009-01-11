using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Custom.Interfaces;
using Custom.Exceptions;


namespace Custom.ConfigFile
{
	/// <summary>
	/// Used to create a configFile object from a filename
	/// when a reference to this object is made AddReference should be called
	/// and when it is dereferenced it RemoveReference should be called
	/// This enables aggressive garbage collection safley
	/// </summary>
	public class ConfigFileLoader : IAgroGarbageCollection, IContainsResource 
	{
		#region Fields
		protected string mFileName = "";
		protected StreamReader mFile;
		protected string mCurrentLine;

		protected bool mIsOpen = false;
		protected bool mIsEOF = false;
		protected bool mIsValid = false;

		protected bool mIsDisposed = false;
		protected int mNoReferences = 0;

		protected ConfigFile mConfigFile;
				
		#endregion

		#region Constructor
		/// <summary>
		/// Use this if a filename to load will be specified later
		/// </summary>
		public ConfigFileLoader()
		{
		}
		
		/// <summary>
		/// written to take a tream from a zip file
		/// </summary>
		/// <param name="theFile"></param>
		public ConfigFileLoader(Stream theFile)
		{
			mFile = new StreamReader(theFile);
			GetNextLine();
			mIsOpen = true;
		}
		#endregion
		#region properties
		/// <summary>
		/// Gets the  path name of the config file - could be relative or absolute
		/// </summary>
		public string FileName
		{ get { return mFileName; } }

		/// <summary>
		/// gets wether the config file has been opened
		/// </summary>
		public bool IsOpen
		{ get { return mIsOpen; } }


		/// <summary>
		/// gets a clone of the config file
		/// </summary>
		public ConfigFile ConfigFile
		{ get { return (ConfigFile)mConfigFile.Clone(); } }
		#endregion
		#region public methods

		public void SetFile(string fileName)
		{
			if (fileName != "")
			{
				mFileName = fileName;
			}
		}

		/// <summary>
		/// Disposes the file object
		/// and resets its name
		/// </summary>
		public void Reset()
		{
			mFile.Dispose();
			mFileName = "";
			mIsOpen = false;
			mConfigFile = null;
			mIsEOF = false;
		}
		public void Open()
		{
			try
			{
				if (!mIsOpen)
				{
					mFile = new StreamReader(mFileName);
					GetNextLine();
					mIsOpen = true;
				}
			}
			catch (System.IO.FileNotFoundException ex)
			{
				throw new FileNotFoundException("Failed to open Config File", mFileName, ex);
			}
		}

		/// <summary>
		/// disposes the file object
		/// </summary>
		public void Close()
		{
			if (mIsOpen)
				mFile.Dispose();
			mIsOpen = false;
			mIsEOF = false;
		}

		public ConfigFile PopulateConfigFile()
		{
			mConfigFile = new ConfigFile();
			mConfigFile.FileName = mFileName;

			while (mIsEOF == false)
			{
				mConfigFile.AddSection(GetNextSection());
				GetNextLine();
			}

			return (ConfigFile)mConfigFile.Clone();
		}

		#endregion

		#region protected Methods
		protected bool IsEOF
		{ get { return mIsEOF; } }



		protected ConfigSection GetNextSection()
		{
			if (!GotoStartofNextSection())
				return null;
			
				// strip off the opening and closing []
				string SectionName = mCurrentLine.Substring(1);
				SectionName = SectionName.Remove(SectionName.Length - 1);

				ConfigSection retVal = new ConfigSection(SectionName);

				if (!GetNextLine())
					return retVal;

				string name;
				string value;

				while (!IsEndOfSection())
				{
					name = mCurrentLine.Substring(0, mCurrentLine.IndexOf("=") - 1);
					value = mCurrentLine.Substring(mCurrentLine.IndexOf("=") + 2);

					retVal.AddSectionItem(name, value);
					if (!GetNextLine())
						return retVal;
				}

				return retVal;
		}	
		

		protected bool GotoStartofNextSection()
		{
			while (!IsStartOfSection())
			{
				if (!GetNextLine())
					return false;
			}
			return true;
		}
		protected bool IsStartOfSection()
		{
			if (mCurrentLine.StartsWith("["))
			{
				if (!IsEndOfSection())
					return true;
			}
			return false;
		}
		protected bool IsEndOfSection()
		{
			if (mCurrentLine.StartsWith("[/"))
				return true;
			return false;
		}
		protected bool GetNextLine()
		{
			if (mFile.EndOfStream)
			{
				mIsEOF = true;
				return false;

			}
			else
			{
				mCurrentLine = mFile.ReadLine();
				return true;
			}
		}
		#endregion



		#region IAgroGarbageCollection Members

		public bool IsDisposed
		{
			get { return mIsDisposed; }
		}

		public int NoReferences
		{
			get { return mNoReferences; }
		}

		public void AddReference()
		{
			mNoReferences++;
		}

		public void RemoveReference()
		{
			if (mNoReferences > 0)
				mNoReferences--;
			else
				throw new NegativeReferencesException("Tried to remove a reference from config file loader when none exist"); 
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (mNoReferences == 0)
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		#endregion

		/// <summary>
		/// If manually called then this forcably disposes the Texture2D object
		/// </summary>
		/// <param name="disposing"></param>
		private void Dispose(bool disposing)
		{
			if (!mIsDisposed)
			{
				if (disposing)
				{
					mFile.Dispose();
				}
				mIsDisposed = true;
			}
		}

		~ConfigFileLoader()
		{
			Dispose(false);
		}

	}
}
