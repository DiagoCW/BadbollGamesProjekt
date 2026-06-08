INCLUDE GlobalsMain2.ink

{ inspectedHole:
<i>If I rip this tape and wrap it tightly around the hose, it could work like a bandage and keep things in place.</i>
<i>I don't think anyone will mind that I took it, doesn't seem to be important.</i>
{ not pickedUpFirstItem:
        ~ pickedUpFirstItem = true
        -> inventory_tutorial
    }
-> END
- else:
-> END
}

=== inventory_tutorial ===
<i><color=\#FFA500>I should probably keep track of the junk I keep stuffing in my pockets. If I hit [TAB], I can take a closer look at my clipboard and inspect what I've found.</color></i>
<i><color=\#FFA500>These items can be dragged from my inventory to the clueboard as evidence to connect to a suspect.</color></i>
-> END