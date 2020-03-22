using System;
using DecisionTree;
using System.Collections.Generic;

/*
 * The goal of this class is to pack locations & decision trees
 * into modules. Location will remember if the place is altered
 * and store all relevant player actions. Location will parse out
 * Choices information and send it to the player.
 */


// TO DO:
// Increase functionality of the moment
// it records time, but it should have an interval. 
// Does the prompt it leads to occur only at the time, greater than, or less than, etc?
// Basically, use this as a way to script events
// i.e Ashe's turn 5 arrival at the bedroom, and it only occurs at turn 5
// but ashe's turn 4 arrive at the living area, only occurs at turn 4
// make sure to have a check in the TakeAction to check whether the next prompt should be scripted or not
public class Moment
{
   public Moment(int aNum, int iID, int pID, int dID)
   {
      int atActionNumber = aNum; // what action # the moment occured at
      int interactableID = iID;  // what interactable the decision occured with
      int promptID = pID;        // what prompt the decision occured on
      int decisionID = dID;      // what decision was chosen
   }
}

public class Location
{

   // checks if the player has made any changes to the area
   protected bool isUnaltered = true;
   protected List<Moment> history = new List<Moment>(); // all moments created
   protected int currentPrompt;
   public String locName;
   public int locID;

   public Choices options = new Choices();  // methods for storing decision information

   public Location(String name, int id)
   {
      locName = name;
      locID = id;
   }


   // Stores the current action to the location's history
   // Additionally, changes the current prompt & calls ReadPrompt
   // TO DO:
   // Add a function for searching for prompts/decisions to increase readability
   // Allow for outside factors to influence choices (such as character's rep/demeanor)
   // interactable disposition

   // TO DO:
   // possibly 

   public int[] TakeAction(int dID, int actions)
   {
      bool isFound = false;
      int aNum = actions, iID = 0, pID = 0;  // for creating the moment

      // [0] returns action number for the incrementer
      // [1] returns location ID 
      // [2] returns prompt ID
      int[] actionAndLocValue = new int[] { 0, -1, -1 };

      // Find all information associated with the decision
      foreach (Interactable i in options.interactables)
      {
         foreach (Decision d in i.decisions)
         {
            // creates the moment and sets the new prompt to go to
            if (d.decisionID == dID && d.isLeavingArea[0] == -1 && d.isLeavingInteractable == -1)
            {
               aNum = d.actionCount;
               actionAndLocValue[0] = d.actionCount;
               iID = i.interactableID;
               pID = currentPrompt;
               currentPrompt = d.leadsToPromptID;
               isFound = true;

            }
            // Not leaving area, so does not affect return variable
            else if (d.isLeavingArea[0] == -1 & d.isLeavingInteractable != -1)
            {
               aNum = d.actionCount;
               actionAndLocValue[0] = d.actionCount;
               iID = i.interactableID;
               pID = currentPrompt;
               isFound = true;
            }
            // Is leaving area, so changes the return variable to the ID of new location
            else if (d.isLeavingInteractable == -1 & d.isLeavingArea[0] != -1)
            {
               aNum = d.actionCount;
               actionAndLocValue[0] = d.actionCount;
               iID = i.interactableID;
               actionAndLocValue[1] = d.isLeavingArea[0];
               actionAndLocValue[2] = d.isLeavingArea[1];
               isFound = true;
            }
            if (isFound) { break; }
         }
         if (isFound) { break; }
      }

      Moment m = new Moment(aNum, iID, pID, dID);

      history.Add(m);
      if (isUnaltered) { isUnaltered = false; }

      return actionAndLocValue;
   } // end method TakeAction


   // uses curent prompt ID to find the right prompt to output
   // additionally parses and reads out the related decisions
   public void ReadPrompt()
   {
      String findPrompt = "Default Prompt. Please report bug! :)";   // current prompt text
      int[] findDIDs = { };                         // decision IDs
      bool isFound = false;

      foreach (Interactable i in options.interactables)
      {
         foreach (Prompt p in i.prompts)
         {
            if (p.promptID == currentPrompt)
            {
               findPrompt = p.promptText;
               findDIDs = p.decisionIDs;
               isFound = true;
               break;
            }
         }
         if (isFound) { isFound = false; break; }
      }
      Console.WriteLine(findPrompt);

      // print all decisions as such:
      // decisionID: decisionText
      // player will enter the decisionID to make their choice


      foreach (int id in findDIDs)
      {
         foreach (Interactable i in options.interactables)
         {
            foreach (Decision d in i.decisions)
            {
               if (d.decisionID == id)
               {
                  Console.WriteLine(id + ": " + d.decisionText);
                  isFound = true;
                  break;
               }
            }
            if (isFound) { isFound = false; break; }
         }
      }
   } // end method ReadPrompt

   public void setPrompt(int pID)
   {
      currentPrompt = pID;
   }

} // end class Location
