using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SpawnMenuAnt : MonoBehaviour {

  private AudioSource src;

  private float time_spawna = 0f;

  public List<Transform> spawnpointant;
  public static Queue<Transform> q_spawn_points_ants;

  public static int FlagStartGame;

  private void Start() {
    src = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    FlagStartGame = -1;
    Time.timeScale = 1f;
    q_spawn_points_ants = new Queue<Transform>(spawnpointant);

    Transform spawn = null;
    int randomIndex = UnityEngine.Random.Range(1, 16);
    for (int i = 0; i < randomIndex; i++) {
      spawn = q_spawn_points_ants.Dequeue();
      q_spawn_points_ants.Enqueue(spawn);
    }
  }

  void Update() {
    time_spawna -= Time.deltaTime;
    if (time_spawna <= 0) {
      if(FlagStartGame == -1) {
        GameObject bullet = PoolObjMenu.ant.GetBulletPrefab();
        IsValidObjectNull(bullet);
        time_spawna = 1f;
      }
    }

    if(FlagStartGame == 0 && ValidActivAllAnt() == 1 && src.volume == 0) {
      SceneManager.LoadScene(1);
      FlagStartGame = -1;
    }
  }

  public int ValidActivAllAnt() {
    for (int i = 0; i < PoolObjMenu.ant.pool.Count; i++) {
      if (PoolObjMenu.ant.pool[i].activeInHierarchy)
        return 0;
    }
    return 1;
  }

  private void IsValidObjectNull(GameObject bullet) {
    if (bullet != null) {
      bullet.transform.position = AntMenu.SearchPoints(q_spawn_points_ants, ref bullet.GetComponent<AntMenu>().presled);
      bullet.SetActive(true);
    }
  }
}