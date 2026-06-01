INCLUDE globalsmainstory.INK

EXTERNAL unlockBoard()

-> Start

=== Start ===
Hey! I am the clueboard debugger NPC. But you can call me Johan #speaker: Debugger
+ [Evaluate clueboard]
    Alright, hmmm...
    -> EvaluateBoard
+ [Unlock all Suspects]
    Unlocking IDs 0, 1, and 2...
    ~ unlockSuspect(0)
    ~ unlockSuspect(1)
    ~ unlockSuspect(2)
    -> Start
+ [Goodbye.]
    Ha det bäst mannen!
    -> END

=== EvaluateBoard ===
~ temp result = getAccusedSuspectID()

{ result == 5:
    <b>Result: 5</b>. The board is <b>NOT READY</b>. You don't have 3 clues attached to a suspect yet. #speaker: Debugger
    ~ unlockBoard()
    -> Start
}

{ result == 3:
    <b>Result: 3</b>. The board is <b>WRONG</b>. You attached 3 clues, but they don't match the solution. #speaker: Debugger
    ~ unlockBoard()
    -> Start
}

{ result == 0:
    <b>Result: 0</b>. SUCCESS! You correctly accused Suspect 0 (Store Clerk). #speaker: Debugger
    ~ unlockBoard()
    -> Start
}

{ result == 1:
    <b>Result: 1</b>. SUCCESS! You correctly accused Suspect 1 (Boss Man). #speaker: Debugger
    ~ unlockBoard()
    -> Start
}

{ result == 2:
    <b>Result: 2</b>. SUCCESS! You correctly accused Suspect 2 (Bartender). #speaker: Debugger
    ~ unlockBoard()
    -> Start
}

<b>Result: {result}</b>. I have no idea what that ID means. Check your script! #speaker: Debugger
~ unlockBoard()
-> Start