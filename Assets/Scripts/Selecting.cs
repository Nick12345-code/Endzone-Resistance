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

    // player can select a friendly planet and a planet to attack
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

    // player can unselect everything by pressing the unselect key
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

    // updates the HUD to show information on selected planets
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

    // player can choose the number of ships they want to send to attack
    private void ChooseShips()
    {
        // if increase button is pressed
        if (Input.GetKeyDown(increase))
        {
            // increase how many ships you are sending by 1
            shipsToSend++;

            // clamps how many ships you can send to a min and max
            shipsToSend = Mathf.Clamp(shipsToSend, minShipsToSend, selectedPlanet.GetComponent<Planet>().planetShips);
        }
        // else if decrease button is pressed
        else if (Input.GetKeyDown(decrease))
        {
            // decrease how many ships you are sending by 1
            shipsToSend--;

            // clamps how many ships you can send to a min and max
            shipsToSend = Mathf.Clamp(shipsToSend, minShipsToSend, selectedPlanet.GetComponent<Planet>().planetShips);
        }

        // updates the HUD text
        shipsToSendText.text = shipsToSend.ToString();
    }

    // sends ships from the selected planet to attack another planet
    private void Attack()
    {
        // if the planet has enough ships to send
        if (selectedPlanet.GetComponent<Planet>().planetShips > shipsToSend)
        {
            // spawn ships that attack the attackPlanet sent from the selectedPlanet
            spawner.SpawnSpaceShip(shipsToSend, attackedPlanet, selectedPlanet);

            // update the amount of ships on the planet that sent them
            selectedPlanet.GetComponent<Planet>().planetShips -= shipsToSend; 
        }
        else
        {
            tips.text = "You don't have enough ships on this planet!";
        }
    }
}
