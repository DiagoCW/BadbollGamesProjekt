INCLUDE GlobalsMain2.ink

{ inspectedHole:
<i>It keeps the engine nice and lubricated. But pouring this sludge into my cooling system...?</i>

{ not pickedUpFirstItem:
    ~ pickedUpFirstItem = true
    -> inventory_tutorial
    }
-> END
- else:
<i>A jug of motor oil I keep in the backseat for emergencies.</i>
-> END
}

=== inventory_tutorial ===
<i><color=\#FFA500>I should probably keep track of the junk I keep stuffing in my pockets. If I hit [TAB], I can take a closer look at my clipboard and inspect what I've found.</color></i>
-> END