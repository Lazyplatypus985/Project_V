using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgardeUI : MonoBehaviour
{
    public List<ShopUpgradeScriptableObject> upgrades;
    public GameObject upgradePrefab;
    public Transform upgradeContainer;

    void Start()
    {
        foreach (var upg in upgrades)
        {
            GameObject obj = Instantiate(upgradePrefab, upgradeContainer);
            UpGradeController controller = obj.GetComponent<UpGradeController>();
            controller.data = upg;
        }
    }
}


