// Creates the CREATE DUNGEON and CLEAR LEVEL buttons for classes inheriting from AbstractDungeonGenerator
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class DungeonEditor : Editor
{
   private AbstractDungeonGenerator _generator;

   private void Awake()
   {
      _generator = (AbstractDungeonGenerator)target;
   }

   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();
      if (GUILayout.Button("Create Dungeon"))
      {
         _generator.GenerateDungeon();
      }

      if (GUILayout.Button("Clear level"))
      {
         _generator.ClearDungeon();
      }
   }
}
