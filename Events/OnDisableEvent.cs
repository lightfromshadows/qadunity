using UnityEngine;
using System.Collections;

namespace QuickAndDirty.Events {
public class OnDisableEvent : EventHandle<OnDisableEvent> {
	void OnDisable()
	{
		Raise();
	}
}
}