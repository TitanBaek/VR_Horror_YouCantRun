using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEncounterGhost : MonoBehaviour
{
    [SerializeField] GameObject[] ghostHead;
    [SerializeField] GameObject ghostChest;
    [SerializeField] Light jumpScareLight;

    private void LateUpdate()
    {
        GhostLookAtPlayer();
        GhostHeadLookAtPlayer();
    }

    public void JumpScarePose()
    {
        jumpScareLight.enabled = true;
        ghostChest.transform.localRotation = Quaternion.Euler(ghostChest.transform.rotation.x, -65f, ghostChest.transform.rotation.z);
    }

    public void GhostLookAtPlayer()
    {
        this.transform.LookAt(Camera.main.transform);

        var origin = this.transform.rotation;
        origin.x = 0;
        origin.z = 0;

        this.transform.rotation = origin;
    }

    public void GhostHeadLookAtPlayer()
    {
        for (int i = 0; i < ghostHead.Length; i++)
        {
            ghostHead[i].transform.LookAt(Camera.main.transform);

            var origin = ghostHead[i].transform.rotation;
            origin.z = 0;

            ghostHead[i].transform.rotation = origin;
        }
    }
}
