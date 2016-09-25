// This stores common code
using UnityEngine;
using System;

namespace Affdex
{
    /// <summary>
    /// Class of utilities implemented as static methods.
    /// </summary>
    public static class AffdexUnityUtils
	{
		/// <summary>
		/// Checks to see if Application.platform is supported by Affectiva's plugin
		/// </summary>
		/// <returns>True or False</returns>
		public static bool ValidPlatform ()
		{
			if (RuntimePlatform.WindowsEditor == Application.platform || RuntimePlatform.WindowsPlayer == Application.platform) {
				// Return false for 32-bit until we support 32-bit
				if (IntPtr.Size == 4) 
				{
					Debug.Log ("32-Bit Windows is not currently supported by Affectiva's Unity Asset!");
					return false;
				}
				return true;
			}
			else if (RuntimePlatform.OSXEditor == Application.platform || RuntimePlatform.OSXPlayer == Application.platform)
				return true;

			Debug.Log (Application.platform + " is not currently supported by Affectiva's Unity Asset!");
			return false;
		}
	}
}

