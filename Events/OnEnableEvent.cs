using UnityEngine;
using System.Collections;

namespace QuickAndDirty.Events {
public class OnEnableEvent : EventHandle<OnEnableEvent> {

	void OnEnable()
	{
		Raise();
	}
}
}
