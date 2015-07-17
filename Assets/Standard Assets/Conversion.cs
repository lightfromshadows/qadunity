using UnityEngine;
using System;

namespace QuickAndDirty {
	public class Conversion {
		/*
		 * Converts a Color32 to a hexidecimal string that's been formatted for use with Unity's UI Rich Text
		 */
		public static string ColorToHexString(Color32 color)
		{
			string retval;
			byte[] colorData = {color.r, color.g, color.b, color.a};

			retval = BitConverter.ToString(colorData).Replace("-", string.Empty);

			return retval;
		}
	}
}
