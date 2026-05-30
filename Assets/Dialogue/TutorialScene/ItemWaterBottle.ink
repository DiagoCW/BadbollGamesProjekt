INCLUDE GlobalsMain2.ink

{ inspectedHole:
<i>The water in them is lukewarm and probably crawling with bacteria. It's not exactly coolant, but I doubt I will find anything better.</i>
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