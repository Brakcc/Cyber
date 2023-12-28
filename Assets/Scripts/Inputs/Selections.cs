using GameContent.Entity.Unit.UnitWorking;
using UnityEngine;
using UnityEngine.Events;

namespace Inputs
{
    public class Selections : MonoBehaviour
    {
        #region fields
        [SerializeField] private Camera mainC;
        private InputsManager _inputsMan;

        public LayerMask tileSelectionMask;

        public UnityEvent<GameObject> selection;
        public UnityEvent<GameObject> unit;
        #endregion

        #region methodes

        private void Awake()
        {
            if (mainC == null) { mainC = Camera.main; }
            _inputsMan = new();
        }

        public void HandleClick(Vector3 mousePos)
        {
            if (_inputsMan.FocV2(tileSelectionMask, mainC, mousePos, out GameObject result))
            {
                if (UnitSelected(result)) { unit?.Invoke(result); }
                else { selection?.Invoke(result); }
            }
        }

        private bool UnitSelected(GameObject result) => result.GetComponent<Unit>() != null;
        #endregion
    }
}
