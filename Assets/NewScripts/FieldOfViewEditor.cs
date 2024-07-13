using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
   private void OnSceneGUI()
   {
      FieldOfView fov = (FieldOfView)target;
      Handles.color = Color.green;
      float thickness = 2.0f;
      
      Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius, thickness);
      Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
      Vector3 viewAngeB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
      Handles.color = Color.blue;
      Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
      Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngeB * fov.viewRadius);

      Handles.color = Color.red;
      foreach (var visibleTarget in fov.VisibleTarget)
      {
         Handles.DrawLine (fov.transform.position, visibleTarget.position, thickness);
      }
   }
}
