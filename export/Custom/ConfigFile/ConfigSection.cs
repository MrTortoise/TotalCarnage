using System;
using System.Collections.Generic;
using System.Text;
using Custom.Exceptions;


namespace Custom.ConfigFile
{
	/// <summary>
	/// Has a name and contains a dictionary of <string,string>	
	/// represents a sectinn containing key value pairs
	/// </summary>
	public class ConfigSection : IEquatable<ConfigSection>, ICloneable 
	{
		#region Fields
		protected string mSectionName;
		protected Dictionary<string,string> mSectionItems;
		#endregion

		#region Constructor
		public ConfigSection(string sectionName)
		{
			if (sectionName.Length == 0)
			{ throw new ArgumentEmptyStringException("tried to create a config section with no name"); }

			mSectionName = sectionName;
			mSectionItems = new Dictionary<string, string>();

		}
		#endregion
		#region Properties
		public string SectionName
		{ get { return mSectionName; } }

		public Dictionary<string,string> SectionItems
		{ get { return mSectionItems; } }
		#endregion
		#region Public Methods

		/// <summary>
		/// Adds an entry to the Config Section
		/// consists of unique name and value
		/// If name exists then existing value is overwritten
		/// </summary>
		/// <param name="Name">Cannot be an Empty String</param>
		/// <param name="value">Cannot be an empty string</param>
		public void AddSectionItem(string Name, string value)
		{
			if (Name == "")
				throw new ArgumentEmptyStringException("tried to add a section with no name");
			if (value == "")
				throw new ArgumentEmptyStringException("tried to add a non value");
			if (mSectionItems.ContainsKey(Name))
			{
				mSectionItems[Name]=value;
			}
			else{
				mSectionItems.Add(Name, value);
			}
		}

		/// <summary>
		/// returns the value of the specified item
		/// from this config section object
		/// if the name doesn't exist an empty string is returned
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
		public string GetItem(string Name)
		{
			if (mSectionItems.ContainsKey(Name))
			{ return mSectionItems[Name]; }

			return "";
		}

		#endregion
		#region Overrided Methods

		/// <summary>
		/// outputs the object in the format of the cfg file
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder desc = new StringBuilder();
			desc.AppendLine("[" + SectionName +"]");
			
			foreach (KeyValuePair<string,string> kvp in mSectionItems)
			{
				desc.AppendLine(kvp.Key + " = " + kvp.Value);
			}
			desc.AppendLine("[/"+SectionName+"]");
			return desc.ToString();
		}

		public override int GetHashCode()
		{
			return (mSectionName.GetHashCode() + mSectionItems.GetHashCode()).GetHashCode();
		}

		/// <summary>
		/// casts to ConfigType and then test for value equality
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
				ConfigSection c = obj as ConfigSection;
				if (c != null)
				{ return c.Equals(this); }

				return false;
		}

		#endregion


		#region IEquatable<ConfigSection> Members

		/// <summary>
		/// Tests for equality by Parameter value
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(ConfigSection other)
		{
			// if null return false
			if ((object)other == null)
			{ return false; }
			
			// if section names are different then we know !=
			if (other.mSectionName==mSectionName)
			{
				// if have different number then we know !=
			   if(other.mSectionItems.Count==mSectionItems.Count)
			   {
				   // loop through and hunt for a not equal case
				   foreach(KeyValuePair<string,string> kvp in mSectionItems)
				   {
					   //check to see is other contains the key in mSectionItems
					   if (other.mSectionItems.ContainsKey(kvp.Key))
					   {
						   // iof so then test its value against this objects
						   // If any key value pair doesn't have a match then we know not equal
						   if (other.mSectionItems[kvp.Key] != kvp.Value)
							   return false;
					   }
					   else
					   {
						   // if the key is not in the list then they are not equal
						   return false;
					   }
				   }
				   //if we get this far then all items matched
				   return true;
			   }
			}
			// if we get here then the items are not equal
			return false;
		}

		#endregion

		#region ICloneable Members

		/// <summary>
		/// returns a new instance that is a copy by value
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			ConfigSection cf = new ConfigSection(mSectionName);
			foreach (KeyValuePair<string, string> kvp in mSectionItems)
			{
				cf.AddSectionItem(kvp.Key, kvp.Value);
			}
			return cf;
		}

		#endregion
	}
}
