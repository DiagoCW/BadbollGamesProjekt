INCLUDE globals.ink

{talkedToBossMan: -> Inspect | -> InspectAgain }

=== Inspect ===
{foundSnusdosa: -> InspectAgain | -> FindSnusdosa }
=== InspectAgain ===
Good ol trash... # speaker: Player
-> END

=== FindSnusdosa === 
This roll... it's still fresh. # speaker: Player
~ foundSnusdosa = true
-> END