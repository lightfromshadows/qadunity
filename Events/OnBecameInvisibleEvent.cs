using UnityEngine;

namespace QuickAndDirty.Events {
public class OnBecameInvisibleEvent : EventHandle<OnBecameInvisibleEvent> {

		void OnBecameInvisible()
		{
			Raise ();
		}
}
}
