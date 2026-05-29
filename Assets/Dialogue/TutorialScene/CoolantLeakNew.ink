INCLUDE GlobalsMain2.ink

{ inspectedHole: -> END } 

{ checkedCoolant: 
    <i>Wait, actually...</i> this isn't just any piss... <color=\#FFA500>It's my car's piss!</color></i>
    <i>The leak is coming from right below the front. I need to go back to the engine and investigate that coolant hose closer.</i>
    ~ foundCoolantLeak = true
    -> END
- else:
    <i>Looks like someone took a piss here. Right underneath my car. For shame.</i>
    -> END
}