local controller = {}

controller.name = "GrogsGarage/DecalCullController"
controller.depth = 0
controller.texture = "Loenn/GrogsGarage/EntityCullController"
controller.placements = {
    name = "DecalCullController",
    data = {
        minDepth = -10500,
        maxDepth = 9000,
        textures = "",
        cullRadius = 400    
    }
}

controller.fieldInformation = {
    minDepth = {fieldType = "integer"},
    maxDepth = {fieldType = "integer"}
}

return controller