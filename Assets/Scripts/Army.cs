using UnityEngine;

public class Army : MonoBehaviour
{
    public int soldiers;

    public void GainSoldiers(int amount)
    {
        soldiers += amount;
    }

    public void LoseSoldiers(int amount)
    {
        soldiers -= amount;
    }
}
