using UnityEngine;

namespace Game.Animation
{
    /// <summary>
    /// For spline instantiated objects
    /// </summary>
    public class StandUpright : MonoBehaviour
    {
        private void Awake()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}