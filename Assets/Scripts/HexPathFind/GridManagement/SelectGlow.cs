using System.Collections.Generic;
using UnityEngine;

public class SelectGlow : MonoBehaviour
{
    #region fields
    private Dictionary<Renderer, Material[]> glowMats = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> originMats = new Dictionary<Renderer, Material[]>();
    private Dictionary<Color, Material> cachedGlowMats = new Dictionary<Color, Material>();
    public Material glowMat;
    private bool isGlowing;
    #endregion

    #region methodes
    void Awake()
    {
        PrepareMatsDicts();
    }

    void PrepareMatsDicts()
    {
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            Material[] origins = rend.materials;
            originMats.Add(rend, origins);
            Material[] newMats = new Material[rend.materials.Length];
            for (int i = 0; i < origins.Length; i++)
            {
                Material mat = null;
                if (!cachedGlowMats.TryGetValue(origins[i].color, out mat))
                {
                    mat = new Material(glowMat);
                    mat.color = origins[i].color;
                    cachedGlowMats[mat.color] = mat;
                }
                newMats[i] = mat;
            }
            glowMats.Add(rend, newMats);
        }
    }

    public void Toggle()
    {
        if (!isGlowing)
        {
            foreach (Renderer rend in originMats.Keys) { rend.materials = glowMats[rend]; }
        }
        else
        {
            foreach (Renderer rend in originMats.Keys) { rend.materials = originMats[rend]; }
        }
        isGlowing = !isGlowing;
    }

    public void ToggleGLow(bool b)
    {
        if (isGlowing == b) return;
        isGlowing = !b;
        Toggle();
    }
    #endregion
}
