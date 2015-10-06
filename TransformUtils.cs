using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

namespace QuickAndDirty
{
    public class TransformUtils
    {
		public static bool verbose = false;

		public static bool Between(Vector3 point, Transform lhs, Transform rhs)
		{
			Plane lhsPlane = new Plane((rhs.position - lhs.position).normalized, lhs.position);
			Plane rhsPlane = new Plane(-lhsPlane.normal, rhs.position);

			return lhsPlane.GetSide(point) && rhsPlane.GetSide(point);
		}

        public static T GetComponentUpwards<T>(Transform transform) where T : Component
        {
            T retval = transform.GetComponent<T>();
            if (retval)
            {
                return retval;
            }
            else if (retval == null && transform.parent)
            {
                return GetComponentUpwards<T>(transform.parent);
            }
            return retval;
        }

        public static void SetEnabled<T>(Transform root, bool enabled, bool recursive = true) where T : Component
        {
        	if (recursive)
        	{
        		SetPropertiesInChildren<T, bool>(root, "enabled", enabled);
        	}
        	else
        	{
				SetProperties<T, bool>(root, "enabled", enabled);
        	}
        }

        public static void SetProperty<T, Y>(T comp, Y value, string propertyName) where T : Component
        {
        	Type type = comp.GetType();
        	PropertyInfo prop = type.GetProperty("enabled");
        	if (prop != null)
        	{
        		prop.SetValue(comp, Convert.ChangeType(value, typeof(Y)), null);
        	}
        	else if (verbose)
        	{
				Debug.Log(string.Format("Type {0} does not contain property named {1}", type, propertyName));
        	}
        }

        public static void SetProperties<T, Y>(Transform root, string propertyName, Y value) where T : Component
        {
        	foreach (T comp in root.GetComponents<T>())
        	{
        		SetProperty<T, Y>(comp, value, propertyName);
        	}
        }

        public static void SetPropertiesInChildren<T, Y>(Transform root, string propertyName, Y value) where T : Component
        {
        	foreach (T comp in root.GetComponentsInChildren<T>())
        	{
        		SetProperty<T, Y>(comp, value, propertyName);
        	}
        }
    }
}