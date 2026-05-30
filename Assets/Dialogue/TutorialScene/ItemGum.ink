INCLUDE GlobalsMain2.ink

{ inspectedHole:
<i>It's sticky, flexible, and a bit disgusting once I start chewing it.</i>
<i>Still. Might be useful for filling punctures.</i>
{ not pickedUpFirstItem:
        ~ pickedUpFirstItem = true
        -> inventory_tutorial
    }
-> END
- else:
<i>I find my pack of Extra Sweetmint gum left on the passenger seat. How long has this been sitting here? </i>
}

=== inventory_tutorial ===
<i><color=\#FFA500>I should probably keep track of the junk I keep stuffing in my pockets. If I hit [TAB], I can take a closer look at my clipboard and inspect what I've found.</color></i>
-> END