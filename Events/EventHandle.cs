using UnityEngine;
using System;

namespace QuickAndDirty.Events {
	public class EventHandle<T> : MonoBehaviour where T : EventHandle<T> {
		protected static void DefaultHandle() {}
		protected static void DefaultHandle(T source) {}
		public event Action _eventHandle = DefaultHandle;
		public event Action<T> _eventHandleWithSource = DefaultHandle;
		
		protected void Raise()
		{
			_eventHandle();
			_eventHandleWithSource(this as T);
		}
		
		public static void Wrap(GameObject go, Action action)
		{
			T c = go.GetComponent<T>();
			if (!c) {
				c = go.AddComponent<T>();
			}
			c._eventHandle += action;
		}

		public static void Wrap(GameObject go, Action<T> action)
		{
			T c = go.GetComponent<T>();
			if (!c) {
				c = go.AddComponent<T>();
			}
			c._eventHandleWithSource += action;
		}
	}
}