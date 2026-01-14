local controller = {}

controller.name = "GrogsGarage/Ports/FlagPlayerCollideController"
controller.depth = 0
controller.texture = "Loenn/GrogsGarage/FlagPlayerCollideController"
controller.placements = {
    name = "FlagWhenPlayerCollideController",
    data = {
        flag = "CollideFlag",
        invertFlag = false,
        types = ""
    }
}

return controller