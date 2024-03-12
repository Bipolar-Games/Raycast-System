# Raycast System

**Raycast System** is a set of a few classes which help setup basic structure for detecting special interest objects in 3D world, mainly for purposes of some kind of interaction system. 



## Configuration
Configuration of the game to support and use Raycast System consists of 2 parts: (1) raycasted objects configuration and raycaster (player) configuration.

### Objects configuration
1. **Layer** - preparing a special layer for detected objects is not mandatory and raycasted objects might be on _Default_ layer as well. However it is a good practice to create a separate layer for purposes of being detected as raycasted or interactive objects.
2. **Raycast Collider** - every raycasted object needs a collider to be detected. After setting a desired layer (chosen in point 1) on colliders game object, component [RaycastCollider](https://github.com/Kosmik123/Raycast-System/blob/master/Scripts/RaycastCollider.cs) must be added. Raycast Collider represents one collider of the object, as the object might use multiple colliders.
3. **Raycast Target** - another component which must be added to raycasted object.


