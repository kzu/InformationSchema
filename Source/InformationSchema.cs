#region BSD License
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

namespace System.Data.Entity.InformationSchema
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Allows inspecting the standard INFORMATION_SCHEMA views on supported databases through Entity Framework
    /// </summary>
    ///	<nuget id="System.Data.Entity.InformationSchema" />
    public class InformationSchemaContext : DbContext
    {
        private readonly BindingFlags ReflectionBinding = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        static InformationSchemaContext()
        {
            Database.SetInitializer<InformationSchemaContext>(null);
        }

        public InformationSchemaContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public InformationSchemaContext(DbConnection existingConnection, bool contextOwnsConnection = false)
            : base(existingConnection, false)
        {
        }

        public InformationSchemaContext(Objects.ObjectContext objectContext, bool dbContextOwnsObjectContext = false)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        public IQueryable<Table> Tables
        {
            get
            {
                return this.Set<Table>()
                    .AsNoTracking()
                    .Where(Table.TablesFilter);
            }
        }

        public IQueryable<Table> Views
        {
            get
            {
                return this.Set<Table>()
                    .AsNoTracking()
                    .Where(Table.ViewsFilter);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RegisterProperties<Table>(modelBuilder, Table.GetHiddenProperties());
            RegisterProperties<Column>(modelBuilder, Column.GetHiddenProperties());

            modelBuilder.Entity<Table>().HasKey(t => new { t.Catalog, t.Schema, t.Name });
            ((dynamic)modelBuilder.Entity<Column>()).HasKey((dynamic)Column.GetKey());
        }

        private void RegisterProperties<TEntity>(DbModelBuilder modelBuilder, IEnumerable<Expression> properties)
            where TEntity : class
        {
            dynamic entity = modelBuilder.Entity<TEntity>();

            foreach (dynamic lambda in properties)
            {
                entity.Property(lambda);
            }
        }
    }
}