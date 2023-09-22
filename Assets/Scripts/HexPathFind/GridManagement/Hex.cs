using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    [SerializeField] private SelectGlow glow;

    public Vector3Int offsetCoords;

    void Awake()
    {
        offsetCoords = new HexCoordonnees(gameObject).offsetCoordonnees;
        glow = GetComponent<SelectGlow>();
    }
    public void EnableGlow()
    {
        glow.ToggleGLow(true);
    }
    public void DisableGlow()
    {
        glow.ToggleGLow(false);
    }
}
