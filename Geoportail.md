# Introduction #

The default URL for geoportail provides a blended map/aerial view. This can be configured in a number of ways.


# Details #

Geoportail provides a number of layers, which can be referred to as: terrain,boundaries,runways,buildings,parcels,railways,hydrography,roads,arial,sealevel and maps.

The map can be configured to include/exclude these using the query parameters.

http://maps.myosotissp.com/geoportail.html?layers=Arial:60,Maps:70,Roads:100#

Would construct a map with Arial at 60% opacity, Maps at 70% opacity, and Roads at 100% opacity.

Note that in the AR - Routes panel, its possible to turn layers on/off or adjust their opacity using the control in the top left, so you may want to include more layers there than in the ST Route panel, where you cant change it dynamically. This can be done by specifying st\_layers, and ar\_layers:

http://maps.myosotissp.com/geoportail.html?stlayers=Maps:100&arlayers=Arial:100,Maps:60,Roads#

This would give you just the Maps view at 100% opacity in the ST (right hand) panel, and a blend of Arial, Maps and Roads in the AR (left hand, or full screen) panel.