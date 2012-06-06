
namespace System.Data.Entity.InformationSchema
{
    using System.ComponentModel.DataAnnotations;

    [ComplexType]
    public class CharacterSet
    {
        /// <summary>
        /// Returns master. This indicates the database in which the character set is located, if the column is character data or text data type. Otherwise, NULL is returned.
        /// </summary>
        [Column("CHARACTER_SET_CATALOG")]
        public string Catalog { get; set; }

        /// <summary>
        /// Always returns NULL. (for T-SQL)
        /// </summary>
        [Column("CHARACTER_SET_SCHEMA")]
        public string Schema { get; set; }

        /// <summary>
        /// Returns the unique name for the character set if this column is character data or text data type. Otherwise, NULL is returned.
        /// </summary>
        [Column("CHARACTER_SET_NAME")]
        public string Name { get; set; }
    }
}
