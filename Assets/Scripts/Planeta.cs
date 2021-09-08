using UnityEngine;

public class Planeta : MonoBehaviour
{
    [SerializeField] private Selecting select;
    public int ships;

    private void Update()
    {
        BuildShips();
    }

    public void BuildShips()
    {
        if (select.isSelected)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GainShips(1);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                LoseShips(1);
            }
        }
    }

    private void GainShips(int amount)
    {
        ships += amount;
    }

    private void LoseShips(int amount)
    {
        ships -= amount;
    }
}
