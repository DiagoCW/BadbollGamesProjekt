INCLUDE GlobalsMain2.ink
EXTERNAL unlockSuspect(id)
EXTERNAL isSolutionCorrect()
EXTERNAL unlockBoard()
EXTERNAL isBoardLocked()
EXTERNAL changeTypingSpeed(float)

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
        ~isHoodOpen = true
        ~ playAudio("car-hood-open")
        <i>The hood of the car slides open.</i>
        -> EngineState
    * [<b>Don't</b> pop the hood]
        <i>I don't open the hood. For whatever reason. I can always come back when I feel like taking this seriously.</i>
        -> END

=== EngineState ===
{ foundCoolantLeak:
    -> InvestigateHole
}

{ checkedCoolant:
    <i>The coolant reservoir is empty. I should check around the back of the car to see if I can identify the problem.</i>
    -> END
}

<i>The engine looks like a jumbled mess of wires and metal. Some people know what this all means and how to fix it. I'll just have to poke around and hope for the best...</i>
-> EngineChoices

= EngineChoices
    * [Check spolarvätska]
        <i>I check the spolarvätska fluid. It's half full, but that shouldn't be an issue.</i>
        -> EngineChoices
    * [Check motor oil]
        <i>I pull the dipstick out, methodically wipe it on my coat sleeve, and dip it once more. A bit low, but not enough to kill the engine.</i>
        -> EngineChoices
    * [Check coolant]
        <i>I lean over the car and look at an empty reservoir. It is dry as dust. It's usually a lot more blue.</i>
        ~ checkedCoolant = true
        { seenPuddle:
        <color=\#FFA500><b>Maybe it has to do with the piss puddle I saw behind the car. I should go back and took a look at it.</b></color>
        -> END
        - else:
        <color=\#FFA500><i>That's not good, that means it had to leak out somehow. I should go check near the back of the car for a leak.</i></color>
        -> END
        }
    + {EngineChoices > 1} [Leave it for now]
        -> END

=== InvestigateHole ===
{ inspectedHole:
    <i>I need to find something to <color=\#FFFF00>plug the hole</color>, something to <color=\#FFFF00>bind it tight</color>, and some <color=\#FFFF00>fluid</color> to replace what I lost. Time to lock in...</i>
    <i>Once I've found all the items, I should go to the clueboard in my trunk and take it from there.</i>
    -> END
}

<i>Now that I've seen the puddle to confirm the coolant leak, I know exactly where to look.</i>
* [<color=\#FFFF00><b>Investigate closer</b></color>]
    <i>Running my hand along the coolant hose, my thumb catches on a massive puncture.</i>
    <i>I don't have any spare coolant or real tools with me. However, only city detectives need gimmicks, <b>country detectives make do.</b></i>
    <i>I should search inside the car and around the roadside. I need to find something to <color=\#FFFF00>plug the hole</color>, something to <color=\#FFFF00>wrap it with</color>, and some <color=\#FFFF00>liquid</color> to replace the coolant.</i>
    <i>Once I've found all the items, I should go to the clueboard in my trunk and take it from there.</i>
    ~ inspectedHole = true
    ~ unlockSuspect(4)
    -> tutorial
* [Leave]
    -> END

= tutorial
<i><color=\#FFA500>That should be enough information to build a profile. Whenever I piece together a major lead or obtain key evidence, a new Suspect may unlock on my Clueboard.</color></i>
<i><color=\#FFA500>My first suspect? This busted engine. Well, in a sense at least. I just need to find three items to patch the leak, head over to the trunk, and then connect the suspect to those items.</color></i>
<i><color=\#FFA500>It would make more sense if I had a real suspect to work with instead of this busted engine, but I don't really work many actual cases these days...</color></i>
-> END

=== EvaluateSolution ===
{~<i>Like my grandma used to say; it's all shits and giggles 'til someone giggles and shits. Let's see if this works...</i>| <i>I am the best god damn detective on Earth. That's why God chose me, and he gave me divine intellect. This better work...</i> | <i>Please work please work please work please work please work please work please work...</i> }


~ temp correct = isSolutionCorrect()

{ correct:
    <i>Well, I'll be damned. It actually works.</i>
    <i>Gum in the hole, evidence tape wrapped tight, and the reservoir topped off with stale water.</i>
    ~ isHoodOpen = false
    ~ playAudio("car-hood-open")
    <i>I'm so smart and cool. Time to hit the road.</i> 
    ~ carIsFixed = true
    ~ boardSubmitted = false
    ~ introCompleted = true
-> END 

- else:
    ~ failCount = failCount + 1
    
    { failCount == 1:
        <i>I'm such a fraud... This isn't working at all!</i>
        <i>I need to go back to my clueboard and rethink those connections.</i>
        <i><color=\#FFA500>Lucky for me, this is just a busted engine. I can rethink this mess and try again. At a real crime scene, I'd only get one shot at the clueboard.</color></i>
        <i><color=\#FFA500>I should also rethink whether the evidence I submitted is missing something. Just because there's 3 slots on the board doesn't mean that there's only 3 pieces of evidence to find.</color></i>
    }
    
    { failCount == 2:
        <i>This is getting embarrassing... This is not reflective for a detective of my stature. Perhaps there's something I'm missing...?</i>
        <i><color=\#FFA500>Have I really found everything that I need to solve this? Or is there something missing somehow? If need be, I can always take another look around the scene.</color></i>
    }
    
    { failCount == 3:
        ~ changeTypingSpeed(0.08)
        Hey, you. Behind the computer screen.
        ~ changeTypingSpeed(0.02)
        C'mon. We don't know how we can make it any more obvious what you have to do at this point. Perhaps you should consider a career in game journalism?
        Since you have somewhere you need to get to, we'll just tell you outright: <color=\#FFA500><b>Stop trying to make the Motor Oil work. It's not going to work.</b></color>
        C'mon, one more try detective. You got this. <i>We believe in you.</i>
    }
    
    { failCount >= 4:
        You're actually just fucking with us at this point, right? Because it's funny or something?
        News flash, pal. Nothing about this game is supposed to be funny. If you construe even a single thing in this game as being funny, then we've failed at what we set out to do. Detective games aren't supposed to be funny.
        ~ isHoodOpen = false
        ~ playAudio("car-hood-open")
        There, the engine is fixed now! You're the greatest detective in the world, probably. Time to get going!
        ~ carIsFixed = true
        ~ boardSubmitted = false
        ~ introCompleted = true
    }

    ~ unlockBoard()
    -> END
}

~ boardSubmitted = false
~ unlockBoard()
-> END
}