using UnityEngine;

namespace Gardening.UI
{
    public class Tooltip : MonoBehaviour
    {
        protected void AttachToTransform(Transform targetTransform, float offset)
        {
            var targetPos = targetTransform.position;
            transform.position = new Vector3(targetPos.x, targetPos.y + offset, targetPos.z);
        }
    }
}