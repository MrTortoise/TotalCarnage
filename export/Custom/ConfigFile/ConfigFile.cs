using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.ConfigFile
{
	/// <summary>
	/// Represents a config file with Named Sections containing 
	/// parameters and values.
	/// This object is populated by ConfigFileLoader
	/// </summary>
	public class ConfigFile :ICloneable, IEquatable<ConfigFile>
	{
		
		#region Fields
		protected Dictionary<string, ConfigSection> mConfigSections;
		protected string mFileName;
		#endregion
		#region Constructor

		public ConfigFile()
		{
			mConfigSections = new Dictionary<string, ConfigSection>();
		}
		#endregion

		#region Properties
		public string FileName
		{
			get { return mFileName; }
			set
			{
				if (value != "")
					mFileName = value;
			}
		}
		public int Count
		{ get { return mConfigSections.Count; } }
		#endregion
		#region public methods

		public void AddSection(ConfigSection theSection)
		{
			
			if (!mConfigSections.ContainsKey(theSection.SectionName))
			{
				if (!Contains(theSection))
				{
					mConfigSections.Add(theSection.SectionName, theSection);
				}
			} 					
		}

		public bool Contains(ConfigSection section)
		{
			if (mConfigSections.Count > 0)
			{
				foreach (KeyValuePair<string, ConfigSection> kvp in mConfigSections)
				{
					if (kvp.Value.Equals(section))
					{ return true; }
				}				
			}
			return false;
		}

		public bool Contains(string Section)
		{
			return mConfigSections.ContainsKey(Section);
		}

		public ConfigSection GetSection(string SectionName)
		{
			if (mConfigSections.ContainsKey(SectionName))
			{
				return mConfigSections[SectionName];
			}
			return null;
		}

		public string GetValue(string Section, string Paramater)
		{
			if (Contains(Section))
			{
				return mConfigSections[Section].GetItem(Paramater);
			}
			else return "";
		}
		#endregion

		#region Overrided Methods
		public override string ToString()
		{
			StringBuilder ret = new StringBuilder();
			ret.AppendLine("Config File Name : " + mFileName);
			foreach (KeyValuePair<string, ConfigSection> kvp in mConfigSections)
			{
				ret.AppendLine(kvp.Value.ToString());
			}
			ret.AppendLine(mConfigSections.ToString());
			ret.AppendLine("End of Config File");
			return ret.ToString();
		}

		public override int GetHashCode()
		{
			return (mFileName.GetHashCode() + mConfigSections.GetHashCode()).GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ConfigFile cf = obj as ConfigFile;
			if (cf!=null)
			{return cf.Equals(this);}
			return false;
		}

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			ConfigFile cf = new ConfigFile();
			cf.FileName = mFileName;
			foreach (KeyValuePair<string, ConfigSection> kvp in mConfigSections)
			{
				cf.AddSection(kvp.Value);
			}
			return cf;
		}

		#endregion

		#region IEquatable<ConfigFile> Members

		public bool Equals(ConfigFile other)
		{
			// if names not equal return false
			if (this.FileName==other.FileName)
			{
				// if number not equal return false
				if (this.mConfigSections.Count==other.mConfigSections.Count)
				{
					// loop through the collection ... if any not equal return false
					// if arrive at end of collection then must be equal
					foreach (KeyValuePair<string, ConfigSection> kvp in mConfigSections)
					{
						// test to see if the key is in the collection
						if (other.mConfigSections.ContainsKey(kvp.Key))
						{
							// if the sections are not equal then return false
							if (!other.mConfigSections[kvp.Key].Equals(kvp.Value))
							{ return false; }
						}
							// if key not in colelction rturn false
						else									   
						{ return false; }
					}
					//if got to here then all items in the collection must match
					return true;
				}
			}
			//if got to here then initial conditions must not of been met
			return false;												 
		}

		

		#endregion
	}
}
