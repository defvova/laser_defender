using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Waveconfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool isLooping = false;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (isLooping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int index = startingWave; index < waveConfigs.Count; index++)
        {
            var currentWave = waveConfigs[index];
            yield return SpawnAllEnemiesInWave(currentWave);
        }
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
}
