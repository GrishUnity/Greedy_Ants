using UnityEngine;
using System.Collections.Generic;
public class AntMenu : MonoBehaviour{

  public float speed = 7;
  public GameObject presled;

  public void Update() {
    if(gameObject.activeInHierarchy && gameObject.GetComponent<Rigidbody>().velocity.y < -10) {
      gameObject.SetActive(false);
    }
    if(SpawnMenuAnt.FlagStartGame == 0) {
      speed = 30;
      gameObject.GetComponent<Animator>().speed = 2;
    }
    if (gameObject.activeInHierarchy) {
      Run();
    }      
  }

  public static Vector3 SearchPoints(Queue<Transform> q_spawn_points_ants, ref GameObject presled) {
    Transform spawn = null;
    spawn = q_spawn_points_ants.Dequeue();
    q_spawn_points_ants.Enqueue(spawn);
    presled = q_spawn_points_ants.Peek().gameObject;
    return spawn.position;
  }

  public void Run() {
    if (presled != null) {
      gameObject.transform.SetPositionAndRotation(Vector3.MoveTowards(gameObject.transform.position, presled.transform.position, speed * Time.deltaTime),
        Quaternion.LookRotation(gameObject.transform.position - presled.transform.position));
    }
  }
  public virtual void OnTriggerEnter(Collider collision) {
    if(presled == collision.gameObject) {
      gameObject.SetActive(false);
      if(SpawnMenuAnt.FlagStartGame == 0) {
        gameObject.GetComponent<Animator>().speed = 1.5f;
      }
    }
  }
}