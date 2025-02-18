using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu()]
public class RedBow : BowSO
{
    private void Awake()
    {
        bowType = BowType.Red;

        upgradeStats = new Dictionary<int, List<StatBonus>>
        {
            {1, new List<StatBonus>{new StatBonus(StatType.ARROWATK, 6), new StatBonus(StatType.ARROWRATE, -1)} },
            {2, new List<StatBonus>{new StatBonus(StatType.ARROWATK, 8) } },
            {3, new List<StatBonus>{new StatBonus(StatType.ARROWSPEED, 8) } },
            {4, new List<StatBonus>{new StatBonus(StatType.ARROWATK, 10), new StatBonus(StatType.ARROWRATE, -1) } }
        };
    }
}
