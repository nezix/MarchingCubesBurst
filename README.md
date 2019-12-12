# MarchingCubesBurst

Multithreaded Marching Cubes algorithm implementation using Unity C# job system, compiled by Burst compiler

I need to benchmark it but it seems really fast...

Based on CUDA samples implementation : https://github.com/tpn/cuda-samples/blob/master/v8.0/2_Graphics/marchingCubes/marchingCubes_kernel.cu

### TODO
- (1) Fix the missing triangle bug
- (2) Compute normals
- (3) Implement an exclusive scan using Unity job system, right now this is the slowest part (because done in serial) but should be the fastest

Feel free to add more...

## Contribute

Pull requests + Issues are more than welcome ! I am also curious to know how it is used :)

## License

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
