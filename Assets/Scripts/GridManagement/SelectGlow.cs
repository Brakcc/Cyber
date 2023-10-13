using System.Collections.Generic;
using UnityEngine;

public class SelectGlow : MonoBehaviour
{
    #region fields
    private Dictionary<Renderer, Material[]> glowMats = new Dictionary<Renderer, Material[]>();
    private Dictionary<Renderer, Material[]> originMats = new Dictionary<Renderer, Material[]>();
    private Dictionary<Color, Material> cachedGlowMats = new Dictionary<Color, Material>();
    public Material glowMat;
    private Color selectedPathColor = Color.green;
    private Color originColor;
    private bool isGlowing;
    #endregion

    #region methodes
    void Awake()
    {
        PrepareMatsDicts();
        originColor = glowMat.GetColor("_GlowColor");
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
                if (!cachedGlowMats.TryGetValue(origins[i].color, out Material mat))
                {
                    mat = new Material(glowMat) { color = origins[i].color };
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

    public void StartGlowPath()
    {
        if (!isGlowing) return;
        foreach (Renderer rend in glowMats.Keys)
        {
            foreach (Material m in glowMats[rend])
            {
                m.SetColor("_GlowColor", selectedPathColor);
            }
        }
    }
    public void ResetGlowPath()
    {
        if (!isGlowing) return;
        foreach (Renderer rend in glowMats.Keys)
        {
            foreach (Material m in glowMats[rend])
            {
                m.SetColor("_GlowColor", originColor);
            }
            rend.materials = glowMats[rend];
        }
    }
    #endregion
}
