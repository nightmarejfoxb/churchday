using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    private Crop curCrop;
    public GameObject [] cropPrefabs;

    public SpriteRenderer sr;
    private bool tilled;

    [Header("Sprites")]
    public Sprite grassSprite;
    public Sprite tilledSprite;
    public Sprite wateredTilledSprite;

    public int selectedCrop = 0;

    /*
     * Selected Crops
     * 0 --> Carrot
     * 1 --> Wheat
     * 2 --> Potato
     */

    void Start ()
    {
        // Set the default grass sprite.
        sr.sprite = grassSprite;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedCrop = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedCrop = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedCrop = 2;
        }

    }
    // Called when the player interacts with the tile.
    public void Interact ()
    {
        
        if(!tilled)
        {
            Till();
        }
        else if(!HasCrop() && GameManager.instance.CanPlantCrop())
        {

            if(selectedCrop == 0)
            {
                PlantNewCrop(GameManager.instance.selectedCarrotToPlant);
            }
            else if(selectedCrop == 1)
            {
                PlantNewCrop(GameManager.instance.selectedWhetToPlant);
            }
            else if(selectedCrop == 2)
            {
                PlantNewCrop(GameManager.instance.selectedPotatoToPlant);
            }
                
        }
        else if(HasCrop() && curCrop.CanHarvest())
        {
            curCrop.Harvest();
        }
        else
        {
            Water();
        }
    }

    // Called when we interact with a tilled tile and we have crops to plant.
    void PlantNewCrop (CropData crop)
    {
        if(!tilled)
            return;

        curCrop = Instantiate(cropPrefabs[0], transform).GetComponent<Crop>();
        curCrop.Plant(crop);

        GameManager.instance.onNewDay += OnNewDay;
    }

    // Called when we interact with a grass tile.
    void Till ()
    {
        tilled = true;
        sr.sprite = tilledSprite;
    }

    // Called when we interact with a crop tile.
    void Water ()
    {
        sr.sprite = wateredTilledSprite;

        if(HasCrop())
        {
            curCrop.Water();
        }
    }

    // Called every time a new day occurs. 
    // Only called if the tile contains a crop.
    void OnNewDay ()
    {
        if(curCrop == null)
        {
            tilled = false;
            sr.sprite = grassSprite;

            GameManager.instance.onNewDay -= OnNewDay;
        }
        else if(curCrop != null)
        {
            sr.sprite = tilledSprite;
            curCrop.NewDayCheck();
        }
    }

    // Does this tile have a crop planted?
    bool HasCrop ()
    {
        return curCrop != null;
    }
}