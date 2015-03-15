# Introduction #

The "AR - Apply Route..." menu allows you to add (or replace) the route on an existing activity (or set of activities - this can be useful for updating old logbooks from your pre-gps-owning days).

# Details #

First you need a Route to apply. You can create your own from scratch, or use [AR - Create Routes From Activities...](CreateRoutes.md) to create one from an existing activity.

Next, select your activity, and go to "Edit Activity -> AR Apply Route...".

If the activity already has GPS data, you will need to uncheck "Ignore Activities with GPS data" to proceed (to prevent accidently overwriting activities with GPS data).

Choose the route to apply, and any options (see below), then hit OK.

  * By default, ApplyRoutes will use the pace from the route, scaled by the time of the activity.
  * "Apply times linearly" will make it apply a constant pace.
  * "Preserve Distances" will use distance/pace from splits, or from a distance track (if present) to set the pace on the activity.
    * "Scaled" means scale the activity's distance to match the route
    * "Rounded" means scale the activity's distance to match an exact number of laps of the route
    * "Exactly" means that the route will be repeated until the distance is used up (even if thats part-way round a lap)