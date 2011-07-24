/*
 * ImMutie
 * Copyright (C) 2011 Arne F. Claassen
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
using NUnit.Framework;

namespace Droog.ImMutie.Test {

    [TestFixture]
    public class TImmutable {

        [Test]
        public void Can_create_immutable() {
            var a = Immutable.Build<Person>(new { Id = 42, Name = "Bob" });
            Assert.AreEqual(42, a.Id);
            Assert.AreEqual("Bob", a.Name);
        }

        [Test]
        public void Can_derive_immutable() {
            var a = Immutable.Build<Person>(new { Id = 42, Name = "Bob" });
            var b = a.With(new { Id = 55 });
            Assert.AreNotSame(a, b);
            Assert.AreEqual(42, a.Id);
            Assert.AreEqual(55, b.Id);
            Assert.AreEqual(a.Name, b.Name);
        }

        [Test]
        public void Can_provide_extraneous_fields() {
            var a = Immutable.Build<Person>(new {Foo = "bar", Id = 23});
            Assert.AreEqual(23, a.Id);
            Assert.IsNull(a.Name);
        }

        [Test]
        public void Can_create_with_complex_type() {
            var Complex = new BaseType();
            var a = Immutable.Build<HazAComplexType>(new {Complex});
            Assert.IsNotNull(a.Complex);
            Assert.AreSame(Complex,a.Complex);
        }

        [Test]
        public void Can_create_with_convertible_type() {
            var Complex = new SubType();
            var a = Immutable.Build<HazAComplexType>(new { Complex });
            Assert.IsNotNull(a.Complex);
            Assert.AreSame(Complex, a.Complex);
        }

        [Test]
        public void Ignores_mismatch_values() {
            var a = Immutable.Build<Person>(new {Id = 2.0});
            Assert.AreEqual(0, a.Id);
        }


        public class Person {
            public int Id { get; private set; }
            public string Name { get; private set; }
        }

        public class HazAComplexType {
            public BaseType Complex { get; private set; }
        }

        public class BaseType {
            public readonly Guid Id = Guid.NewGuid();
        }
        public class SubType : BaseType {}
    }
}
