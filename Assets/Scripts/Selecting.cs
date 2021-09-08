using UnityEngine;

public class Selecting : MonoBehaviour
{
    public bool isSelected;                                 // is something selected or not
    [SerializeField] private KeyCode select;                // key used to select

    private void Update()
    {
        // if select key is pressed and nothing is selected
        if (Input.GetKeyDown(select) && !isSelected)
        {
            Select();         
        }
        // else if select key is pressed and something is selected
        else if (Input.GetKeyDown(select) && isSelected)
        {
            Unselect();
        }
    }


    private void Select()
    {
        // stores info on what ray hits
        RaycastHit hit;

        // ray is shot from center of camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ray is casted
        if (Physics.Raycast(ray, out hit))
        {
            // if it hits a selectable object
            if (hit.collider.CompareTag("Planet"))
            {
                Selected();
            }
        }
    }

    private void Selected()
    {
        // object is selected
        isSelected = true;
    }

    private void Unselect()
    {
        // object is unselected
        isSelected = false;
    }
}
