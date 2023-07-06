using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : UseableItemBase
{
    [SerializeField] GameObject _light;
    [SerializeField] float fuelSpendMultiplier = 1;

    public override void SetActive()
    {
        if (itemData.currentFuel <= 0)
            return;
        
        isActive = true;
        _light.SetActive(true);
    }

    public override void SetDeactive()
    {
        base.SetDeactive();
        _light.SetActive(false);
    }

    protected override void Update()
    {
        if (!isActive)
            return;

        if (itemData.currentFuel > 0)
        {
            if (!_light.activeSelf)
                _light.SetActive(true);

            float amountToReduce = Time.deltaTime * fuelSpendMultiplier;
            itemData.currentFuel = Mathf.Max(0, itemData.currentFuel - amountToReduce);
            itemData.OnItemFuelChanged?.Invoke(itemData.currentFuel);
        }
        else
        {
            if (_light.activeSelf)
                SetDeactive();

        }
    }
    
}
