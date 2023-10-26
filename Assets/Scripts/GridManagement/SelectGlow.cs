using System.Collections.Generic;
using UnityEngine;

public class SelectGlow : MonoBehaviour
{
    #region fields
    private readonly Dictionary<Renderer, Material[]> glowMats = new();
    private readonly Dictionary<Renderer, Material[]> originMats = new();
    private readonly Dictionary<Color, Material> cachedGlowMats = new();
    public Material glowMat;
    [SerializeField] private Color selectedPathColor;
    [SerializeField] private Color selectedKapaColor;
    [SerializeField] private Color selectedKapaColorButton;
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

    #region standard glow
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

    public void ToggleGlow(bool b)
    {
        if (isGlowing == b) return;
        isGlowing = !b;
        Toggle();
    }
    #endregion

    #region path glow
    public void StartGlowPath()
    {
        if (!isGlowing) return;
        foreach (Renderer rend in glowMats.Keys)
        {
            foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", selectedPathColor); }
        }
    }
    public void ResetGlowPath()
    {
        if (!isGlowing) return;
        foreach (Renderer rend in glowMats.Keys)
        {
            foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", originColor); }
            rend.materials = glowMats[rend];
        }
    }
    #endregion

    #region kapa glow
    public void ToggleKapa()
    {
        if (!isGlowing)
        {
            foreach (Renderer rend in originMats.Keys) 
            {
                rend.materials = glowMats[rend];
                foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", selectedKapaColor); }
            }
        }
        else
        {
            foreach (Renderer rend in originMats.Keys) 
            {
                rend.materials = originMats[rend]; 
                foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", originColor); }
                rend.materials = originMats[rend];
            }
        }
        isGlowing = !isGlowing;
    }

    public void ToggleGlowKapa(bool b)
    {
        if (isGlowing == b) return;
        isGlowing = !b;
        ToggleKapa();
    }
    #endregion

    #region preselect kapa glow
    public void ToggleSelectKapa()
    {
        if (!isGlowing)
        {
            foreach (Renderer rend in originMats.Keys)
            {
                rend.materials = glowMats[rend];
                foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", selectedKapaColorButton); }
            }
        }
        else
        {
            foreach (Renderer rend in originMats.Keys)
            {
                rend.materials = originMats[rend];
                foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", originColor); }
                rend.materials = originMats[rend];
            }
        }
        isGlowing = !isGlowing;
    }

    public void ToggleSelectGlowKapa(bool b)
    {
        if (isGlowing == b) return;
        isGlowing = !b;
        ToggleKapa();
    }

    public void StartGlowButton()
    {
        if (!isGlowing) return;
        foreach (Renderer rend in glowMats.Keys)
        {
            foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", selectedKapaColorButton); }
        }
    }
    public void ResetGlowButton()
    {
        if (!isGlowing) return;
        foreach (Renderer rend in glowMats.Keys)
        {
            foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", originColor); }
            rend.materials = glowMats[rend];
        }
    }
    #endregion
    #endregion
}
