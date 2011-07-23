/*
 * ImMutie
 * Copyright (C) 2010 Arne F. Claassen
 * http://www.claassen.net/geek/blog geekblog [at] claassen [dot] net
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Linq;
using System.Reflection;

namespace Droog.ImMutie {
    public static class Immutable {
        public static T Build<T>(object initializer) {
            return CreateInstance<T>().Populate(initializer);
        }

        private static T CreateInstance<T>() {
            var t = typeof(T);
            return (T)Activator.CreateInstance(t);
        }

        public static T With<T>(this T source, object initializer) {
            return CreateInstance<T>().Populate(source).Populate(initializer);
        }

        private static T Populate<T>(this T instance, object initializer) {
            var t = typeof(T);
            var initializerProperties = initializer.GetType().GetProperties();
            var typeProperties = t.GetProperties().ToDictionary(x => x.Name);
            foreach(var source in initializerProperties) {
                PropertyInfo destination;
                if(!typeProperties.TryGetValue(source.Name, out destination)) {
                    continue;
                }
                if(!destination.PropertyType.IsAssignableFrom(source.PropertyType)) {
                    continue;
                }
                destination.SetValue(instance, source.GetValue(initializer, null), null);
            }
            return instance;
        }
    }
}
