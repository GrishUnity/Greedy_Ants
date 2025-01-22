using UnityEngine;

public interface IAnt {
  public void Eats() { }
  public void Move() { }
  public void Run() { }
  public void Search() { }
  public void OnCollisionEnter(Collision collision) { }
}