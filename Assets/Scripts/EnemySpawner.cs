using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Waveconfig> waveConfigs;
    int startingWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnAllEnemiesInWave(CurrentWave()));
    }

    private IEnumerator SpawnAllEnemiesInWave(Waveconfig waveConfig)
    {
        for(int index = 0; index < waveConfig.GetNumberOfEnemies(); index++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);

            newEnemy.GetComponent<Enemy>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private Waveconfig CurrentWave()
    {
        return waveConfigs[startingWave];
    }
}
