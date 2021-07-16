using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;

    Enemy[] _enemies;

    void OnEnable()
    {
      _enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
      if (MonstersAreAllDead())
      {
        StartCoroutine(GoToNextLevel());
      }
    }

    IEnumerator  GoToNextLevel()
    {
      yield return new WaitForSeconds(3.0f);
      SceneManager.LoadScene(_nextLevelName);
    }

    bool MonstersAreAllDead()
    {
      foreach (var enemy in _enemies)
      {
        if (enemy.gameObject.activeSelf) {
          return false;
        }
      }
      return true;
    }
}
