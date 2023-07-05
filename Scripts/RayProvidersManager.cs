using UnityEngine;

namespace Bipolar.RaycastSystem
{
    public class RayProvidersManager : MonoBehaviour
    {
        [SerializeField]
        private RaycastController raycastController;
        public RaycastController RaycastController
        {
            get => raycastController;
            set
            {
                raycastController = value;
            }
        }

        public static void SetRayProvider(RaycastController controller, RayProvider rayProvider)
        {
            controller.RayProvider = rayProvider;
        }

        public void SetRayProvider(RayProvider rayProvider)
        {
            if (raycastController != null)
                SetRayProvider(raycastController, rayProvider);
        }    

        public static void SetRayProvider<T>(RaycastController controller) where T : RayProvider
        {
            T rayProvider = controller.GetComponent<T>();
            if (rayProvider == null)
                rayProvider = controller.gameObject.AddComponent<T>();

            SetRayProvider(controller, rayProvider);
        }

        public void SetRayProvider<T>() where T : RayProvider
        {
            if (raycastController != null)
                SetRayProvider<T>(raycastController);
        }
    }
}
