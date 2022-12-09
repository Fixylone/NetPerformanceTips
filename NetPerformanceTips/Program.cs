// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using NetPerformanceTips;

// Specify here name of the class that you want to be benchmarked.
BenchmarkRunner.Run<ValueTypeVsReferenceType>();