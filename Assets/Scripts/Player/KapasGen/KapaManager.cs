using UnityEngine;

public class KapaManager : MonoBehaviour
{
    public void NormalAttack(Unit unit) => unit.KapasList[0].Execute();
    public void Competence(Unit unit) => unit.KapasList[1].Execute();
    public void Ultimate(Unit unit) => unit.KapasList[2].Execute();
    public void Turret(Unit unit) => unit.KapasList[3].Execute();
    public void Skip(Unit unit) => unit.KapasList[4].Execute();
}
