using UnityEngine;
using EndzoneResistance.Cleon;

public class Selecting : MonoBehaviour
{
    [SerializeField] private SpaceShip spaceship;
    [SerializeField] private Planet planet;

    public bool isSelected;   
    public Planet selectedPlanet;
    public Planet attackedPlanet;
    [SerializeField] private KeyCode select;
    [SerializeField] private KeyCode unselect;

    private void Update()
    {
        // if select key is pressed and nothing is selected
        if (Input.GetKeyDown(select))
        {
            Select();         
        }
        // else if select key is pressed and something is selected
        else if (Input.GetKeyDown(unselect) && isSelected)
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
            if (hit.collider.CompareTag("Planet") && !isSelected)
            {
                selectedPlanet = hit.collider.gameObject.GetComponent<Planet>();
                isSelected = true;
            }
            else if (hit.collider.CompareTag("Planet") && isSelected)
            {
                attackedPlanet = hit.collider.gameObject.GetComponent<Planet>();

                if (attackedPlanet != selectedPlanet && attackedPlanet.team != Team.Player)
                {
                    //spaceship.MoveSpaceShip();
                    print($"attacking {attackedPlanet.gameObject.name}");
                }
                else
                {
                    attackedPlanet = null;
                }
            }
        }
    }

    private void Unselect()
    {
        // object is unselected
        isSelected = false;
        selectedPlanet = null;
        attackedPlanet = null;
    }
}
