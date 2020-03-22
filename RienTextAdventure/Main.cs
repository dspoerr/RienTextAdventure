using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RienTextAdventure
{
   class MainLoop
   {
      // TO DO:
      // Create Setup section, parse JSON files in and assign them to objects

      // As defined in Intro, p => player
      String pName, pRace,
         pHome, pHomeRep,
         pProfession;

      // TO DO:
      // Lock screen size of console?

      // Creates the general object for each location the player will visit

      // TO DO:
      // JSON parse
      // basically, parse location, then parse interactable ID, then do the decisions.
      // make an if statement with a loop so you can pass the interactable into the function
      // should be able to quickly gen the JSON file as well
      //
      // Add an autosave feature, preventing the person from undoing what they just did
      // Maybe even catch if they do this
      // Also saves progress

      static void Main(string[] args)
      {
         // TO DO:
         // Read this value in from save file   
         int actionCounter = 0;

         // keeps track of all locations available
         List<Location> locations = new List<Location>();

         // tracks the ID of the current loc
         // TO DO:
         // read in from save file
         int currentLoc = 1;
         int newPrompt = 0;

         // temp storage
         // [0] returns action # for the counter
         // [1] returns the location ID
         int[] takeAction = new int[] { -1, -1, -1 };
         Console.WriteLine("hello");

         int[] changeLoc1 = new int[] { 2, 0 };
         int[] changeLoc2 = new int[] { 1, 0 };

         // demo setup
         Location demo = new Location("demo", 1);
         locations.Add(demo);
         demo.options.NewInteractable(0, "int1");
         demo.options.AddDecision(demo.options.interactables[0], 0, "LOOKS LIKE YOU'RE STUCK!!", 100, 1);
         demo.options.AddDecision(demo.options.interactables[0], 10, "option 1 :)", 100, 1);
         demo.options.AddDecision(demo.options.interactables[0], 11, "option 2!!", 101, 1);
         demo.options.AddDecision(demo.options.interactables[0], 12, "option 3 :O", 102, changeLoc1);



         int[] ids = { 10, 11, 12 };
         demo.options.AddPrompt(demo.options.interactables[0], 0, "THIS IS THE PROMPT >:)", ids);

         int[] idsnew = { 0, 0 };
         demo.options.AddPrompt(demo.options.interactables[0], 100, "haha you lose!", idsnew);
         demo.options.AddPrompt(demo.options.interactables[0], 101, "oops, we have stalemate", idsnew);
         demo.options.AddPrompt(demo.options.interactables[0], 103, "victoly :(", idsnew);

         // demo2 setup
         Location demo2 = new Location("demo2", 2);
         locations.Add(demo2);
         demo2.options.NewInteractable(0, "int1");
         demo2.options.AddDecision(demo2.options.interactables[0], 0, "this is the second demo", 100, 1);
         demo2.options.AddDecision(demo2.options.interactables[0], 10, "1 looks familiar)", 100, 1);
         demo2.options.AddDecision(demo2.options.interactables[0], 11, "2 birds", 101, 1);
         demo2.options.AddDecision(demo2.options.interactables[0], 12, "3 go back", 103, changeLoc2);

         demo2.options.AddPrompt(demo2.options.interactables[0], 0, "demo 2 prompt", ids);

         int[] idsnew2 = { 0, 0 };
         demo2.options.AddPrompt(demo2.options.interactables[0], 100, "waaa ", idsnew);
         demo2.options.AddPrompt(demo2.options.interactables[0], 101, "grrrr", idsnew);
         demo2.options.AddPrompt(demo2.options.interactables[0], 103, "vfdsfdsf :(", idsnew);


         String input = "";
         // Main Game Loop
         while (true)
         {

            // Intro
            // Here, people will choose their name
            // They'll also pick race, which determines how friendly people will be towards them
            // Eventually they'll choose class, but by default it will be warrior.
            // Determine home town & its reputation. If none, will be bad by default

            // Most importantly, players will choose their profession. This determines their starting point
            // and all starting characters, as well as how the world will perceive them.
            // Professions may include: merchant, mercenary, innkeeper

            // TO DO:
            // Restrict input to only available values on screen

            // loops to verify correct location
            foreach (Location l in locations)
            {
               if (l.locID != currentLoc) { continue; }
               else
               {
                  if (takeAction[1] != -1)
                  {
                     l.setPrompt(newPrompt);
                  }
                  l.ReadPrompt();
                  input = Console.ReadLine();
                  takeAction = l.TakeAction(Int32.Parse(input), actionCounter);
                  actionCounter = actionCounter + takeAction[0];

                  // if takeAction[1] != 1, decision made was a leave action
                  // directs to the new location for next loop
                  if (takeAction[1] != -1)
                  {
                     currentLoc = takeAction[1];
                     newPrompt = takeAction[2];
                  }
                  break; // location found, no need to keep going
               }
            } // End of foreach
         } // end of game loop
      } // end of main
   }
}
