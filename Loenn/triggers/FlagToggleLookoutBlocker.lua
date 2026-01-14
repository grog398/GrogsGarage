local trigger = {}

trigger.name = "GrogsGarage/Ports/FlagToggleLookoutBlocker"

trigger.placements = {
    name = "FlagToggleLookoutBlocker",
    data = {
        flag = "lookout_flag",
        invert = false
    }
}

return trigger