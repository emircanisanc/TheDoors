using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : UseableItemBase
{
    [SerializeField] GameObject _light;

    public override void SetActive()
    {
        if (itemData.CurrentFuel <= 0)
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

        if (itemData.CurrentFuel > 0)
        {
            if (!_light.activeSelf)
                _light.SetActive(true);

            float amountToReduce = Time.deltaTime * itemData.itemSO.fuelSpendMultiplier;
            itemData.CurrentFuel = Mathf.Max(0, itemData.CurrentFuel - amountToReduce);
        }
        else
        {
            if (_light.activeSelf)
                SetDeactive();

        }
    }
    
}
