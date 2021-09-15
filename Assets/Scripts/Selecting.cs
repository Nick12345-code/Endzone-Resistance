using UnityEngine;
using EndzoneResistance.Cleon;

public class Selecting : MonoBehaviour
{
    [Header("Selection Feedback")]
    public bool isSelected;   
    public Planet selectedPlanet;
    public Planet attackedPlanet;
    [Header("Controls")]
    [SerializeField] private KeyCode select;
    [SerializeField] private KeyCode unselect;

    private void Update()
    {
        // if select key is pressed and nothing is selected
        if (Input.GetKeyDown(select)) Select();
        // else if select key is pressed and something is selected
        else if (Input.GetKeyDown(unselect) && isSelected) Unselect();
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
            // if ray hits a planet and nothing is already selected
            if (hit.collider.CompareTag("Planet") && !isSelected)
            {
                // the hit Planet is stored in selectedPlanet
                selectedPlanet = hit.collider.gameObject.GetComponent<Planet>();

                // if selectedPlanet is owned by the player
                if (selectedPlanet.team == Team.Player)
                {
                    // selection is confirmed
                    isSelected = true;
                }
                else
                {
                    // else selected planet is set to null
                    selectedPlanet = null;
                    print("You don't control that planet currently!");
                }
            }
            // else if ray hits a Planet and something is already selected
            else if (hit.collider.CompareTag("Planet") && isSelected)
            {
                // the hit planet is stored in attackedPlanet
                attackedPlanet = hit.collider.gameObject.GetComponent<Planet>();

                // if attackedPlanet is not owned by the player
                if (attackedPlanet.team != Team.Player)
                {
                    // current selectedPlanet spaceships are sent to attack the current attackedPlanet 
                    print($"attacking {attackedPlanet.gameObject.name}");
                }
                else
                {
                    // else attackedPlanet is set to null
                    attackedPlanet = null;
                    print("You can't attack your own planets!");
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
