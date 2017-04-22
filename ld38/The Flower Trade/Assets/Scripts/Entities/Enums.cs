namespace Enums
{
    #region Plant Stuff

    public enum PlantRarity
    {
        VeryCommon = 0,
        Common = 1,
        Rare = 2,
        VeryRare = 3,
        Legendary = 4
    }

    public enum PlantStage
    {
        Seed = 0, //Plantable
        Sapling = 1, //Growing
        Flower = 2 //Pickable
    }

    //TODO: Update these to be actual names.
    public enum PlantType
    {
        Type0 = 0,
        Type1 = 1,
        Type2 = 2,
        Type3 = 3,
        Type4 = 4,
        Type5 = 5
    }
    #endregion

    #region Land Stuff

    public enum LandStage
    {
        Prepable = 0,
        Prepped = 1,
        Planted = 2,
        Collectable = 3,
        Unusable = 99
    }

    #endregion

    #region Selection Stuff

    public enum Selected
    {
        Nothing = 0,
        Ground = 1,
        Shop = 2
    }
    #endregion
}

