using UnityEngine;
using TMPro;
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
    [SerializeField] private KeyCode increase;
    [SerializeField] private KeyCode decrease;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currentShipsText;
    [SerializeField] private TextMeshProUGUI shipsToSendText;
    [SerializeField] private TextMeshProUGUI tips;
    [SerializeField] private int shipsToSend;
    [SerializeField] private int minShipsToSend;
    [SerializeField] private Spawner spawner;

    private void Update()
    {
        // if select key is pressed and nothing is selected
        if (Input.GetKeyDown(select)) Select();
        // else if select key is pressed and something is selected
        else if (Input.GetKeyDown(unselect) && isSelected) Unselect();

        PlanetInfo();
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
                    tips.text = "You don't control that planet!";
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
                    // selectedPlanet spaceships are sent to attackedPlanet 
                    Attack();
                    print($"attacking {attackedPlanet.gameObject.name}");
                }
                else
                {
                    // else attackedPlanet is set to null
                    attackedPlanet = null;
                    tips.text = "You can't attack your own planets!";
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
        // display nothing
        currentShipsText.text = "";
        shipsToSendText.text = "";
        shipsToSend = minShipsToSend;
    }

    private void PlanetInfo()
    {
        // if player owned planet is selected
        if (isSelected)
        {
            // display current amount of ships on that planet
            currentShipsText.text = selectedPlanet.GetComponent<Planet>().planetShips.ToString();
            // choose how many ships to send
            ChooseShips();
        }
    }

    private void ChooseShips()
    {
        if (Input.GetKeyDown(increase))
        {
            shipsToSend++;
            shipsToSend = Mathf.Clamp(shipsToSend, minShipsToSend, selectedPlanet.GetComponent<Planet>().planetShips);
        }
        else if (Input.GetKeyDown(decrease))
        {
            shipsToSend--;
            shipsToSend = Mathf.Clamp(shipsToSend, minShipsToSend, selectedPlanet.GetComponent<Planet>().planetShips);
        }
        shipsToSendText.text = shipsToSend.ToString();
    }

    private void Attack()
    {
        if (selectedPlanet.GetComponent<Planet>().planetShips > shipsToSend)
        {
            spawner.SpawnSpaceShip(shipsToSend, attackedPlanet, selectedPlanet);
            selectedPlanet.GetComponent<Planet>().planetShips -= shipsToSend; 
        }
        else
        {
            tips.text = "You don't have enough ships on this planet!";
        }
    }
}
