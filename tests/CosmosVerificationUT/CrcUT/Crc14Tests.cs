﻿using Cosmos.Security.Verification.CRC;
using Shouldly;
using Xunit;

namespace CrcUT
{
    [Trait("CrcUT", "Crc14Tests")]
    public class Crc14Tests
    {
        [Theory(DisplayName = "CRC-14/DARC")]
        [InlineData("Nice", "372C", "11011100101100", "11011100101100")]
        [InlineData("Nice Boat", "3030", "11000000110000", "11000000110000")]
        public void Crc14DarcTest(string data, string hex, string bin, string binWithZero)
        {
            var function = CrcFactory.Create(CrcTypes.Crc14Darc);
            var hashVal = function.ComputeHash(data);
            hashVal.AsHexString(true).ShouldBe(hex);
            hashVal.AsBinString().ShouldBe(bin);
            hashVal.AsBinString(true).ShouldBe(binWithZero);
        }
    }
}