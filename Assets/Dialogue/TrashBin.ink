INCLUDE globals.ink

{talkedToBossMan: -> Inspect | -> InspectAgain }

=== Inspect ===
{visitedCrimeScene: -> InspectAgain | -> FindRoll }
=== InspectAgain ===
Good ol trash... # speaker: Player
-> END

=== FindRoll === 
This roll... it's still fresh. # speaker: Player
~ visitedCrimeScene = true
-> END