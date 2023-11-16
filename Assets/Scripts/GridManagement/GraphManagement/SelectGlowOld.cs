using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectGlowOld
{
    #region fields
    //Dicts de materials
    private readonly Dictionary<Renderer, Material[]> glowMats = new();
    private readonly Dictionary<Renderer, Material[]> originMats = new();
    private readonly Dictionary<Color, Material> cachedGlowMats = new();

    //couleurs et materials de base pour le glow

    [SerializeField] private Material glowMat;
    [SerializeField] Color selectedPathColor;
    [SerializeField] Color selectedKapaColor;
    [SerializeField] Color selectedKapaColorButton;
    
    //Simple glow and color logic
    private Color originColor;
    private bool isGlowing;
    private Hex thisHex;
    #endregion

    #region methodes
    public void SetGlow(Hex h) 
    {
        thisHex = h;
        PrepareMatsDicts();
        originColor = glowMat.GetColor("_GlowColor");
    }

    void PrepareMatsDicts()
    {
        foreach (Renderer rend in thisHex.GetComponentsInChildren<Renderer>())
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
    void Toggle()
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
    void ToggleKapa()
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

    public void GlowKapaOnButton()
    {
        if (!isGlowing) return;
        foreach (Renderer rend in glowMats.Keys)
        {
            foreach (Material m in glowMats[rend]) { m.SetColor("_GlowColor", selectedKapaColor); }
        }
    }
    #endregion

    #region preselect kapa glow
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