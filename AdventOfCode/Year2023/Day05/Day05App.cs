using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2023
{
    class MapData {
        public long Destination { get; set; } = 0;
        public long Source { get; set; } = 0;
        public long Range { get; set; } = 0;
    }

    class Map {
        public string MapType { get; set; } = "";
        public List<MapData> Data { get; set; }
        //public Dictionary<long, long> Correspondings { get; set; } = new Dictionary<long, long>();

        public Map() { 
            Data = new List<MapData>();
        }
    }

    class SeedToLocation {
        public long SourceStart { get; set; } = 0;
        public long SourceEnd { get; set; } = 0;
        public long Adjustment { get; set; } = 0;
    }

    internal class Day05App
    {
        private List<string> Input = AppExtensions.GetInputList($"./Year2023/Day05/input.txt");

        List<Map> Maps = new List<Map>();
        List<SeedToLocation> SeedToLocations = new List<SeedToLocation>();

        public void Execute() {           

            // Prepare the data
            for (var i = 2; i < Input.Count; i++) {
                var line = Input[i];

                if (line.EndsWith("map:")) {
                    var map = new Map();
                    map.MapType = line.Replace(" map:", "").Trim();

                    for (var mi = i + 1; mi < Input.Count; mi++) {
                        var dataLine = Input[mi];
                        if (dataLine.Trim() != "") {
                            var dataInput = dataLine.Trim().Split(' ');

                            var data = new MapData() {
                                Destination = Convert.ToInt64(dataInput[0]),
                                Source = Convert.ToInt64(dataInput[1]),
                                Range = Convert.ToInt64(dataInput[2]),
                            };

                            map.Data.Add(data);

                        } else {
                            break;
                        }

                    }

                    map.Data = map.Data.OrderBy(m => m.Destination).ToList();

                    Maps.Add(map);
                }
            }

            Part1();
            Part2();
        }

        private void Part1()
        {
            var seeds = Input[0].Replace("seeds:", "").Split(' ').Where(m => m.Trim().Length > 0).Select(m => Convert.ToInt64(m)).ToList();

            long lowestLocation = 0;

            seeds.ForEach(seed => {
                var result = ProcessSeedToLocation(seed);

                if (lowestLocation == 0 || result < lowestLocation) {
                    lowestLocation = result;
                }
            });

            Console.WriteLine(lowestLocation);
        }

        /// <summary>
        /// Brute force, not good, never finished as i never let it run long enough.
        /// Couldn't visualize a way in my head on how to map the data so you don't have to brute force.
        /// </summary>
        private void Part2() {
            var seeds = Input[0].Replace("seeds:", "").Split(' ').Where(m => m.Trim().Length > 0).Select(m => Convert.ToInt64(m)).ToList();

            var seedData = new Dictionary<long, long>();
            for (var i = 0; i < seeds.Count; i = i + 2) {
                seedData.Add(seeds[i], seeds[i + 1]);
            }

            var orderedSeedData = seedData.OrderBy(m => m.Key).ToList();

            long lowestLocation = 0;

            for (var i = 0; i < orderedSeedData.Count; i++) {
                var seed = orderedSeedData[i].Key;
                var range = orderedSeedData[i].Value;

                for (var si = 0; si < range; si++) {
                    long result = ProcessSeedToLocation(seed);

                    if (lowestLocation == 0 || result < lowestLocation) {
                        lowestLocation = result;
                    }

                    seed++;
                }
            }

            Console.WriteLine(lowestLocation);
        }

        private long ProcessSeedToLocation(long seed) {
            long pos = seed;

            var seedSoil = Maps.First(m => m.MapType == "seed-to-soil");
            pos = getCorrespondingPos(seedSoil, pos);

            var soilFertilizer = Maps.First(m => m.MapType == "soil-to-fertilizer");
            pos = getCorrespondingPos(soilFertilizer, pos);

            var fertilizerWater = Maps.First(m => m.MapType == "fertilizer-to-water");
            pos = getCorrespondingPos(fertilizerWater, pos);

            var waterLight = Maps.First(m => m.MapType == "water-to-light");
            pos = getCorrespondingPos(waterLight, pos);

            var lightTemp = Maps.First(m => m.MapType == "light-to-temperature");
            pos = getCorrespondingPos(lightTemp, pos);

            var tempHumidity = Maps.First(m => m.MapType == "temperature-to-humidity");
            pos = getCorrespondingPos(tempHumidity, pos);

            var humidityLocation = Maps.First(m => m.MapType == "humidity-to-location");
            pos = getCorrespondingPos(humidityLocation, pos);

            return pos;
        }
                
        private long getCorrespondingPos(Map input, long pos) {
            long output = pos;

            var dataPoints = input.Data.Where(m => pos >= m.Source && pos <= m.Source + m.Range).FirstOrDefault();
            if (dataPoints != null) {
                var diff = dataPoints.Destination - dataPoints.Source;
                output = pos + diff;
            }
            //for (var i = 0; i < dataPoints.Count; i++) {
            //    var dataPoint = dataPoints[i];

            //}

            //for (var i = 0; i < input.Data.Count; i++) {
            //    var data = input.Data[i];
            //    if (pos >= data.Source && pos <= data.Source + data.Range) {
            //        var diff = data.Destination - data.Source;
            //        output = pos + diff;
            //        break;
            //    }
            //}

            return output;
        }

    }
}
