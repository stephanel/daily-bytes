``` ini
BenchmarkDotNet v0.13.12, Windows 10 (10.0.18363.1556/1909/November2019Update/19H2)
Intel Core i7-4750HQ CPU 2.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX2 [AttachedDebugger]
  DefaultJob : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX2
```


| Method                 | collectionSize | collectionType | Mean            | Error         | StdDev        | Median          | Gen0    | Gen1    | Gen2    | Allocated |
|----------------------- |--------------- |--------------- |----------------:|--------------:|--------------:|----------------:|--------:|--------:|--------:|----------:|
| 'Index based for loop' | 10             | Array          |        72.81 ns |      1.448 ns |      3.024 ns |        71.86 ns |       - |       - |       - |         - |
| 'Foreach loop'         | 10             | Array          |        33.59 ns |      0.701 ns |      1.720 ns |        33.40 ns |  0.0005 |       - |       - |      32 B |
| 'Linq ForEach'         | 10             | Array          |        81.71 ns |      2.218 ns |      6.292 ns |        80.05 ns |  0.0020 |       - |       - |     136 B |
| While                  | 10             | Array          |        72.08 ns |      1.487 ns |      2.132 ns |        71.56 ns |       - |       - |       - |         - |
| Do/While               | 10             | Array          |        71.71 ns |      1.015 ns |      0.950 ns |        71.48 ns |       - |       - |       - |         - |
| Parallel.ForEach       | 10             | Array          |    14,905.73 ns |    686.183 ns |  1,979.795 ns |    14,034.57 ns |  0.0305 |       - |       - |    2206 B |
| 'Linq as parallel'     | 10             | Array          |    47,291.96 ns |    939.678 ns |  1,764.943 ns |    46,951.54 ns |  0.1221 |       - |       - |    4536 B |
| 'Index based for loop' | 10             | List           |        16.88 ns |      0.383 ns |      0.484 ns |        16.81 ns |       - |       - |       - |         - |
| 'Foreach loop'         | 10             | List           |        50.02 ns |      0.980 ns |      1.203 ns |        49.92 ns |  0.0006 |       - |       - |      40 B |
| 'Linq ForEach'         | 10             | List           |        65.05 ns |      1.803 ns |      5.287 ns |        63.75 ns |  0.0020 |       - |       - |     136 B |
| While                  | 10             | List           |        17.78 ns |      0.394 ns |      0.589 ns |        17.60 ns |       - |       - |       - |         - |
| Do/While               | 10             | List           |        17.61 ns |      0.396 ns |      0.406 ns |        17.45 ns |       - |       - |       - |         - |
| Parallel.ForEach       | 10             | List           |    13,148.59 ns |    262.673 ns |    506.082 ns |    13,214.47 ns |  0.0305 |       - |       - |    2207 B |
| 'Linq as parallel'     | 10             | List           |    46,315.89 ns |    918.372 ns |  2,054.070 ns |    46,264.38 ns |  0.1221 |       - |       - |    4536 B |
| 'Index based for loop' | 1000           | Array          |     4,939.33 ns |     98.052 ns |    271.702 ns |     4,875.59 ns |       - |       - |       - |         - |
| 'Foreach loop'         | 1000           | Array          |     2,410.61 ns |     48.110 ns |    103.562 ns |     2,355.64 ns |       - |       - |       - |      32 B |
| 'Linq ForEach'         | 1000           | Array          |     2,094.26 ns |     37.763 ns |     91.202 ns |     2,058.47 ns |  0.1221 |       - |       - |    8056 B |
| While                  | 1000           | Array          |     4,906.67 ns |     93.953 ns |    221.457 ns |     4,833.10 ns |       - |       - |       - |         - |
| Do/While               | 1000           | Array          |     5,020.26 ns |     29.675 ns |     26.306 ns |     5,025.32 ns |       - |       - |       - |         - |
| Parallel.ForEach       | 1000           | Array          |    22,898.44 ns |    445.332 ns |    680.069 ns |    22,813.59 ns |       - |       - |       - |    2310 B |
| 'Linq as parallel'     | 1000           | Array          |    43,847.62 ns |    864.210 ns |  1,580.257 ns |    43,397.17 ns |       - |       - |       - |    4537 B |
| 'Index based for loop' | 1000           | List           |     1,010.69 ns |      7.235 ns |      6.768 ns |     1,008.03 ns |       - |       - |       - |         - |
| 'Foreach loop'         | 1000           | List           |     3,486.44 ns |     40.521 ns |     37.903 ns |     3,486.26 ns |       - |       - |       - |      40 B |
| 'Linq ForEach'         | 1000           | List           |     1,987.31 ns |     25.419 ns |     21.226 ns |     1,979.83 ns |  0.1183 |       - |       - |    8056 B |
| While                  | 1000           | List           |     1,041.06 ns |      9.308 ns |      8.706 ns |     1,043.07 ns |       - |       - |       - |         - |
| Do/While               | 1000           | List           |     1,039.82 ns |      9.337 ns |      8.734 ns |     1,040.90 ns |       - |       - |       - |         - |
| Parallel.ForEach       | 1000           | List           |    25,325.61 ns |    501.490 ns |  1,347.220 ns |    25,229.74 ns |       - |       - |       - |    2408 B |
| 'Linq as parallel'     | 1000           | List           |    48,220.78 ns |    971.012 ns |  2,863.050 ns |    49,018.15 ns |       - |       - |       - |    4537 B |
| 'Index based for loop' | 10000          | Array          |    51,511.39 ns |  1,019.911 ns |  2,106.293 ns |    50,191.88 ns |       - |       - |       - |         - |
| 'Foreach loop'         | 10000          | Array          |    23,139.74 ns |    135.326 ns |    126.584 ns |    23,182.88 ns |       - |       - |       - |      32 B |
| 'Linq ForEach'         | 10000          | Array          |    20,897.90 ns |     82.360 ns |     68.775 ns |    20,905.29 ns |  1.1902 |  0.2136 |       - |   80056 B |
| While                  | 10000          | Array          |    49,854.49 ns |    216.613 ns |    180.882 ns |    49,919.93 ns |       - |       - |       - |         - |
| Do/While               | 10000          | Array          |    49,875.96 ns |    205.712 ns |    171.779 ns |    49,926.32 ns |       - |       - |       - |         - |
| Parallel.ForEach       | 10000          | Array          |    64,352.02 ns |    500.535 ns |    468.201 ns |    64,341.99 ns |       - |       - |       - |    3161 B |
| 'Linq as parallel'     | 10000          | Array          |    59,740.14 ns |  1,138.471 ns |  1,118.130 ns |    60,166.33 ns |       - |       - |       - |    4544 B |
| 'Index based for loop' | 10000          | List           |    10,030.75 ns |    123.102 ns |    115.150 ns |     9,981.89 ns |       - |       - |       - |         - |
| 'Foreach loop'         | 10000          | List           |    34,512.38 ns |    318.514 ns |    282.355 ns |    34,595.86 ns |       - |       - |       - |      40 B |
| 'Linq ForEach'         | 10000          | List           |    20,970.50 ns |     94.272 ns |     88.182 ns |    20,966.24 ns |  1.1902 |  0.2136 |       - |   80056 B |
| While                  | 10000          | List           |    10,223.08 ns |     78.291 ns |     61.124 ns |    10,219.18 ns |       - |       - |       - |         - |
| Do/While               | 10000          | List           |    10,248.07 ns |     48.695 ns |     43.167 ns |    10,240.34 ns |       - |       - |       - |         - |
| Parallel.ForEach       | 10000          | List           |    81,553.80 ns |    827.018 ns |    733.130 ns |    81,729.69 ns |       - |       - |       - |    3313 B |
| 'Linq as parallel'     | 10000          | List           |    76,981.03 ns |  1,531.056 ns |  2,146.326 ns |    76,715.06 ns |       - |       - |       - |    4558 B |
| 'Index based for loop' | 100000         | Array          |   496,073.77 ns |  6,025.893 ns |  5,341.798 ns |   494,702.54 ns |       - |       - |       - |       6 B |
| 'Foreach loop'         | 100000         | Array          |   232,657.49 ns |  2,910.554 ns |  2,722.534 ns |   231,333.75 ns |       - |       - |       - |      34 B |
| 'Linq ForEach'         | 100000         | Array          |   299,986.10 ns |  8,373.244 ns | 24,688.699 ns |   297,726.81 ns | 17.5781 | 17.5781 | 17.5781 |  800188 B |
| While                  | 100000         | Array          |   495,707.77 ns |  4,987.818 ns |  4,665.608 ns |   497,111.23 ns |       - |       - |       - |         - |
| Do/While               | 100000         | Array          |   494,853.31 ns |  3,168.636 ns |  2,963.944 ns |   495,127.05 ns |       - |       - |       - |         - |
| Parallel.ForEach       | 100000         | Array          |   198,496.06 ns |  2,753.367 ns |  2,440.788 ns |   198,703.98 ns |       - |       - |       - |    3608 B |
| 'Linq as parallel'     | 100000         | Array          |   192,591.87 ns |  3,790.424 ns |  3,892.486 ns |   191,561.52 ns |       - |       - |       - |    4600 B |
| 'Index based for loop' | 100000         | List           |    99,401.96 ns |  1,511.803 ns |  1,856.630 ns |    98,730.71 ns |       - |       - |       - |         - |
| 'Foreach loop'         | 100000         | List           |   339,826.65 ns |  2,232.017 ns |  1,978.625 ns |   339,485.77 ns |       - |       - |       - |      40 B |
| 'Linq ForEach'         | 100000         | List           |   300,405.63 ns |  8,147.653 ns | 24,023.538 ns |   293,969.48 ns | 18.0664 | 18.0664 | 18.0664 |  800186 B |
| While                  | 100000         | List           |   100,600.41 ns |    329.083 ns |    291.723 ns |   100,611.46 ns |       - |       - |       - |       1 B |
| Do/While               | 100000         | List           |   100,609.29 ns |    395.609 ns |    350.697 ns |   100,634.36 ns |       - |       - |       - |       1 B |
| Parallel.ForEach       | 100000         | List           |   311,811.12 ns |  2,293.173 ns |  2,032.838 ns |   311,812.40 ns |       - |       - |       - |    3625 B |
| 'Linq as parallel'     | 100000         | List           |   273,162.63 ns |  4,747.414 ns |  4,208.459 ns |   272,809.81 ns |       - |       - |       - |    4594 B |
| 'Index based for loop' | 1000000        | Array          | 4,613,328.36 ns | 30,137.959 ns | 28,191.065 ns | 4,606,592.58 ns |       - |       - |       - |      48 B |
| 'Foreach loop'         | 1000000        | Array          | 2,297,380.86 ns | 14,426.561 ns | 13,494.614 ns | 2,298,912.89 ns |       - |       - |       - |      34 B |
| 'Linq ForEach'         | 1000000        | Array          | 3,815,662.68 ns | 54,381.832 ns | 45,411.293 ns | 3,794,641.41 ns | 15.6250 | 15.6250 | 15.6250 | 8000068 B |
| While                  | 1000000        | Array          | 4,645,964.12 ns | 73,763.536 ns | 65,389.453 ns | 4,622,100.78 ns |       - |       - |       - |      48 B |
| Do/While               | 1000000        | Array          | 4,631,129.30 ns | 30,983.694 ns | 28,982.167 ns | 4,634,769.14 ns |       - |       - |       - |      48 B |
| Parallel.ForEach       | 1000000        | Array          | 1,245,977.15 ns |  8,155.405 ns |  7,229.554 ns | 1,246,568.85 ns |       - |       - |       - |    3663 B |
| 'Linq as parallel'     | 1000000        | Array          | 1,009,453.17 ns | 14,133.835 ns | 12,529.276 ns | 1,007,665.92 ns |       - |       - |       - |    4610 B |
| 'Index based for loop' | 1000000        | List           |   984,473.58 ns |  4,449.047 ns |  3,943.965 ns |   986,479.30 ns |       - |       - |       - |      12 B |
| 'Foreach loop'         | 1000000        | List           | 3,424,938.38 ns | 28,215.169 ns | 22,028.548 ns | 3,426,365.23 ns |       - |       - |       - |      64 B |
| 'Linq ForEach'         | 1000000        | List           | 2,676,282.45 ns | 39,390.861 ns | 32,893.153 ns | 2,660,812.50 ns |       - |       - |       - | 8000068 B |
| While                  | 1000000        | List           |   998,414.00 ns |  5,941.047 ns |  4,961.043 ns |   998,340.62 ns |       - |       - |       - |      12 B |
| Do/While               | 1000000        | List           |   997,927.72 ns |  6,149.408 ns |  5,451.290 ns |   998,175.10 ns |       - |       - |       - |      12 B |
| Parallel.ForEach       | 1000000        | List           | 2,338,581.47 ns | 19,959.444 ns | 17,693.527 ns | 2,334,510.16 ns |       - |       - |       - |    3653 B |
| 'Linq as parallel'     | 1000000        | List           | 1,778,368.37 ns | 12,413.545 ns | 11,611.637 ns | 1,780,693.95 ns |       - |       - |       - |    4588 B |