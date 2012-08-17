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
    using System.Data.Common;
    using System.Data.Entity.InformationSchema.Internal;
    using System.Linq;

    /// <summary>
    /// Allows inspecting the standard INFORMATION_SCHEMA views on supported databases through Entity Framework
    /// </summary>
    ///	<nuget id="System.Data.Entity.InformationSchema" />
    public class InformationSchemaContext : DbContext
    {
        /// <summary>
        /// Sets the initializer for this context to null, as we never create 
        /// the schema views.
        /// </summary>
        static InformationSchemaContext()
        {
            Database.SetInitializer<InformationSchemaContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationSchemaContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public InformationSchemaContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        ///  Constructs a new context instance using the existing connection to connect to a database.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed, otherwise
        /// the caller must dispose the connection.</param>
        public InformationSchemaContext(DbConnection existingConnection, bool contextOwnsConnection = false)
            : base(existingConnection, false)
        {
        }

        /// <summary>
        /// Constructs a new context instance around an existing ObjectContext.
        /// </summary>
        /// <param name="objectContext">An existing ObjectContext to wrap with the new context.</param>
        /// <param name="dbContextOwnsObjectContext">If set to true the ObjectContext is disposed when the DbContext is disposed,
        /// otherwise the caller must dispose the connection.</param>
        public InformationSchemaContext(Objects.ObjectContext objectContext, bool dbContextOwnsObjectContext = false)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        /// <summary>
        /// Gets the tables in the database.
        /// </summary>
        public IQueryable<Table> Tables
        {
            get
            {
                return this.Set<Table>()
                    .AsNoTracking()
                    .Include("COLUMNS.KeyInfo")
                    .Include("CONSTRAINTS.COLUMNS")
                    .Where(t => t.TABLE_TYPE == "BASE TABLE");
            }
        }

        /// <summary>
        /// Gets the views in the database.
        /// </summary>
        public IQueryable<Table> Views
        {
            get
            {
                return this.Set<Table>()
                    .AsNoTracking()
                    .Include(t => t.COLUMNS)
                    .Where(t => t.TABLE_TYPE == "VIEW");
            }
        }

        /// <summary>
        /// Customizes the default mappings.
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map internal properties.
            modelBuilder.Entity<Table>().Property(x => x.TABLE_TYPE);
            modelBuilder.Entity<Column>().Property(x => x.TABLE_CATALOG);
            modelBuilder.Entity<Column>().Property(x => x.TABLE_SCHEMA);
            modelBuilder.Entity<Column>().Property(x => x.TABLE_NAME);
            modelBuilder.Entity<Column>().Property(x => x.IS_NULLABLE);

            modelBuilder.Entity<Table>().HasKey(x => new { x.Catalog, x.Schema, x.Name });
            modelBuilder.Entity<Column>().HasKey(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME, x.Name });
            modelBuilder.Entity<Table>().HasMany(x => x.COLUMNS).WithRequired().HasForeignKey(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME });
            modelBuilder.Entity<Table>().HasMany(x => x.CONSTRAINTS).WithRequired().HasForeignKey(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME });

            modelBuilder.Entity<Column>()
                .HasOptional(x => x.KeyInfo)
                .WithRequired();

            modelBuilder.Entity<KeyInfo>().HasKey(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME, x.COLUMN_NAME });

            modelBuilder.Entity<TableConstraints>().HasKey(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME, x.CONSTRAINT_CATALOG, x.CONSTRAINT_SCHEMA, x.CONSTRAINT_NAME });
            modelBuilder.Entity<TableConstraints>().HasMany(x => x.COLUMNS).WithRequired().HasForeignKey(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME, x.CONSTRAINT_CATALOG, x.CONSTRAINT_SCHEMA, x.CONSTRAINT_NAME });

            modelBuilder.Entity<ConstraintColumnUsage>().HasKey(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME, x.COLUMN_NAME });
        }
    }
}