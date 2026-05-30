INCLUDE GlobalsMain2.ink

{ inspectedHole:
<i>If I rip the tape and wrap it tightly around the hose, it could work like a bandage and keep things in place.</i>

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
-> END