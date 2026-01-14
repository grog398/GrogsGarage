local risingLava = {}

risingLava.name = "GrogsGarage/Ports/FallingLava"
risingLava.depth = 0
risingLava.texture = "Loenn/GrogsGarage/FallingLava"
risingLava.placements = {
    name = "FallingLava",
    data = {
        intro = false,
        speed = 30.0,
        rubberbandSpeed = 500,
        rubberbandAccel = 100,
        rubberbandDist = 92,
        depth = -1000000,
        surface_color = "ff8933",
        edge_color = "f25e29",
        center_color = "d01c01",
        pauseFlag = "pause_flag",
        enableRubberbanding = "enableRubberbanding"
    }
}


return risingLava