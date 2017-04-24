using System.Linq;
using Enums;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public GameObject Flower;
    public GameObject Stem;
    public GameObject LeafLeft;
    public GameObject LeafRight;

    private ObjectManager _objectManager;

	// Use this for initialization
	void Start ()
	{
	    _objectManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePlantVisuals(Plant plant)
    {
        switch (plant.Stage)
        {
            case PlantStage.Sapling:
                SetSapling(plant);
                break;
            case PlantStage.Flower:
                SetFlower(plant);
                break;
            default:
                ResetPlant();
                break;
        }
    }

    public void ResetPlant()
    {
        Flower.SetActive(false);
        Stem.SetActive(false);
        LeafLeft.SetActive(false);
        LeafRight.SetActive(false);
    }

    private void SetSapling(Plant plant)
    {
        Stem.SetActive(true);
        LeafLeft.SetActive(true);
        LeafRight.SetActive(true);

        var spriteSet = _objectManager.PlantSprites.FirstOrDefault(o => o.Type == plant.Type);

        var stemRenderer = Stem.GetComponent<SpriteRenderer>();
        stemRenderer.sprite = spriteSet.Stem;
        stemRenderer.color = plant.StemColour;

        var rightLeafRenderer = LeafRight.GetComponent<SpriteRenderer>();
        rightLeafRenderer.sprite = spriteSet.Leaf;
        rightLeafRenderer.color = plant.LeafColour;

        var leftLeafRenderer = LeafLeft.GetComponent<SpriteRenderer>();
        leftLeafRenderer.sprite = spriteSet.Leaf;
        leftLeafRenderer.color = plant.LeafColour;
    }

    private void SetFlower(Plant plant)
    {
        Flower.SetActive(true);
        Stem.SetActive(true);
        LeafLeft.SetActive(true);
        LeafRight.SetActive(true);

        var spriteSet = _objectManager.PlantSprites.FirstOrDefault(o => o.Type == plant.Type);
        var flowerRenderer = Flower.GetComponent<SpriteRenderer>();
        flowerRenderer.sprite = spriteSet.Flower;
        flowerRenderer.color = plant.FlowerColour;

        var stemRenderer = Stem.GetComponent<SpriteRenderer>();
        stemRenderer.sprite = spriteSet.Stem;
        stemRenderer.color = plant.StemColour;

        var rightLeafRenderer = LeafRight.GetComponent<SpriteRenderer>();
        rightLeafRenderer.sprite = spriteSet.Leaf;
        rightLeafRenderer.color = plant.LeafColour;

        var leftLeafRenderer = LeafLeft.GetComponent<SpriteRenderer>();
        leftLeafRenderer.sprite = spriteSet.Leaf;
        leftLeafRenderer.color = plant.LeafColour;
    }
}
