# Goals

- To make a flexible, efficient system that simulates fluid dynamics for game development purposes.
- 


# Questions

- Should a node have a maximum volumetric density? And if they do, can one fluid push out another type of fluid? Think wind pushing water, causing ripples. I believe that, if done properly, this would simulate waves and erosion. *Stone is just an incredibly dense fluid*.
- Should a node be the one who contains the different fluids, or should different maps be responsible for only one type of fluid?
- 


### Fluids

- A fluid is a data struct that tell the simulator the properties of the element. These are qualities such as:
	1. Density, which determines how a fluid is sorted in a map in relation to other fluids. [Can two fluids with the same density co-exist well? In a "parts-per-million" sort of way.]
	2. Volume, which determines how much of a fluid is store in a node, and keeps the system in balance. Viscosity should be factored in.
	3. Direction, which determines where the fluid is pushing towards.
- One fluid should push a different fluid type, according to it's mass.

### Nodes

- Nodes should support variable shapes, having three or more sides.
- A node should have a float value between 0 and 1. At 0, no fluid passes through, acting as a wall. At .75, fluid diverts partially to other neighbours. 1 is normal.
- 

### Maps

Maps are essentially "linked graphs". They contain a community of nodes, and updates them according to their data.

- The map iterates through its nodes, and performs the required operations through each node.



# Sprints

1. Wind simulation.
	A. Map, nodes and fluid.
	B. 