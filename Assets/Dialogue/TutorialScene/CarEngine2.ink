INCLUDE GlobalsMain2.ink
EXTERNAL unlockSuspect(id)

{ carIsFixed: 
    <i>I jammed the chewing gum into the puncture, wrapped it tight with the evidence tape, and topped off the reservoir with the water.</i>
    <i>Always said I was a genius. Time to hit the road.</i> 
    -> END 
}

{ isHoodOpen:
    -> EngineState
}

{not carHood:
    <i>Heat waves emanate from the hood of my car like a hot summer day in Helsingborg.</i>
    ~carHood = true
}
<i><b>Pop the hood?</b></i>
    * [Pop the hood]
        <i>The hood of the car slides open.</i>
        ~isHoodOpen = true
        -> EngineState
    * [<b>Don't</b> pop the hood]
        <i>I don't open the hood. For whatever reason. I can always come back when I feel like taking this seriously.</i>
        -> END

=== EngineState ===
{ foundCoolantLeak:
    -> InvestigateHole
}

{ checkedCoolant:
    <i>The coolant reservoir is empty. I should check the perimeter of the car.</i>
    -> END
}

<i>The engine looks like a jumbled mess of wires and metal. Some people know what this all means and how to fix it.</i>
-> EngineChoices

= EngineChoices
    * [Check spolarvätska]
        <i>I check the spolarvätska fluid. It's half full, doesn't seem to be the issue.</i>
        -> EngineChoices
    * [Check motor oil]
        <i>I pull the dipstick out, methodically wipe it on my coat sleeve, and dip it once more. A bit low, but not enough to kill the engine.</i>
        -> EngineChoices
    * [Check coolant]
        <i>I lean over the car and look at an empty reservoir. It is dry as dust. It's usually a lot more blue.</i>
        ~ checkedCoolant = true
        { seenPuddle:
        <color=\#FFA500><b>Maybe it has to do with the piss puddle I saw behind the car. I should look at it again.</b></color>
        -> END
        - else:
        <color=\#FFA500><b>If the fluid is gone, it had to leak out somewhere. I should check around the car.</b></color>
        -> END
        }
    + {EngineChoices > 1} [Leave it for now]
        -> END

=== InvestigateHole ===
{ inspectedHole:
    <i>I need to find something to plug the hole, something to bind it tight, and some fluid to replace what I lost. Time to lock in.</i>
    -> END
}

<i>I already know the coolant leaked out. Now that I've seen the puddle, I know exactly where to look.</i>
* [Investigate closer]
    <i>Running my hand along the coolant hose, my thumb catches on a massive puncture.</i>
    <i>I don't have any spare coolant or real tools with me. However, only city detectives need gimmicks, country detectives make do.</i>
    <i>I should search inside the car and around the roadside. I need to find something to plug the hole, something to wrap it with, and some liquid to replace the coolant.</i>
    ~ inspectedHole = true
    ~ unlockSuspect(4)
    -> tutorial
* [Leave]
    -> END

= tutorial
<i><color=\#FFA500>You just unlocked your first "Suspect" on the Clueboard</color></i>
<i><color=\#FFA500>Find the necessary items, open the Clueboard in the trunk, and connect the red threads from the Engine to the 3 correct items to fix the car.</color></i>
-> END