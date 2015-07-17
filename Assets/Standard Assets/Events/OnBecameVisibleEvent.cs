using UnityEngine;

namespace QuickAndDirty.Events {
	public class OnBecameVisibleEvent : EventHandle<OnBecameVisibleEvent> {
		
		void OnBecameVisible()
		{
			Raise ();
		}
	}
}
