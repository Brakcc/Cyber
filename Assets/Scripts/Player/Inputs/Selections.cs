using UnityEngine;
using UnityEngine.Events;

public class Selections : MonoBehaviour
{
    #region fields
    [SerializeField] private Camera mainC;
    [SerializeField] private HexGridStore hexGrid;
    private InputsManager inputsMan;

    public LayerMask tileSelectionMask;

    public UnityEvent<GameObject> Selection;
    public UnityEvent<GameObject> Unit;
    #endregion

    #region methodes
    void Awake()
    {
        if (mainC == null) { mainC = Camera.main; }
        inputsMan = new InputsManager();
    }

    public void HandleClick(Vector3 mousePos)
    {
        GameObject result;
        if (inputsMan.FocV2(tileSelectionMask, mainC, mousePos, out result))
        {
            if (UnitSelected(result)) { Unit?.Invoke(result); }
            else { Selection?.Invoke(result); }
        }
    }

    bool UnitSelected(GameObject result) => result.GetComponent<Unit>() != null;
    #endregion
}
