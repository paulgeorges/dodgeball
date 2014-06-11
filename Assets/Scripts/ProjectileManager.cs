using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileManager : MonoBehaviour
{
    public Transform projectileContainer;

    private static ProjectileManager _instance;

    public static ProjectileManager Instance
    {
        get {
            if (_instance == null) {
                GameObject projectileManagerGO = GameObject.FindGameObjectWithTag("ProjectileManager");
                if (projectileManagerGO) {
                    _instance = projectileManagerGO.GetComponent<ProjectileManager>();
                }
                else {
                    projectileManagerGO = new GameObject("ProjectileManager");
                    _instance = projectileManagerGO.AddComponent<ProjectileManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null) {
            return;
        }

        projectileContainer = transform.FindChild("ProjectileContainer");
    }
}
