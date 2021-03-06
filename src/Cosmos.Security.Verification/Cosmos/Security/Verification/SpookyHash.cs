﻿using Factory = Cosmos.Security.Verification.SpookyHashFactory;

namespace Cosmos.Security.Verification
{
    /// <summary>
    /// SpookyHash Function Factory
    /// </summary>
    public class SpookyHash
    {
        public static ISpookyHash Create(SpookyHashTypes type = SpookyHashTypes.SpookyHash2Bit128) => Factory.Create(type);

        public static ISpookyHash Create(SpookyHashTypes type, SpookyHashConfig config) => Factory.Create(type, config);
    }
}