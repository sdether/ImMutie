ImMutie 0.1
==========
A simple factory for creating and deriving immutable objects.

Uses
====
- Create immutable data containers for data and message passing without having to create a Builder or create verbose constructors


Dependencies
============
Nunit is used by the Test project. ImMutie itself has no dependencies

License
=======
Copyright (C) 2011 Arne F. Claassen

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

Patches
=======
Patches are welcome and will likely be accepted.  By submitting a patch you assign the copyright to me, Arne F. Claassen.  This is necessary to simplify the number of copyright holders should it become necessary that the copyright need to be re-assigned or the code re-licensed.  The code will always be available under an OSI approved license.

Roadmap
=======
- "Harden" the reflection code to gracefully fail
- Add reflection caching and more efficient object instantiation code.
- Add support for object graphs

Usage
=====

Simply create your data objects with Properties that have public getters and private settters. Now you can instantiate, populate and derive them without ever exposing mutability.

::

  // create a new data class
  public class Person {
     public int Id { get; private set; }
     public string Name { get; private set; }
  }
  
  // create and instance
  var person = Immutable.Create<Person>(new {Id = 43, Name = "Bob"});
  
  // derive new instance with different name (using extension method)
  person.With(new {Name = "Robert"});

Contributors
============
- Arne F. Claassen (sdether)


