using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Army army;
    [SerializeField] private int sensitivity;
    [SerializeField] private bool isSelected;

    private void Update()
    {
        // if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            Select();         
        }

        Scroll();
    }


    private void Select()
    {
        isSelected = true;
    }

    private void Scroll()
    {
        army.soldiers += (int) Input.GetAxis("Mouse ScrollWheel") * sensitivity;

    }
}
