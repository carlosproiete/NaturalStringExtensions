﻿#region Copyright 2021 C. Augusto Proiete & Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NaturalStringExtensions.Tests
{
    public class StringComparisonExtensionsTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void StringComparison_AsNatural_behaves_the_same_as_using_NaturalStringComparer_StringComparison_constructor(StringComparison comparisonType)
        {
            var input = new[]
            {
                "Folder 3",
                "Folder 13",
                "FOlder 1",
                "Folder 1",
                "Folder 26",
                "Folder 10",
                "Folder 6",
                "Folder 4",
                "Folder 5",
                "FOlder 5",
                "Folder 2",
                "00Folder 5",
                "00Folder 1",
                "00FOlder 1",
                "00Folder 10",
            };

            var expected = input
                .OrderBy(v => v, new NaturalStringComparer(comparisonType))
                .ToArray();

            var result = input
                .OrderBy(v => v, comparisonType.AsNatural())
                .ToArray();

            result.Should().BeEquivalentTo(expected,
                options => options.WithStrictOrdering());
        }

        public static IEnumerable<object[]> TestData
        {
            get
            {
                var allComparisons = Enum.GetValues(typeof(StringComparison)).Cast<StringComparison>();

                return allComparisons
                    .Select(x => new object[]
                        {
                            x,
                        }
                    );
            }
        }
    }
}
