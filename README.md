# combining-meshes-redurce-batches
In Unity you always have to look for ways to optimize you game operations, like rendering meshes.  If you have more individual meshes there is more time the computer has to spend on rendering. You can combine meshes sometimes to save precious time. On the MeshFilter.mesh object there is a function called CombineMeshes that combines an array of CombineInstance meshes into one mesh. This saves on the Stats Batches count.

YouTube:   https://youtu.be/zf2Vyfm4Sk8
