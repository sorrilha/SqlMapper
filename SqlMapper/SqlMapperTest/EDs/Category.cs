using System;
using SqlMapper.Framework;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapperTest.EDs
{
    [Table("Categories")]
    public class Category : IEntity
    {
        [Pk("CategoryID",true)]
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }
        public String Description { get; set; }
        private readonly String[] mapMapName = { "CategoryID", "CategoryName", "Description" };

        public String[] getMapNames()
        {
            return mapMapName;
        }

        public int getId()
        {
            return CategoryID;
        }
    }
}
