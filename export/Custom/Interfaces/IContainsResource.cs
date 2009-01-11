using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Interfaces
{
	/// <summary>
	/// To be used with an object that contains an expensive resource
	/// provides methods for managing the resource
	/// </summary>
	public interface IContainsResource
	{
		/// <summary>
		/// Returns wether or not the resource has been opened or not
		/// </summary>
		bool IsOpen
		{ get; }

		/// <summary>
		/// Opens the resource - assuming that pre-requisites met
		/// </summary>
		void Open();

		/// <summary>
		/// disposes of the resource. ReOpen it to use again
		/// </summary>
		void Close();
	}
}
