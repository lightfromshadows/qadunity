using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace QuickAndDirty.Events {
public class OnDestroyEvent : EventHandle<OnDestroyEvent> {
	public UnityEvent eventHandle;

	void OnDestroy()
	{
		Raise();
	}
}
}