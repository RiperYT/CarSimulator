# CarSimulator

This project was implemented as part of a test task for the "VRL" company.  Total time spent - 9 hours.


In this project, three assets are used:
- Roads and Environment (https://assetstore.unity.com/packages/3d/environments/roadways/low-poly-road-pack-67288)
- Vehicles (https://assetstore.unity.com/packages/3d/vehicles/land/4-low-poly-toon-cars-205608)
- Vehicle Physics Pro (https://assetstore.unity.com/packages/tools/physics/vehicle-physics-pro-community-edition-153556)


As part of the technical task, the CarInfo class was implemented, which allows you to conveniently find out the following parameters about a car that uses Vehicle Physics Pro asset for movement:
- speed
- engine revolutions
- engine status
- gear number
- gear type and distance to the nearest car, if it is in the field of view and the distance is less than 20 meters.


To demonstrate the parameters in real time, a scene was created where a track was built using Roads and Environment assets and a car from Vehicles.

Was created a IU and was created HUD script for it, which reads the parameters from CarInfo and displays them to the user.

Test task is in Test_Task.pdf.
