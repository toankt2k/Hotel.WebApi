using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.WebApi.core.Attributies
{
    public class CustomAttribute:Attribute
    {
        /// <summary>
        /// chỉ định thuộc tính không được lưu vào database
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class NotMap : Attribute
        {

        }
        /// <summary>
        /// đặt tên để hiển thị cho thuộc tính bawts buooc
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class Required : Attribute
        {
            public string? Msg { get; set; }
        }
    }
}
