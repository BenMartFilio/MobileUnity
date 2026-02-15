using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private LifeBarGestion lifeBar;
    public int bonus = 1;
    private int PV;
    private int basePV = 3;

    private void Start()
    {
        PV = basePV;
        lifeBar.OnLifeUpdate(PV);
    }

    public void MinusLife()
    {
        LifeGestion(-1);
    }

    public void BonusLife()
    {
        LifeGestion(1);
    }

    public void LifeGestion(int HowManyPV)
    {
        PV += HowManyPV;
        lifeBar.OnLifeUpdate(PV);
    }
    
}
