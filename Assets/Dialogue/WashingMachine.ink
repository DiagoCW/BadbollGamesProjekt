INCLUDE globals.ink
VAR foundWallet = false
{talkedToBossMan: -> FindWallet | -> Inspect }

=== Inspect ===
Just trash. 
Makes me hungry...
->END

=== FindWallet ===
{foundWallet: -> Inspect | -> FindWallet2}
->END

=== FindWallet2 ===
Wait... There's something buried beneath the trash. #speaker: Player
    + [Dig deeper]
        ...A wallet?
This is the part of the game where I find an incriminating piece of evidence and promptly go back to a suspect to confront them, even though there is nothing here yet to suggest that they would have something to do with this.
~ visitedCrimeScene = true
~ foundWallet = true
-> END