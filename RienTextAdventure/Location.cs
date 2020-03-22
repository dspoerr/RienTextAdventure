using System;
using System.Collections.Generic;


/* 
 * A subset of the overall location object, this class
 * has several functions to store decision tree choices 
 * along with accompanying text. Choices will store all
 * interactable objects and NPCs  
*/

namespace DecisionTree
{
   // container for prompts
   public struct Prompt
   {
      public int promptID;
      public String promptText;
      public int[] decisionIDs; // holds which decisions go with this promp

      public Prompt(int pID, String pText, int[] dIDs)
      {
         promptID = pID;
         promptText = pText;
         decisionIDs = dIDs;
      }

   }

   // container for decisions
   // TO DO:
   // Add a variable for "isLeavingArea" and "IsLeavingInteractable" or something
   // This way, the decision can prompt whether there should be a change of scene or area
   // without hardcoding it

   //TO DO:
   // possible add a flag to track whether the decision was checked or not. may be good
   // in the future to check conditional things like if x decision was made, script event y
   public struct Decision
   {
      public int decisionID;
      public String decisionText;
      public int leadsToPromptID; // dictates which promptID this goes to
      public int actionCount;     // how many actions does this take up for the player, default 1
      // dictates whether the decision changes the Location object, 
      // nonnegative vals dictate new location object and new prompt to go to respectively
      public int[] isLeavingArea;
      //dictates whether the decision changes the interactable, nonnegative value is the prompt it points to
      public int isLeavingInteractable;

      // most common and simplified decision, leads to a new prompt, default 1 action
      public Decision(int dID, String dText, int ltpid)
      {
         decisionID = dID;
         decisionText = dText;
         leadsToPromptID = ltpid;
         actionCount = 1; // default
         isLeavingInteractable = -1;
         isLeavingArea = new int[] { -1, -1 };
      }

      // most common and simplified decision, leads to a new prompt, nondefault actioncount
      public Decision(int dID, String dText, int ltpid, int aCount)
      {
         decisionID = dID;
         decisionText = dText;
         leadsToPromptID = ltpid;
         actionCount = aCount;
         actionCount = 1; // default
         isLeavingInteractable = -1;
         isLeavingArea = new int[] { -1, -1 };
      }

      // specifies the decision is leaving an interactable
      public Decision(int dID, String dText, int ltpid, int aCount, int leavingI)
      {
         decisionID = dID;
         decisionText = dText;
         leadsToPromptID = ltpid; //not used. TO DO: clean this up
         actionCount = aCount;
         isLeavingInteractable = leavingI;
         isLeavingArea = new int[] { -1, -1 };
      }

      // specifies the decision is leaving an area
      public Decision(int dID, String dText, int aCount, int[] leavingA)
      {
         decisionID = dID;
         decisionText = dText;
         leadsToPromptID = -1;
         actionCount = aCount;
         isLeavingArea = leavingA;
         isLeavingInteractable = -1;
      }
   }

   // container for an interactable
   public class Interactable
   {
      public int interactableID;
      String interactableName;
      public List<Decision> decisions = new List<Decision>();
      public List<Prompt> prompts = new List<Prompt>();


      public Interactable(int iID, String iName)
      {
         interactableID = iID;
         interactableName = iName;
      }
   }


   public class Choices
   {
      // global array holding all interactables
      public List<Interactable> interactables = new List<Interactable>();

      public Choices()
      {
      }


      // Adds a new interactable object to the interactables array.
      public void NewInteractable(int iID, String name)
      {
         Interactable i = new Interactable(iID, name);
         interactables.Add(i);
      }

      // most common and simplified decision, leads to a new prompt, default 1 action
      public void AddDecision(Interactable i, int dID, String dText, int ltpid)
      {
         Decision d = new Decision(dID, dText, ltpid);
         i.decisions.Add(d);
      }

      // most common and simplified decision, leads to a new prompt, nondefault actioncount
      public void AddDecision(Interactable i, int dID, String dText, int leadsTo, int aCount)
      {
         Decision d = new Decision(dID, dText, leadsTo, aCount);
         i.decisions.Add(d);
      }

      // specifies the decision is leaving an interactable
      public void AddDecision(Interactable i, int dID, String dText, int ltpid, int aCount, int leavingI)
      {
         Decision d = new Decision(dID, dText, aCount, leavingI);
         i.decisions.Add(d);
      }

      // specifies the decision is leaving an area
      public void AddDecision(Interactable i, int dID, String dText, int aCount, int[] leavingA)
      {
         Decision d = new Decision(dID, dText, aCount, leavingA);
         i.decisions.Add(d);
      }

      // assigns a prompt to the promp array of the apt interactable
      public void AddPrompt(Interactable i, int pID, String pText, int[] dIDs)
      {
         Prompt p = new Prompt(pID, pText, dIDs);
         i.prompts.Add(p);
      }
   }
}