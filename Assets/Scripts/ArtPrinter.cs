using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArtPrinter : MonoBehaviour
{
    [SerializeField] private Transform[] artSlots;
    private int artCount = 0;
    [SerializeField] private Transform artSpawnPoint;
    private GameObject artToPrint;
   
    
    public void PrintArt()
    {
        GameObject shape;
        if (artSpawnPoint.childCount != 0)
        {
            shape = artSpawnPoint.GetChild(0).gameObject;
        }
        else
        {
            shape = new GameObject("ArtPiece");
        }
        GetTrailDrawings(shape);
        shape.transform.parent = artSlots[artCount];
        //Rotera art
        //artToPrint.transform.position = Vector3.zero; //GetArtSlotPosition(); //+offset y
        //shape.transform.localScale /= 4;
        shape.transform.position = artSlots[artCount].transform.position;
        //shape.transform.localPosition = Vector3.zero;
        artCount++;
    }

    private Vector3 GetArtSlotPosition()
    {
        //float yOffset = 0.5f + pedestal.transform.localScale.y / 2; //0,5 =half original heigth
        float yOffset = artSlots[artCount].position.y - artSpawnPoint.transform.position.y;
        
        Vector3 newPos = new Vector3(artSlots[artCount].position.x,
            artSlots[artCount].position.y + yOffset, artSlots[artCount].position.z);
        
        return newPos;
    }

    
    // Get all drawings and parent
    private void GetTrailDrawings(GameObject artPiece)
    {
        GameObject[] drawingTrails = GameObject.FindGameObjectsWithTag("Trail");

        foreach (GameObject trail in drawingTrails)
        {
            TrailRenderer tr = trail.GetComponent<TrailRenderer>();
            tr.emitting = false;
            trail.gameObject.tag = "Untagged";
            trail.transform.parent = artPiece.transform;/*pedestal.transform;*/
            RepositionTrails( artSlots[artCount].transform.position/*artPiece.transform.position*/, trail, tr);
        }
    }

    private void RepositionTrails(Vector3 newPosition, GameObject trail, TrailRenderer trailRenderer)
    {
        Vector3 offset = newPosition - trail.transform.position;/*transform.position;*/
        
       // var trailRenderers = GetComponentsInChildren<TrailRenderer>();
       // foreach (var trailRenderer in trailRenderers)
        //{
        int positionCount = trailRenderer.positionCount;
        for (int i = 0; i < positionCount; i++)
            trailRenderer.SetPosition(i, trailRenderer.GetPosition(i) + offset);
        //}
        trail.transform.position = newPosition; // moving the parent of the trails
    }
}
