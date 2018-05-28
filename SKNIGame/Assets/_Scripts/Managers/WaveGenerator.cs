using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaveGenerator {

    //generates new wave data used by spawner
    public static WaveData GenerateWaveData(EnemyLibrary enemyLibrary, int waveStrength, int numOfSubWave) {
        WaveData wave = new WaveData();

        //create new sub waves numOfSubWaves times, all have strength equal waveStrength divided by numOfSubWave
        for(int i = 0; i < numOfSubWave; i++)
        {
            SubWave sub = new SubWave();
            sub.AddEnemyList(GenerateEnemyList(enemyLibrary, waveStrength / numOfSubWave));
            wave.m_SubWaves.Add(sub);
            Debug.Log("Create " + i + " wave");
        }
        return wave;
    }

    private static List<EnemyController> GenerateEnemyList(EnemyLibrary enemyLibrary, int subWaveStrength)
    {
        List<EnemyController> enemyList = new List<EnemyController>();
        while (subWaveStrength > 1) //can be changed to zero, one add margin of error
        {
            int cost;
            EnemyController enemy = enemyLibrary.GetEnemyInCostRange(subWaveStrength, out cost);
            if (enemy != null)
            {
                subWaveStrength -= cost; 
                enemyList.Add(enemy);
            }
            if(cost <= 0) //second infinity loop
                break;
        }
        return enemyList;
    }
}
