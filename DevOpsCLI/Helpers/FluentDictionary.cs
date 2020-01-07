﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Helpers
{
    using System;
    using System.Collections.Generic;

    public class FluentDictionary
    {
        private readonly IDictionary<string, object> dictionary;

        internal FluentDictionary(IDictionary<string, object> dictionary)
        {
            this.dictionary = dictionary;
        }

        public static FluentDictionary For(IDictionary<string, object> dictionary)
        {
            return new FluentDictionary(dictionary);
        }

        public FluentDictionary Add<T>(string key, T value)
        {
            this.dictionary[key] = value;
            return this;
        }

        public FluentDictionary Add<T>(string key, T value, T defaultValue)
            where T : IComparable
        {
            if (value != null && value.CompareTo(defaultValue) != 0)
            {
                this.dictionary[key] = value;
            }

            return this;
        }

        public FluentDictionary Add<T>(string key, T value, Func<bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (condition())
            {
                this.dictionary[key] = value;
            }

            return this;
        }
    }
}
