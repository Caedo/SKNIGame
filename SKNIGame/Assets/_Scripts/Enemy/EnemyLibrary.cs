using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyLibrary : ScriptableObject {

    public List<ElementItem> enemySortedByElementList;


    //return random enemy from random element list in max cost range, if it's not possible return null
    public EnemyController GetEnemyInCostRange(int maxEnemyCost, out int cost)
    {
        ElementItem element = enemySortedByElementList[Random.Range(0, enemySortedByElementList.Count-1)];
        int searchTime = 0;
        cost = 0;
        while (searchTime < 20)
        {
            ElementItem.EnemyItem e = element.enemyList[Random.Range(0, element.enemyList.Count - 1)];
            if (e.cost <= maxEnemyCost)
            {
                cost = e.cost;
                return e.enemyPrefab;
            }
        }
        return null;
    }

}

[System.Serializable]
public class ElementItem
{
    public string name;
    //public Element element;  //not used, to get weakness of enemy element, for better balance of spwned teams
    public List<EnemyItem> enemyList;


    [System.Serializable]
    public class EnemyItem
    {
        public EnemyController enemyPrefab;
        [Range(1, 20)]
        public int cost;
    }
}