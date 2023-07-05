using UnityEngine;

namespace Bipolar.RaycastSystem
{
#if ENABLE_LEGACY_INPUT_MANAGER
    public class InputManagerRayFromMouseProvider : RayFromMouseProvider
    {
        protected override Vector2 GetScreenPosition()
        {
            return Input.mousePosition;
        }
    }
#endif
}