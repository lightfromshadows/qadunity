# QADUnity
Quick And Dirty for Unity is a collection of helper classes that I've found make life easier. The overall mindset, as the name suggests, is to facilitate getting code working quicky and correctly.


# Math
A lot of seemingly random functions exist here for everything from Local-To-Local transformations to a storage free averages to Sign. (ikr?)

# Data
Helpers for cramming in-game data (ie inspector-exposed structs/classes) into PlayerPrefs and getting it back out again.

# Events
Sometimes you just wanna know when another object has receieved one of it's built in events such as OnDestroy or OnEnable without being tightly coupled to the object. The classes in QuickAndDirty.Events make it easy to drop a listener component that rebroadcasts events. Supports anonymous lamdas.

# Splash
A simple way to quickly create particle systems that are unique and complex. The ParticleController and base classes serve to make it possible to influence all the particles in a system by overriding a single function. _**NOTE** I don't update Splash often (which is a big part of why it's in QuickAndDirty instead of having it's own repo). It's not really optimized and I still haven't gotten around to custom initializers yet._

# TransformUtils
Some functions in this are deprecated since Unity has now included native versions of them.
