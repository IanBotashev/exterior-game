using UnityEngine;

namespace Controllers.Building_Scripts
{
    [CreateAssetMenu(fileName = "Build Item", menuName = "Build Menu Items/Build Item", order = 0)]
    public class BuildMenuItem : ScriptableObject
    {
        public GameObject buildingPrefab;
        public int cost;
    }
}