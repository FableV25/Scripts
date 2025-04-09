using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTracker : singleton<enemyTracker>
{
    private HashSet<string> defeatedEnemies = new HashSet<string>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
    }

    public void MarkEnemyAsDefeated(string enemyID)
    {
        defeatedEnemies.Add(enemyID);
    }

    public bool IsEnemyDefeated(string enemyID)
    {
        return defeatedEnemies.Contains(enemyID);
    }
}

