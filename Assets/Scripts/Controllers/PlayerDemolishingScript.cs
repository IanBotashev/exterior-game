using UnityEngine;

namespace Controllers
{
    public class PlayerDemolishingScript : PlayerControlModule
    {
        /// <summary>
        /// Tries to demolish a building on a certain sector
        /// </summary>
        public void TryToDemolsih()
        {
            if (!CanRun()) return;
            
            var selectable = GridSystem.Instance.GetGameObjectAtPos(
                GridSystem.Instance.GetMouseWorldPosition(),
                GameManager.Instance.selectableLayerMask);

            if (selectable &&selectable.GetComponent<AttachedToSector>())
            {
                selectable.GetComponent<AttachedToSector>().Demolish();
            }
        }
    }
}