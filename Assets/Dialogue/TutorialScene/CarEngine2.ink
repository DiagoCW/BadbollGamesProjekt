INCLUDE GlobalsMain2.ink
EXTERNAL unlockSuspect(id)
EXTERNAL isSolutionCorrect()
EXTERNAL unlockBoard()
EXTERNAL isBoardLocked()

EXTERNAL playAudio(string)
EXTERNAL playAmbience(string)
EXTERNAL stopAmbience(string)
EXTERNAL stopAllAmbience()
EXTERNAL lowerPitch(string)

{ carIsFixed: 
<i>Man, this car fucking sucks. I should totally get a Chinese EV...</i>
    -> END 
}

{ isBoardLocked():
-> EvaluateSolution
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
        ~ playAudio("car-hood-open")
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
    <i>Once I have enough items to connect to the car engine, I should go to the clueboard in my trunk and drag the items over from my inventory.</i>
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
<i><color=\#FFA500>That's enough information to build a profile. Whenever I piece together a major lead or inspect key evidence, a new "Suspect" will unlock on my Clueboard.</color></i>
<i><color=\#FFA500>My first suspect? This busted engine. Now, I need to find 3 items to patch the leak, open the trunk, and tie them to the Engine with red string.</color></i>
-> END

=== EvaluateSolution ===
<i>Like my grandma used to say: It's all shits and giggles 'til someone giggles and shits. Let's see if my solution holds up.</i>

~ temp correct = isSolutionCorrect()

{ correct:
    <i>Well, I'll be damned. It actually works.</i>
    <i>Gum in the hole, evidence tape wrapped tight, and the reservoir topped off with stale water.</i>
    ~ isHoodOpen = false
    <i>...I'm so smart and cool. Time to hit the road.</i> 
    ~ carIsFixed = true
    ~ boardSubmitted = false
    ~ introCompleted = true
-> END 

- else:
    ~ failCount = failCount + 1
    
    { failCount == 1:
        <i>I'm such a fraud. This isn't working at all!</i>
        <i>I need to go back to my clueboard and rethink those connections.</i>
        <i><color=\#FFA500>Lucky for me, this is just a busted engine. I can rethink this mess and try again. At a real crime scene, I'd only get one shot at the clueboard.</color></i>
    }
    
    { failCount == 2:
        <i>This is getting embarrassing. Let's try another combination.</i>
    }
    
    { failCount >= 3:
        <i>Hey you behind the computer screen!</i>
        <i>Genuine question. Did your mom drop you as a child?</i>
    }

    ~ unlockBoard()
    -> END
}

~ boardSubmitted = false
~ unlockBoard()
-> END
}