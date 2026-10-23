using System;
using UnityEngine;
using UnityEngine.UI;

public class ProjectionEffects : MonoBehaviour
{
    public LevelRotate levRot;
    public RawImage projection;
void Start()
    {
        levRot = FindFirstObjectByType<LevelRotate>().GetComponent<LevelRotate>();
    }
    void Update()
    {
        //projection.color = Color.Lerp(Color.white,Color.lightSalmon,Math.Abs(levRot.rotate));
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(60,150,Math.Abs(levRot.rotate));
    }
}
