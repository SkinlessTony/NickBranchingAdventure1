using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        bool playAgain = true;

        while (playAgain)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the eternal winter wasteland of Everfrost, a merciless frozen hellscape, run by marauders and mutated creatures." +
                " You have left your underground bunker behind, and along with it the only home, family, and safety you've ever known." +
                " Like many, you've left for a mythical civilization, at the edge of the apocalypse, Paradise, a place with temperate weather, green grass and bountiful food, there you know you can be happy."
            );
            Console.WriteLine("What is your name, wanderer?");
            string playerName = Console.ReadLine() ?? "Adventurer";

            string classChoice;
            while (true)
            {
                Console.WriteLine("Choose your weapon: Melee or Firearm.");
                classChoice = Console.ReadLine()?.ToLower() ?? "";
                if (classChoice == "melee" || classChoice == "firearm")
                    break;
                Console.WriteLine("Invalid choice. You must choose either 'Melee' or 'Firearm'.");
            }

            Character player = new Character(playerName, classChoice);
            HashSet<string> visited = new HashSet<string>();

            Direction crossroads = new Direction(
                "Crossroads",
                "You've been wandering for days with no interaction with other living creatures, you start to think leaving the safety of your bunker, " +
                "yet it's far too late to turn back. " +
                "You must keep moving forward through the white haze. " +
                "Just as you were falling back into a spiral of loneliness you come upon a small abandoned neighborhood, you see two houses which may have some supplies, possibly some food that isn't just rations. " +
                "One house has a broken window, the second house is completely boarded up, or you can keep going ahead."
            );

            Direction houseWithBrokenWindow = new Direction(
                "House with broken window",
                "As you approach the shattered window you notice a scrap of blood-soaked fabric hanging from the broken window."
            );

            Direction boardedUpHouse = new Direction(
                "Boarded up house",
                "The house is sealed tight with thick wooden planks. It appears to have been undisturbed since the beginning of the frost."
            );

            Direction enterHouse = new Direction(
                "Inside the house",
                "You climb through the window. " +
                "You find yourself in the house's living room; most of it has collapsed under the weight of the snow. " +
                "The only places to go are the room you are currently in or upstairs."
            );

            Direction searchRoom = new Direction(
                "Search the room",
                "You start to search the living room, finding nothing but empty cans, a bedroll, and a lantern." +
                " Suddenly you feel a gun pointed at the back of your head." +
                "'What do you think you're doing here? This is my house.' The voice behind you says in an eerily calm demeanor."
            );

            Direction goUpstairs = new Direction(
                "Go upstairs",
                "You head up the rickety stairs, each step groaning under your weight. " +
                "At the top of the stairs, you see a man banging his head against the wall."
            );

            Direction upstairsCombat = new Direction(
                "Upstairs Encounter",
                "The man looks at you, eyes filled with madness, 'GET OUT OF MY HOUSE!' before he lunges at you."
            );
            upstairsCombat.OnEnter = () =>
            {
                Combat(player, new Monster("Frenzied Man", 25));
                if (player.Health > 0)
                    Console.WriteLine("\nThe man falls to the ground, dead, a low gurgle coming from his throat.");
            };

            Direction attackBranch1 = new Direction(
                "Follow the tendril upstairs",
                "You rush upstairs after the man, you see the body get dragged up into the barely intact attic."
            );

            Direction attackBranch2 = new Direction(
                "Leave the house quickly",
                "Deciding not to risk anything further, you exit the house back into the neighborhood."
            );

            Direction postCombatRoom = new Direction(
                "After defeating the man",
                "The hostile man lies dead on the ground, examining his body you see a long tendril coming out from his back. " +
                "The body is quickly dragged up the stairs by the tendril."
            );

            Direction upstairsAfterFight = new Direction(
                "After the Upstairs Fight",
                "The body twitches violently. A tendril erupts from his spine and drags the corpse toward the attic."
            );

            Direction leaveHouseUpstairs = new Direction(
                "Leave the house",
                "Uneasy, you decide not to risk anything further inside. You head back outside to the crossroads, leaving the house behind."
            );
            leaveHouseUpstairs.OnEnter = () => visited.Add(houseWithBrokenWindow.Name);
            leaveHouseUpstairs.AddChoice("Return to crossroads", crossroads);

            upstairsAfterFight.AddChoice("Follow the tendril upstairs", attackBranch1);
            upstairsAfterFight.AddChoice("Leave the house", leaveHouseUpstairs);

            upstairsCombat.AddChoice("Continue", upstairsAfterFight);
            goUpstairs.Choices.Clear();
            goUpstairs.AddChoice("Confront the man", upstairsCombat);

            Direction breakBoards = new Direction(
                "Breaking inside",
                "You rip the boards free and step into the silent house. " +
                "The house isn't very large, you can see most of it from the front door. " +
                "You see a door leading to the basement and some boarded-up cabinets in the kitchen."
            );

            Direction inspectKitchen = new Direction(
                "Inspect the kitchen",
                "Not finding any supplies on the counters or floors, you rip the board off of the cabinet. " +
                "Before you can take it fully off though, you hear a click, followed by an explosion.\n\nGAME OVER."
            );

            Direction inspectBasement = new Direction(
                "Go to the basement",
                "You descend the stairs into the basement below. " +
                "A furnace appears to be the only fixture in the otherwise empty room."
            );

            Direction basementTunnel = new Direction(
                "Examine Furnace",
                "As you move closer to the furnace you see scrape marks on the floorboards. " +
                "You move the furnace aside, revealing a hidden passage."
            );

            Direction hiddenJournal = new Direction(
                "Hidden Journal",
                "Under a loose floorboard near the furnace, you find a journal detailing the owner's own path to finding Paradise." +
                " This is it, a path, you can now make it to Paradise."
            );

            Direction survivorCamp = new Direction(
                "Reach Survivor Camp",
                "You follow the tunnel under the snow for what feels like days, burning through your remaining rations. " +
                "You pass out from exhaustion. Your body will never be found deep below the snow. " +
                "You wake up in a hospital. Medical staff watch over you. " +
                "'So you're finally awake,' says a man in a white coat. " +
                "'Nearly died in our tunnel system, but lucky for you we have scouts routinely surveying the tunnels.' " +
                "You notice something: the air feels temperate, you look out the window and see green grass and kids playing in a park. You've made it!\n\nENDING: Paradise."
            );

            Direction frozenRoad = new Direction(
                "Frozen Road",
                "After walking for nearly an hour, you feel the ground start to shake under you. " +
                "Suddenly you're blinded by a massive floodlight. Once your vision returns you see that it was a massive mobile war base." +
                " The gate slowly opens, a group of heavily armed men step out and surround you." +
                " 'Oi, what are ya doin out here all alone? Look like you're freezing, come on in, it's nice and warm.'"
            );

            Direction forwardOption1 = new Direction(
                "Shoot at the man",
                "You fire a shot at the man, hitting him in the shoulder. Before you can react, you hear a loud crack and feel a sharp pain in your stomach. " +
                "You collapse into the crimson-stained snow.\n\nGAME OVER."
            );

            Direction forwardOption2 = new Direction(
                "Run away",
                "Bullets tear through the snow as you sprint away from the men."
            );

            Direction hideInRuins = new Direction(
                "Hide in nearby ruins",
                "You dive into the collapsed remains of a home."
            );

            Direction keepRunning = new Direction(
                "Keep running",
                "Your legs burn as you run as fast as you can away from the men, weaving through gunfire until they decide you are no longer worth the ammunition. " +
                "You survive a few days on your rations until the cold eventually takes you.\n\nGAME OVER."
            );

            Direction enterthebase = new Direction(
                "Enter the mobile base",
                "Warm air surrounds you as the gate shuts behind you."
            );

            Direction takedrink = new Direction(
                "Take the mug and drink",
                "A warmth spreads through your body as you take a sip of the hot liquid, quickly followed by darkness." +
                " You wake up in a cage somewhere deep in the base, waiting to be sold to the highest bidder.\n\nGAME OVER."
            );

            Direction denydrink = new Direction(
                "Deny the drink",
                "'I get it, you're cautious. Makes sense, don't even know my name. My name's Hans, I'm with the Helsing group. this is our main base of operations.' " +
                "'Now c'mon, drink, warm your bones, you're practically on the verge of the bite.'"
            );

            Direction denydrinkagain = new Direction(
                "Deny the drink again",
                "After denying the drink, you feel the butt of a gun bash you in the back of the head. " +
                "Your vision blacks out and you awaken trapped in a cage awaiting to be sold to the elites of the wasteland.\n\nGAME OVER."
            );

            Direction Takedrinkagain = new Direction(
                "Take the drink and sip",
                "A warmth spreads through your body as you take a sip of the hot liquid, quickly followed by darkness. " +
                "You wake up in a cage somewhere deep in the base, waiting to be sold to the highest bidder.\n\nGAME OVER."
            );

            Direction atticCombat = new Direction(
     "Attic Encounter",
     "You follow the tendril into the attic. When you reach the top of the unstable staircase, you're horrified to see the body assimilated into a writhing mass of flesh hanging from the roof."
 );

            Direction joinTheMassAttic = new Direction(
                "Join the Mass",
                "You approach the writhing mass and let it envelop you. As your flesh is assimilated, you feel at peace. This is your personal paradise.\n\nENDING: Joined the Mass."
            );

            Direction fleeFromAtticFinal = new Direction(
                "Flee the Attic",
                "You turn to run, but the mass grabs you using one of its tendrils, keeping you held in place. The attic collapses onto you, crushing you under debris and snow.\n\nGAME OVER."
            );

            Direction shootMassAttic = new Direction(
                "Shoot the Mass",
                "You fire at the mass, but it barely reacts. " +
                "Suddenly the mass sends the attic roof crashing down on you.\n\nGAME OVER."
            );

            atticCombat.AddChoice("Approach the mass", joinTheMassAttic);
            atticCombat.AddChoice("Flee", fleeFromAtticFinal);
            if (player.Class == "firearm")
                atticCombat.AddChoice("Shoot the mass", shootMassAttic);

            attackBranch1.AddChoice("Follow the tendril into the attic", atticCombat);

            crossroads.AddChoice("Go to house with broken window", houseWithBrokenWindow);
            crossroads.AddChoice("Go to boarded up house", boardedUpHouse);
            crossroads.AddChoice("Walk forward on frozen road", frozenRoad);

            houseWithBrokenWindow.AddChoice("Climb through the window", enterHouse);
            houseWithBrokenWindow.AddChoice("Walk away", crossroads);

            enterHouse.AddChoice("Search the room", searchRoom);
            enterHouse.AddChoice("Go upstairs", goUpstairs);
            enterHouse.AddChoice("Leave the house", crossroads);

            Direction failEnteringHouse = new Direction("Locked Out", "You'd need a melee weapon to open these boards.");
            failEnteringHouse.AddChoice("Leave house", crossroads);

            if (player.Class == "melee")
                boardedUpHouse.AddChoice("Enter the house", breakBoards);
            else
                boardedUpHouse.AddChoice("Enter the house", failEnteringHouse);
            boardedUpHouse.AddChoice("Leave it alone", crossroads);

            breakBoards.AddChoice("Inspect the kitchen", inspectKitchen);
            breakBoards.AddChoice("Go to the basement", inspectBasement);
            breakBoards.AddChoice("Leave the house", crossroads);

            inspectBasement.AddChoice("Search behind the furnace", basementTunnel);
            inspectBasement.AddChoice("Search the basement thoroughly", hiddenJournal);
            basementTunnel.AddChoice("Follow the tunnel", survivorCamp);

            Direction safeRoute = new Direction(
                "Journey to find Paradise",
                "You carefully follow the journal's instructions, avoiding the traps. " +
                "Eventually, you emerge to sunlight and warmth. You've found Paradise!\n\nENDING: Fruitless Journey."
            );

            hiddenJournal.AddChoice("Journey to find Paradise", safeRoute);

            Direction leaveruins = new Direction(
                "Leave the ruins",
                "You stand up and exit the collapsed house.\n\nGAME OVER."
            );

            Direction hiddenShelter = new Direction(
                "Stay hidden",
                "You hide in the ruins of the collapsed house, you end up finding a hidden shelter under the rubble. " +
                "Hiding in the shelter you found you are able to stay hidden from the men searching for you" +
                " When you stop hearing footsteps and shouting you leave the ruins and continue on your lonesome road" +
                " You are no better or worse off than you were when you started your journey.\n\nENDING: Nothing changed."
            );

            hideInRuins.AddChoice("leave the ruins", leaveruins);
            hideInRuins.AddChoice("Stay Hidden", hiddenShelter);

            frozenRoad.AddChoice("Shoot at the man", forwardOption1);
            frozenRoad.AddChoice("Run away", forwardOption2);
            forwardOption2.AddChoice("Hide in nearby ruins", hideInRuins);
            forwardOption2.AddChoice("Keep running", keepRunning);
            frozenRoad.AddChoice("Enter the base", enterthebase);
            enterthebase.AddChoice("Take the mug and drink", takedrink);
            enterthebase.AddChoice("Deny the drink", denydrink);
            denydrink.AddChoice("Deny again", denydrinkagain);
            denydrink.AddChoice("Take the drink", Takedrinkagain);

            Direction current = crossroads;
            bool reachedEnd = false;

            while (!reachedEnd)
            {
                Console.Clear();
                Console.WriteLine($"\n--- {current.Name} ---");
                Console.WriteLine(current.Description);
                Console.WriteLine();

                current.OnEnter?.Invoke();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();

                visited.Add(current.Name);

                if (current.Description.Contains("GAME OVER") || current.Description.Contains("ENDING:") || current.Choices.Count == 0)
                {
                    Console.WriteLine("\nYour journey ends here.");
                    reachedEnd = true;
                    break;
                }

                Console.WriteLine("Choose an option:");
                List<KeyValuePair<string, Direction>> availableChoices = new List<KeyValuePair<string, Direction>>();
                foreach (var choice in current.Choices)
                    if (!(current == crossroads && visited.Contains(choice.Value.Name)))
                        availableChoices.Add(choice);

                for (int i = 0; i < availableChoices.Count; i++)
                    Console.WriteLine($"{i + 1}. {availableChoices[i].Key}");

                int choiceIndex = 0;
                while (true)
                {
                    Console.Write("\nEnter the number of your choice: ");
                    if (int.TryParse(Console.ReadLine(), out choiceIndex) && choiceIndex > 0 && choiceIndex <= availableChoices.Count)
                        break;
                    Console.WriteLine("Invalid choice, try again.");
                }

                current = availableChoices[choiceIndex - 1].Value;
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();

                player.DisplayStoryBasedOnHealth();
            }

            Console.WriteLine("\nDo you want to restart the game? (y/n)");
            string restartChoice = Console.ReadLine()?.ToLower() ?? "n";
            playAgain = restartChoice == "y";
        }

        Console.WriteLine("\nThank you for playing. Farewell, wanderer.");
        Console.ReadLine();
    }

    static void Combat(Character player, Monster monster)
    {
        Random rng = new Random();
        while (player.Health > 0 && monster.Health > 0)
        {
            Console.WriteLine($"\n{monster.Name} Health: {monster.Health}");
            Console.WriteLine($"{player.Name} Health: {player.Health}");
            Console.WriteLine(player.Class == "melee" ? "Choose: 1 to attack" : "Choose: 1 to shoot");

            string choice = Console.ReadLine();
            if (player.Class == "melee" && choice == "1")
            {
                int damage = rng.Next(3, 7);
                monster.Health -= damage;
                Console.WriteLine($"You strike for {damage} damage.");
            }
            else if (player.Class == "firearm" && choice == "1")
            {
                int damage = rng.Next(4, 9);
                monster.Health -= damage;
                Console.WriteLine($"You fire for {damage} damage.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                continue;
            }

            if (monster.Health > 0)
            {
                int monsterDamage = rng.Next(2, 6);
                player.Health -= monsterDamage;
                Console.WriteLine($"{monster.Name} hits you for {monsterDamage} damage!");
            }
        }
    }
}
