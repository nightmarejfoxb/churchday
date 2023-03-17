using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int curDay;
    public int money;
    public int cropInventory;

    public CropData selectedCarrotToPlant;
    public CropData selectedWhetToPlant;
    public CropData selectedPotatoToPlant;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI CropsHaveText;

    public CropControl cropControl;

    public event UnityAction onNewDay;

    // Singleton
    public static GameManager instance;

    void OnEnable ()
    {
        Crop.onPlantCrop += OnPlantCrop;
        Crop.onHarvestCrop += OnHarvestCrop;
    }

    void OnDisable ()
    {
        Crop.onPlantCrop -= OnPlantCrop;
        Crop.onHarvestCrop -= OnHarvestCrop;
    }

    void Awake ()
    {
        // Initialize the singleton.
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Called when we press the next day button.
    public void SetNextDay ()
    {
        curDay++;
        onNewDay?.Invoke();
        UpdateStatsText();
    }

    // Called when a crop has been planted.
    // Listening to the Crop.onPlantCrop event.
    public void OnPlantCrop (CropData cop)
    {
        cropInventory--;
        UpdateStatsText();
    }

    // Called when a crop has been harvested.
    // Listening to the Crop.onCropHarvest event.
    public void OnHarvestCrop (CropData crop)
    {
        money += crop.sellPrice;
        UpdateStatsText();
    }

    // Called when we want to purchase a crop.
    public void PurchaseCrop (CropData crop)
    {
        money -= crop.purchasePrice;
        
        //Old Code
        cropInventory++;
        UpdateStatsText();

        //New and More Betterer code
        if(crop.name.Equals("Wheat"))
        {
            cropControl.wheatStock++;
        }
        if (crop.name.Equals("carrots"))
        {
            cropControl.carrotStock++;
        }
        if (crop.name.Equals("Potato"))
        {
            cropControl.potatoStock++;
        }


        cropControl.UpdateCropsDisplay();


    }

    // Do we have enough crops to plant?
    public bool CanPlantCrop ()
    {
        return cropInventory > 0;
    }

    // Called when the buy crop button is pressed.
    public void OnBuyCropButton (CropData crop)
    {
        if(money >= crop.purchasePrice)
        {
            PurchaseCrop(crop);
        }
    }

    // Update the stats text to display our current stats.
    void UpdateStatsText ()
    {
        statsText.text = $"Day: {curDay}\nMoney: ${money}\nCrop Inventory: {cropInventory}";
    }
}