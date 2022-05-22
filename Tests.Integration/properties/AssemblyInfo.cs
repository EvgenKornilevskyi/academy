using NUnit.Framework;

[assembly: LevelOfParallelism(12)]
[assembly: Parallelizable(ParallelScope.All)]