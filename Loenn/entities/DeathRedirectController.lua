local controller = {}

controller.name = "GrogsGarage/Ports/DeathRedirectController"
controller.depth = 0
controller.texture = "Loenn/GrogsGarage/DeathRedirectController"
controller.placements = {
    name = "DeathRedirectController",
    data = {
        flag = "redirect_flag",
        room = "",
        priority = 0,
    }
}

return controller