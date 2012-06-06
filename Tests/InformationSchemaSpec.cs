﻿#region BSD License
/* 
Copyright (c) 2011, NETFx
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list 
  of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this 
  list of conditions and the following disclaimer in the documentation and/or other 
  materials provided with the distribution.

* Neither the name of Clarius Consulting nor the names of its contributors may be 
  used to endorse or promote products derived from this software without specific 
  prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH 
DAMAGE.
*/
#endregion

using System;
using System.Data.Entity;
using System.Data.Entity.InformationSchema;
using Xunit;

namespace NetFx.System.Data.Entity.InformationSchema
{
    public class InformationSchemaSpec : IDisposable
    {
        public InformationSchemaSpec()
        {
            using (var context = new TestContext())
            {
                if (context.Database.Exists())
                    context.Database.Delete();

                context.Database.Create();
            }
        }

        public void Dispose()
        {
            using (var context = new TestContext())
            {
                if (context.Database.Exists())
                    context.Database.Delete();
            }
        }

        [Fact]
        public void when_retrieving_tables_then_succeeds()
        {
            using (var context = new TestContext())
            using (var schema = new InformationSchemaContext(context.Database.Connection))
            {
                schema.Tables.Include(x => x.Columns).Dump(Console.Out);
            }
        }
    }

    public class TestContext : DbContext
    {
        public DbSet<TestEntity> Entities { get { return this.Set<TestEntity>(); } }
    }

    public class TestEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}