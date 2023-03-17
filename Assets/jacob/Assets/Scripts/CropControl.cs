using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CropControl : MonoBehaviour
{
    //Crops

    public int carrotStock;
    public int potatoStock;
    public int wheatStock;

    //Display Box
    public TextMeshProUGUI cropsHaveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCropsDisplay()
    {
        string carrotText = "Carrots: " + carrotStock;
        string potatoText = "Potatoes: " + potatoStock;
        string wheatText = "Wheat: " + wheatStock;

        string updatedDisplayText = carrotText + "\n" + potatoText + "\n" + wheatText;

        cropsHaveText.text = updatedDisplayText;
    }
}
