INCLUDE GlobalsMain2.ink

{ inspectedHole:
<i>This stale pack of gum is sticky, flexible... Could be just the thing for filling up a puncture.</i>
<i>Tastes really fucking bad, though.</i>
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
<i><color=\#FFA500>These items can be dragged from my inventory to the clueboard as evidence to connect to a suspect.</color></i>
-> END