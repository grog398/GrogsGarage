local controller = {}

controller.name = "GrogsGarage/Ports/DateCounterController"
controller.depth = 0
controller.texture = "Loenn/GrogsGarage/DateCounterController"
controller.placements = {
    name = "DateCounterController",
    data = {
        month = 1;
        day = 1;
        dayCounter = "",
        hourCounter = "",
        minuteCounter = "",
        secondCounter = "",
    }
}

controller.fieldInformation = {
    month = {
        minimumValue = 1,
        maximumValue = 12,    
    },
    day = {
        minimumValue = 1,
        maximumValue = 31,    
    }    
}

return controller